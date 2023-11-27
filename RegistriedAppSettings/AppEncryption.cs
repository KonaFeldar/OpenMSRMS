using System;
using System.IO;
using System.Security.Cryptography;

namespace RegistriedAppSettings
{
    /// <summary>
    /// AES encryption for program global variables
    /// </summary>
    public class AppEncryption : IDisposable
    {
        private readonly Aes _encAes = Aes.Create();
        private bool _disposed;
    
        /// <summary>
        /// Encryption method for sensitive data before storing in a file or the registry
        /// </summary>
        /// <param name="key">The password salt to add to the encrypted string (Hash String)</param>
        /// <param name="siv">A string value to also add to the encrypted string (Title or VariableName)</param>
        public AppEncryption(string key, string siv = "")
        {
            // Initialize the crypto provider.
            _encAes.Key = TruncateHash(key, _encAes.KeySize / 8);
            _encAes.IV = TruncateHash(siv, _encAes.BlockSize / 8);
            _disposed = false;
        }

        private static byte[] TruncateHash(string key, int length)
        {
            using (var sha1 = SHA256.Create())
            {
                // Hash the key. 
                var keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
                var hash = sha1.ComputeHash(keyBytes);

                // Truncate or pad the hash. 
                Array.Resize(ref hash, length);
                return hash;
            }
        }

        /// <summary>
        /// Encrypt a string for storage
        /// </summary>
        /// <param name="plaintext">The raw text you wish to encrypt</param>
        /// <returns>An encrypted string</returns>
        public string EncryptData(string plaintext)
        {
            // Convert the plaintext string to a byte array. 
            var plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream. 
            using (var ms = new MemoryStream())
            {
                // Create the encoder to write to the stream. 
                using (var encStream = new CryptoStream(ms, _encAes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Use the crypto stream to write the byte array to the stream.
                    encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
                    encStream.FlushFinalBlock();

                    // Convert the encrypted stream to a printable string. 
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypt a saved string
        /// </summary>
        /// <param name="encryptedText">The encrypted text you want to decrypt</param>
        /// <returns>A plain text string</returns>
        public string DecryptData(string encryptedText)
        {
            // Convert the encrypted text string to a byte array. 
            var encryptedBytes = Convert.FromBase64String(encryptedText);

            // Create the stream. 
            using (var ms = new MemoryStream())
            {
                // Create the decoder to write to the stream. 
                using (var decStream = new CryptoStream(ms, _encAes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Use the crypto stream to write the byte array to the stream.
                    decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    decStream.FlushFinalBlock();

                    // Convert the plaintext stream to a string. 
                    return System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }

        private void ReleaseUnmanagedResources()
        {
            // TO-DO release unmanaged resources here
            // I think this is optional, but probably a good practice to include it if you add unmanaged resources later
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();

            if (!_disposed)
            {
                //We want to dispose the AES every time, yes? Disposed manually or by garbage collector 
                _encAes.Dispose();
                _disposed = true;
            }
            if (disposing)
            {
                //Apparently this shouldn't go here, but then why have a disposing boolean?
                //GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            //Manual dispose, this is the standard way to dispose of resources 
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AppEncryption()
        {
            //aka Finalize, this gets called by the garbage collector if we forget to manually dispose
            Dispose(false);
        }
    }
}