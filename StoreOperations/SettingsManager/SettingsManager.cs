using System;
using System.Data.SqlClient;
using RegistriedAppSettings;

namespace StoreOperations.SettingsManager
{
    /// <summary>
    /// A set of global app settings that I use a lot in my programs
    /// </summary>
    public class SettingsManager
    {
        private readonly BaseAppSettings _gsSettings;

        /// <summary>
        /// Load with default values
        /// </summary>
        public SettingsManager()
        {
            // Default Settings - Set Custom Registry Location in the Client Program
            _gsSettings = new BaseAppSettings();

            // Create Database Connection String Variable (One per store if needed)
            var ksDatabaseCon = new BaseAppSetting()
            {
                SettingType = BaseAppSetting.EnSettingTypes.String,
                Encrypt = true
            };
            _gsSettings.Values.Add("MSConnection", ksDatabaseCon);

            // Image Variables (For product images)
            var ksImgLoc = new BaseAppSetting()
            {
                SettingType = BaseAppSetting.EnSettingTypes.ImageLocation,
                Encrypt = true,
                Value = BaseAppSetting.EnImageLocationType.NotSet.ToString()
            };
            _gsSettings.Values.Add("ImageLocation", ksImgLoc);

            var ksImgPath = new BaseAppSetting()
            {
                SettingType = BaseAppSetting.EnSettingTypes.String,
                Encrypt = true
            };
            _gsSettings.Values.Add("ImagePath", ksImgPath);

            var ksAddSubF = new BaseAppSetting()
            {
                SettingType = BaseAppSetting.EnSettingTypes.Bool,
                Encrypt = false,
                Value = true.ToString()
            };
            _gsSettings.Values.Add("AddSubfolders", ksAddSubF);

            var ksImgEnding = new BaseAppSetting()
            {
                SettingType = BaseAppSetting.EnSettingTypes.String,
                Encrypt = true,
                Value = ".jpg"
            };
            _gsSettings.Values.Add("ImageEnding", ksImgEnding);
        }

        /// <summary>
        /// Set the registry location to save settings (Default "SOFTWARE\KonaSurfCo")
        /// </summary>
        public string RegistryLocation
        {
            set => _gsSettings.RegistryLocation = value;
        }

        /// <summary>
        /// Load (and decrypt) the settings from a file
        /// </summary>
        private void LoadFromFile(object sender,EventArgs e)
        {
            if (_gsSettings.LoadFromFile())
            {
                var btnLoad = (System.Windows.Forms.Button)sender;
                var dgAcPrompt = (AppSettingsDialog)btnLoad.TopLevelControl;
                FillAppSettingsDialog(dgAcPrompt);
            }
        }

        /// <summary>
        /// Main Store connection
        /// </summary>
        /// <returns>A new sql connection for the main store</returns>
        public SqlConnection GetStoreConnection => new SqlConnection(_gsSettings.Values["MSConnection"].Value);

        /// <summary>
        /// Where product images are stored (Web or Files)
        /// </summary>
        /// <returns>ImageLocationType Enum</returns>
        public BaseAppSetting.EnImageLocationType ImageLocation
        {
            get
            {
                if (Enum.TryParse(_gsSettings.Values["ImageLocation"].Value, out BaseAppSetting.EnImageLocationType enTempILoc))
                {
                    return enTempILoc;
                }

                return BaseAppSetting.EnImageLocationType.NotSet;
            }
        }

        /// <summary>
        /// Where product images are stored (Url or file location)
        /// </summary>
        /// <returns>String representing a file location</returns>
        public string ImagePath => _gsSettings.Values["ImagePath"].Value;

        /// <summary>
        /// Should subfolders be automatically added to file locations? (Like Magento)
        /// </summary>
        /// <returns>Boolean</returns>
        public bool AddSubFolders => Convert.ToBoolean(_gsSettings.Values["AddSubfolders"].Value);

        /// <summary>
        /// Filename ending for images (.jpg)
        /// </summary>
        /// <returns>String representing a filename ending</returns>
        public string ImageEnding => _gsSettings.Values["ImageEnding"].Value;

        /// <summary>
        /// The last error encountered when saving or loading the settings
        /// </summary>
        /// <returns>String representing an error</returns>
        public string LastError => _gsSettings.LastError;

        /// <summary>
        /// Automatically prompt for all setting values and save them
        /// </summary>
        /// <returns>Boolean that indicates if the settings saved successfully</returns>
        public bool PromptForValues()
        {
            // Get All info in one prompt
            using (var dgAcPrompt = new AppSettingsDialog())
            {
                FillAppSettingsDialog(dgAcPrompt);
            
                if (dgAcPrompt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Check if connection(s) work
                    var strConStatus = TestDbCon(dgAcPrompt.tbMSCon.Text);
                    if (strConStatus == "SUCCESS!")
                    {
                        _gsSettings.Values["MSConnection"].Value = dgAcPrompt.tbMSCon.Text;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(@"MSConnection: " + strConStatus, @"Connection failed", System.Windows.Forms.MessageBoxButtons.OK);
                    }

                    // Save Image Info
                    if (dgAcPrompt.rbFromFolder.Checked)
                    {
                        _gsSettings.Values["ImageLocation"].Value = BaseAppSetting.EnImageLocationType.File.ToString();
                        if (System.IO.Directory.Exists(dgAcPrompt.tbFolderPath.Text))
                        {
                            _gsSettings.Values["ImagePath"].Value = dgAcPrompt.tbFolderPath.Text;
                        }
                    }
                    if (dgAcPrompt.rbFromWeb.Checked)
                    {
                        _gsSettings.Values["ImageLocation"].Value = BaseAppSetting.EnImageLocationType.Web.ToString();
                        _gsSettings.Values["ImagePath"].Value = dgAcPrompt.tbWebPath.Text.Trim('/');
                        if (ImagePath.ToLower().StartsWith("http://") == false && ImagePath.ToLower().StartsWith("https://") == false)
                        {
                            _gsSettings.Values["ImagePath"].Value = "http://" + ImagePath;
                        }
                    }
                    _gsSettings.Values["ImageEnding"].Value = dgAcPrompt.cbEnding.Checked ? 
                        dgAcPrompt.tbImageEnding.Text : ".jpg";
                    _gsSettings.Values["AddSubfolders"].Value = dgAcPrompt.cbSubFolders.Checked ? 
                        true.ToString() : false.ToString();
                }
            }

            if (_gsSettings.LoadedFromFile == false)
            {
                if (System.Windows.Forms.MessageBox.Show(@"Would you like to save the settings to a file as well?", @"save to file?", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _gsSettings.SaveToFile();
                }
            }

            return _gsSettings.Save();
        }

        private void FillAppSettingsDialog(AppSettingsDialog dgToLoad)
        {
            // Fill in the fields (also broken)
            dgToLoad.tbMSCon.Text = _gsSettings.Values["MSConnection"].Value;
            switch (ImageLocation)
            {
                // Images
                case BaseAppSetting.EnImageLocationType.File:
                    dgToLoad.rbFromFolder.Checked = true;
                    dgToLoad.tbFolderPath.Text = ImagePath;
                    break;
                case BaseAppSetting.EnImageLocationType.Web:
                    dgToLoad.rbFromWeb.Checked = true;
                    dgToLoad.tbWebPath.Text = ImagePath;
                    break;
                case BaseAppSetting.EnImageLocationType.NotSet:
                    dgToLoad.rbFromFolder.Checked = true;
                    dgToLoad.tbWebPath.Text = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            dgToLoad.tbImageEnding.Text = ImageEnding;
            dgToLoad.cbSubFolders.Checked = AddSubFolders;
            dgToLoad.btnLoad.Click += LoadFromFile;
        }
    
        /// <summary>
        /// Load all setting values
        /// </summary>
        /// <returns>Boolean that indicates if the settings saved successfully</returns>
        public bool Load()
        {
            return _gsSettings.Load();
        }

        /// <summary>
        /// Check if a Database Connection is working
        /// </summary>
        /// <returns>String "SUCCESS!" when successful or the error message if not.</returns>
        private static string TestDbCon(string strConToTest)
        {
            var testConn = new SqlConnection(strConToTest);
            try
            {
                testConn.Open();
                testConn.Close();
                return "SUCCESS!";
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Reformat a picture name based on stored settings
        /// </summary>
        /// <returns>A string that should point to a picture location</returns>
        public string GetImageLoc(string strImgPathNoExt)
        {
            char[] chrTrims = { '\\', '/' };
            var strReturnPath = strImgPathNoExt.Trim(chrTrims).Replace(@"/", @"\") + ImageEnding;

            if (AddSubFolders)
            {
                var strImageName = strReturnPath;
                var intLastSlash = strReturnPath.LastIndexOf(@"\", StringComparison.Ordinal);
                if (intLastSlash > -1)
                {
                    strImageName = strReturnPath.Substring(intLastSlash).Trim(chrTrims);
                }

                if (strImageName.Length > 2)
                {
                    strReturnPath = strImageName.Substring(0, 1) + @"\" + strImageName.Substring(1, 1) + @"\" + strImageName;
                }
            }

            switch (ImageLocation)
            {
                case BaseAppSetting.EnImageLocationType.File:
                    strReturnPath = System.IO.Path.Combine(ImagePath, strReturnPath);
                    break;
                case BaseAppSetting.EnImageLocationType.Web:
                {
                    var strWebPath = ImagePath;
                    if (strWebPath.EndsWith("/") == false)
                    {
                        strWebPath += "/";
                    }
                    var uriBase = new Uri(strWebPath);
                    var uriImage = new Uri(uriBase, strReturnPath.Replace(@"\", "/"));
                    strReturnPath = uriImage.ToString();
                    break;
                }
                case BaseAppSetting.EnImageLocationType.NotSet:
                default:
                    strReturnPath = string.Empty;
                    break;
            }

            return strReturnPath;
        }
    }
}