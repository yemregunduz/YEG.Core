using System.Security.Cryptography;
using System.Text;

namespace Yeg.Utilities.Helpers
{
    /// <summary>
    /// Provides encryption and decryption methods using a symmetric encryption algorithm.
    /// </summary>
    public class CryptographyHelper
    {
        /// <summary>
        /// Encrypts the specified plain text using the provided encryption key and symmetric encryption algorithm.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <param name="algorithm">The symmetric encryption algorithm.</param>
        /// <param name="autoCompleteForKey">Optional. Specifies whether to auto-complete the key size if it does not match the desired key size of the algorithm. Default is false.</param>
        /// <returns>The encrypted text.</returns>
        /// <exception cref="ArgumentException">Thrown when the plain text or encryption key is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the encryption algorithm is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the algorithm is not a symmetric encryption algorithm.</exception>
        /// <exception cref="ArgumentException">Thrown when the key size does not match the desired key size of the algorithm, and auto-completion is not enabled.</exception>
        public static string Encrypt(string plainText, string key, SymmetricAlgorithm algorithm,bool autoCompleteForKey = false)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("Plain text cannot be null or empty.", nameof(plainText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Encryption key cannot be null or empty.", nameof(key));

            if (algorithm is null)
                throw new ArgumentNullException(nameof(algorithm));

            if (!IsKeySizeValid(key, algorithm.KeySize))
            {
                if (autoCompleteForKey)
                    key = AutoCompleteKeySize(key, algorithm.KeySize);
                else
                    throw new ArgumentException($"Algorithm key size must be {algorithm.KeySize}", nameof(algorithm));
            }


            byte[] encryptedBytes;

            using (algorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key);
                algorithm.Mode = CipherMode.CBC;
                algorithm.Padding = PaddingMode.PKCS7;

                var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    ms.Write(algorithm.IV, 0, algorithm.IV.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Decrypts the specified encrypted text using the provided encryption key and symmetric encryption algorithm.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <param name="key">The encryption key.</param>
        /// <param name="algorithm">The symmetric encryption algorithm.</param>
        /// <param name="autoCompleteForKey">Optional. Specifies whether to auto-complete the key size if it does not match the desired key size of the algorithm. Default is false.</param>
        /// <returns>The decrypted text.</returns>
        /// <exception cref="ArgumentException">Thrown when the encrypted text or encryption key is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the encryption algorithm is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the algorithm is not a symmetric encryption algorithm.</exception>
        /// <exception cref="ArgumentException">Thrown when the key size does not match the desired key size of the algorithm, and auto-completion is not enabled.</exception>
        public static string Decrypt(string encryptedText, string key, SymmetricAlgorithm algorithm,bool autoCompleteForKey = false)
        {
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentException("Encrypted text cannot be null or empty.", nameof(encryptedText));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Encryption key cannot be null or empty.", nameof(key));

            if (algorithm is null)
                throw new ArgumentNullException(nameof(algorithm));

            if (!IsKeySizeValid(key, algorithm.KeySize))
            {
                if (autoCompleteForKey)
                    key = AutoCompleteKeySize(key, algorithm.KeySize);
                else
                    throw new ArgumentException($"Algorithm key size must be {algorithm.KeySize}", nameof(algorithm));
            }

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] iv = new byte[algorithm.BlockSize / 8];
            byte[] cipherBytes = new byte[encryptedBytes.Length - iv.Length];

            Array.Copy(encryptedBytes, iv, iv.Length);
            Array.Copy(encryptedBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

            string decryptedText;

            using (algorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(key);
                algorithm.IV = iv;
                algorithm.Mode = CipherMode.CBC;
                algorithm.Padding = PaddingMode.PKCS7;

                var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using (var ms = new System.IO.MemoryStream(cipherBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new System.IO.StreamReader(cs, Encoding.UTF8))
                {
                    decryptedText = reader.ReadToEnd();
                }
            }

            return decryptedText;
        }

        /// <summary>
        /// Checks whether the key size is valid for the specified algorithm.
        /// </summary>
        /// <param name="key">The encryption key.</param>
        /// <param name="desiredKeySize">The desired key size in bits.</param>
        /// <returns><c>true</c> if the key size is valid; otherwise, <c>false</c>.</returns>
        private static bool IsKeySizeValid(string key, int desiredKeySize)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            return keyBytes.Length == desiredKeySize / 8;
        }

        /// <summary>
        /// Auto-completes or trims the key to match the desired key size.
        /// </summary>
        /// <param name="key">The encryption key.</param>
        /// <param name="desiredKeySize">The desired key size in bits.</param>
        /// <returns>The updated key with the desired key size.</returns>
        private static string AutoCompleteKeySize(string key, int desiredKeySize)
        {
            int keySizeInBytes = desiredKeySize / 8;

            if (key.Length > keySizeInBytes)
                return key.Substring(0, keySizeInBytes);
            else if (key.Length < keySizeInBytes)
                return key.PadRight(keySizeInBytes, '0');
            else
                return key;
        }
    }
}
