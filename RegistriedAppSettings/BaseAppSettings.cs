using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace RegistriedAppSettings
{
    /// <summary>
    /// A dictionary of settings that can be encrypted and saved to the registry
    /// </summary>
    public class BaseAppSettings
    {
        private readonly string _encryptionSalt;
        private string _strRegLoc;
        private Dictionary<string, BaseAppSetting> _dicValues;
        private string _strLastError;
        private bool _bLoadedFromFile;

        /// <summary>
        /// Initialize GlobalAppSettings with default values
        /// </summary>
        public BaseAppSettings()
        {
            //Salt can be whatever you want, it's recommended to use a generated secure string
            _encryptionSalt = "CHANGE-ME";
            //Default Registry location, change to your own folder if you like
            _strRegLoc = @"SOFTWARE\KonaSurfCo";
            _dicValues = new Dictionary<string, BaseAppSetting>();
            _strLastError = "";
            _bLoadedFromFile = false;
        }

        /// <summary>
        /// Set the registry location to save settings (Default "SOFTWARE\KonaSurfCo")
        /// </summary>
        public string RegistryLocation
        {
            set => _strRegLoc = value;
        }

        /// <summary>
        /// Get or Set the dictionary of values used for settings
        /// </summary>
        /// <returns>Dictionary(Of String, BaseAppSetting)</returns>
        public Dictionary<string, BaseAppSetting> Values
        {
            get => _dicValues;
            set => _dicValues = value;
        }

        /// <summary>
        /// Get the last error this class has encountered
        /// </summary>
        /// <returns>A string representing an error</returns>
        public string LastError => _strLastError;

        /// <summary>
        /// Get if the settings were loaded from a file or not
        /// </summary>
        /// <returns>A boolean indicating if the settings were loaded from a file or not</returns>
        public bool LoadedFromFile => _bLoadedFromFile;

        /// <summary>
        /// Save (and encrypt) the settings in the dictionary to the registry
        /// </summary>
        /// <returns>A boolean indicating if the operation was successful or not</returns>
        public bool Save()
        {
            _strLastError = "";

            try
            {
                // Create or Open a root registry key for the application
                var regRootKey = Registry.CurrentUser.CreateSubKey(_strRegLoc);
                // warnings insist that regRootKey will never be null, but I'll check just to be safe
                if (regRootKey != null)
                {
                    foreach (var kvpBaseSetting in _dicValues)
                    {
                        try
                        {
                            // Create or Open a sub key from our root key
                            var regSettingKey = regRootKey.CreateSubKey(kvpBaseSetting.Key);
                            // Set the Encrypted value so we know whether to decrypt it on load
                            // warnings insist that regSettingsKey will never be null, but I'll check just to be safe
                            if (regSettingKey != null)
                            {
                                regSettingKey.SetValue("Encrypted", kvpBaseSetting.Value.Encrypt.ToString());
                                // Save the value and type
                                if (kvpBaseSetting.Value.Encrypt)
                                {
                                    using (var aeKey = new AppEncryption(_encryptionSalt, kvpBaseSetting.Key))
                                    {
                                        var encValue = aeKey.EncryptData(kvpBaseSetting.Value.Value);
                                        // Save Encrypted Value
                                        regSettingKey.SetValue(kvpBaseSetting.Value.SettingType.ToString() + "Setting",
                                            encValue);
                                    }
                                }
                                else
                                {
                                    // Save Raw Value
                                    regSettingKey.SetValue(kvpBaseSetting.Value.SettingType.ToString() + "Setting",
                                        kvpBaseSetting.Value.Value);
                                }

                                // Close the sub key
                                regSettingKey.Close();
                            }
                        }
                        catch (Exception ex2)
                        {
                            _strLastError = "Error saving setting '" + kvpBaseSetting.Key + "': " + ex2.Message;
                        }

                        if (!string.IsNullOrEmpty(_strLastError))
                        {
                            break;
                        }
                    }

                    // Close the root key
                    regRootKey.Close();
                }
                else
                {
                    _strLastError = "Error saving setting: Root Key does not exist";
                }
            }
            catch (Exception ex)
            {
                _strLastError = ex.Message;
            }

            if (string.IsNullOrEmpty(_strLastError))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Load (and decrypt) the settings from the registry
        /// </summary>
        /// <returns>A boolean indicating if the operation was successful or not</returns>
        public bool Load()
        {
            _strLastError = "";

            try
            {
                // Create or Open a root registry key for the application
                var regRootKey = Registry.CurrentUser.OpenSubKey(_strRegLoc, false);
                if (regRootKey != null)
                {
                    foreach (var kvpBaseSetting in _dicValues)
                    {
                        try
                        {
                            // Open a sub key from our root key
                            var regSettingKey = regRootKey.OpenSubKey(kvpBaseSetting.Key, false);
                            // Get the Encrypted value so we know whether to decrypt it
                            if (regSettingKey != null)
                            {
                                // Warning, this could be null even though I checked for null...?
                                var strIsEncrypted = regSettingKey.GetValue("Encrypted").ToString();
                                // Make sure it's not NULL
                                if (string.IsNullOrEmpty(strIsEncrypted) == false)
                                {
                                    kvpBaseSetting.Value.Encrypt = Convert.ToBoolean(strIsEncrypted);

                                    // Load the value and type
                                    string encValue;
                                    if (kvpBaseSetting.Value.Encrypt)
                                    {
                                        // Decrypt the value
                                        var aeKey = new AppEncryption(_encryptionSalt, kvpBaseSetting.Key);
                                        // Warning, this could be null too
                                        encValue = aeKey.DecryptData(regSettingKey.GetValue(kvpBaseSetting.Value.SettingType.ToString() + "Setting").ToString());
                                    }
                                    else
                                    {
                                        // Load the raw value, could also be null... I check for it below
                                        encValue = regSettingKey.GetValue(kvpBaseSetting.Value.SettingType.ToString() + "Setting").ToString();
                                    }

                                    if (string.IsNullOrEmpty(encValue) == false && !string.IsNullOrEmpty(encValue.Trim()))
                                    {
                                        kvpBaseSetting.Value.Value = encValue.Trim();
                                    }
                                    else
                                    {
                                        _strLastError = kvpBaseSetting.Key + " is Not set";
                                    }
                                }
                                else
                                {
                                    _strLastError = kvpBaseSetting.Key + " is Not set";
                                }
                                // Close the sub key
                                regSettingKey.Close();
                            }
                        }
                        catch (Exception ex2)
                        {
                            _strLastError = "Error loading setting '" + kvpBaseSetting.Key + "': " + ex2.Message;
                        }

                        if (!string.IsNullOrEmpty(_strLastError))
                        {
                            break;
                        }
                    }

                    // Close the root key
                    regRootKey.Close();
                }
                else
                {
                    _strLastError = "Error loading setting: Root Key does not exist";
                }
            }
            catch (Exception ex)
            {
                _strLastError = ex.Message;
            }

            if (string.IsNullOrEmpty(_strLastError))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Save (and encrypt) the settings in the dictionary to a file
        /// </summary>
        public bool SaveToFile()
        {
            var bSaved = false;

            // All this does is save existing values...
            _strLastError = "";

            var dgSaveIt = new SaveFileDialog();
            dgSaveIt.Filter = @"Configuration Files (*.cfg)|*.cfg";
            dgSaveIt.DefaultExt = "cfg";
            dgSaveIt.FileName = "SavedSettings.cfg";
            dgSaveIt.Title = @"Select a save spot.";

            if (dgSaveIt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create or Open the file
                    //FileSystem.FileOpen(1, dgSaveIt.FileName, OpenMode.Output);
                    // the new C# way
                    using (var swSaveFile = new StreamWriter(dgSaveIt.FileName))
                    {
                        foreach (var kvpBaseSetting in _dicValues)
                        {
                            try
                            {
                                if (kvpBaseSetting.Value.Encrypt)
                                {
                                    var aeKey = new AppEncryption(_encryptionSalt, kvpBaseSetting.Key);
                                    var encValue = aeKey.EncryptData(kvpBaseSetting.Value.Value);
                                    // Save Encrypted Value
                                    //FileSystem.PrintLine(1, kvpKonaSetting.Key + "=" + encValue);
                                    swSaveFile.WriteLine(kvpBaseSetting.Key + "=" + encValue);
                                }
                                else
                                {
                                    // Save Raw Value
                                    //FileSystem.PrintLine(1, kvpKonaSetting.Key + "=" + kvpKonaSetting.Value.Value);
                                    swSaveFile.WriteLine(kvpBaseSetting.Key + "=" + kvpBaseSetting.Value.Value);
                                }
                            }
                            catch (Exception ex2)
                            {
                                _strLastError = "Error saving setting '" + kvpBaseSetting.Key + "': " + ex2.Message;
                            }

                            if (!string.IsNullOrEmpty(_strLastError))
                            {
                                break;
                            }
                        } 
                        swSaveFile.Close();
                    }
                    //FileSystem.FileClose(1);

                    bSaved = true;
                }
                catch (Exception ex)
                {
                    _strLastError = ex.Message;
                }
            }

            if (!string.IsNullOrEmpty(_strLastError))
            {
                MessageBox.Show(_strLastError, @"Unable To Save", MessageBoxButtons.OK);
            }

            return bSaved;
        }

        /// <summary>
        /// Load (and decrypt) the settings from a file
        /// </summary>
        public bool LoadFromFile()
        {
            var bLoaded = false;

            // This will load the settings from a file
            _strLastError = "";

            var dgOpenIt = new OpenFileDialog();
            dgOpenIt.Filter = @"Configuration Files (*.cfg)|*.cfg";
            dgOpenIt.DefaultExt = "cfg";
            dgOpenIt.FileName = "SavedSettings.cfg";
            dgOpenIt.Title = @"Select a file to open.";

            if (dgOpenIt.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Open the file
                    //FileSystem.FileOpen(1, dgOpenIt.FileName, OpenMode.Input);
                    // The new C# way
                    var strLines = File.ReadLines(dgOpenIt.FileName);
                
                    foreach (var strLine in strLines)
                    {
                        //string strLine = FileSystem.LineInput(1);
                        string[] arSeps = { "=" };
                        var arSetting = strLine.Split(arSeps, 2, StringSplitOptions.None);
                        if (arSetting.Length > 1)
                        {
                            //var bEncrypted = false;
                            var settingName = arSetting[0].Trim();

                            if (_dicValues[settingName].Encrypt)
                            {
                                // Decrypt the value
                                try
                                {
                                    var aeKey = new AppEncryption(_encryptionSalt, settingName);
                                    _dicValues[settingName].Value = aeKey.DecryptData(arSetting[1].Trim());
                                }
                                catch (System.Security.Cryptography.CryptographicException)
                                {
                                    // It didn't work
                                }
                            }
                            else
                            {
                                // Load the raw value
                                _dicValues[settingName].Value = arSetting[1].Trim();
                            }
                        }
                    }
                    //FileSystem.FileClose(1);

                    _bLoadedFromFile = true;
                    bLoaded = true;
                }
                catch (Exception ex)
                {
                    _strLastError = ex.Message;
                }
            }

            if (!string.IsNullOrEmpty(_strLastError))
            {
                MessageBox.Show(_strLastError, @"Unable To Load", MessageBoxButtons.OK);
            }

            return bLoaded;
        }
    }

    /// <summary>
    /// A single instance of a setting stored in the settings dictionary
    /// </summary>
    public class BaseAppSetting
    {
        private EnSettingTypes _enSettingType;
        private string _strValue;
        private bool _bEncrypt;

        /// <summary>
        /// The current set of object types that I can work with. All are stored as string and converted later.
        /// </summary>
        public enum EnSettingTypes
        {
            /// <summary>
            /// Read as String
            /// </summary>
            String,
            /// <summary>
            /// Read as Boolean
            /// </summary>
            Bool,
            /// <summary>
            /// Read as Integer
            /// </summary>
            Integer,
            /// <summary>
            /// Read as Decimal
            /// </summary>
            Decimal,
            /// <summary>
            /// Read as Hex Code
            /// </summary>
            Hex,
            /// <summary>
            /// Read as File Location
            /// </summary>
            ImageLocation
        }

        /// <summary>
        /// Where are product images stored?
        /// </summary>
        public enum EnImageLocationType
        {
            /// <summary>
            /// Location Not Set
            /// </summary>
            NotSet,
            /// <summary>
            /// Location is Local File
            /// </summary>
            File,
            /// <summary>
            /// Location is a URL
            /// </summary>
            Web
        }

        /// <summary>
        /// Initialize with default values (String, Empty Value, Do Encrypt)
        /// </summary>
        public BaseAppSetting()
        {
            _enSettingType = EnSettingTypes.String;
            _strValue = "";
            _bEncrypt = true;
        }

        /// <summary>
        /// The object type for this entry
        /// </summary>
        /// <returns>A setting type</returns>
        public EnSettingTypes SettingType
        {
            get => _enSettingType;
            set => _enSettingType = value;
        }

        /// <summary>
        /// The string representation of the value for this entry (Checked against the object type)
        /// </summary>
        /// <returns></returns>
        public string Value
        {
            get => _strValue;
            set
            {
                switch (_enSettingType)
                {
                    case EnSettingTypes.String:
                    {
                        _strValue = value;
                        break;
                    }
                    case EnSettingTypes.Bool:
                    {
                        if (bool.TryParse(value, out var bTemp))
                        {
                            _strValue = bTemp.ToString();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(Value), @"Specified Boolean value could not be parsed.");
                        }

                        break;
                    }
                    case EnSettingTypes.Integer:
                    {
                        if (int.TryParse(value, out var intTemp))
                        {
                            _strValue = intTemp.ToString();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(Value), @"Specified Integer value could not be parsed.");
                        }

                        break;
                    }
                    case EnSettingTypes.Decimal:
                    {
                        if (decimal.TryParse(value, out var decTemp))
                        {
                            _strValue = decTemp.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(Value), @"Specified Decimal value could not be parsed.");
                        }

                        break;
                    }
                    case EnSettingTypes.Hex:
                    {
                        if (int.TryParse(value, NumberStyles.HexNumber,
                                CultureInfo.InvariantCulture, out _))
                            _strValue = value;
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(Value), @"Specified Hex value could not be parsed.");
                        }

                        break;
                    }
                    case EnSettingTypes.ImageLocation:
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            value = "NotSet";
                        }

                        if (Enum.TryParse(value, out EnImageLocationType enTempILoc))
                        {
                            _strValue = enTempILoc.ToString();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(nameof(Value), @"Specified ImageLocation could not be parsed.");
                        }

                        break;
                    }

                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(Value), @"Specified Value is not a valid SettingType");
                    }
                }
            }
        }

        /// <summary>
        /// Whether this entry should be encrypted when saved or not
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Encrypt
        {
            get => _bEncrypt;
            set => _bEncrypt = value;
        }
    }
}