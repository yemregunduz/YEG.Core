using System.Security.Cryptography;
using Yeg.Utilities.Helpers;
using Yeg.Utilities.Options;

namespace YEG.Utilities.Test
{
    public class CryptographyHelperTests
    {

        [Fact]
        public void Constructor_Should_Throw_Exception_On_Null_Options()
        {
            CryptographyHelperOptions options = null;
            Assert.Throws<ArgumentNullException>(() => new CryptographyHelper(options));
        }

        [Fact]
        public void Encrypt_And_Decrypt_Text()
        {
            var symmetricAlgorithm = Aes.Create();
            var securityKey = "mysupersecretkey";
            var plainText = "Hello, World!";
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            var encryptedText = helper.Encrypt(plainText);
            var decryptedText = helper.Decrypt(encryptedText);

            Assert.NotEqual(plainText, encryptedText);
            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void Verify_Encrypted_Text()
        {
            var symmetricAlgorithm = Aes.Create();
            var securityKey = "mysupersecretkey";
            var plainText = "Hello, World!";
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            var encryptedText = helper.Encrypt(plainText);
            var isValid = helper.VerifyEncyrptedText(plainText, encryptedText);

            Assert.True(isValid);
        }

        [Fact]
        public void Encrypt_And_Decrypt_Empty_Text()
        {
            var symmetricAlgorithm = Aes.Create(); 
            var securityKey = "mysupersecretkey";
            var plainText = "";
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            var encryptedText = helper.Encrypt(plainText);
            var decryptedText = helper.Decrypt(encryptedText);

            Assert.NotEqual(plainText, encryptedText);
            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void Verify_Encrypted_Empty_Text()
        {
            var symmetricAlgorithm = Aes.Create();
            var securityKey = "mysupersecretkey";
            var plainText = "";
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            var encryptedText = helper.Encrypt(plainText);
            var isValid = helper.VerifyEncyrptedText(plainText, encryptedText);

            Assert.True(isValid);
        }

        [Fact]
        public void Encrypt_And_Decrypt_Null_Text()
        {
            // Arrange
            var symmetricAlgorithm = Aes.Create();
            var securityKey = "mysupersecretkey";
            string plainText = null;
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                var encryptedText = helper.Encrypt(plainText);
                var decryptedText = helper.Decrypt(encryptedText);

            });
        }

        [Fact]
        public void Verify_Encrypted_Null_Text()
        {
            var symmetricAlgorithm = Aes.Create();
            var securityKey = "mysupersecretkey";
            string plainText = null;
            var helper = CryptographyHelper.CreateInstance(symmetricAlgorithm, securityKey);

            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var encryptedText = helper.Encrypt(plainText);
                var isValid = helper.VerifyEncyrptedText(plainText, encryptedText);
            });

            Assert.Equal("plainText", exception.ParamName);
        }
        [Fact]
        public void Encrypt_Decrypt_ReturnsOriginalText()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void VerifyEncyrptedText_ReturnsTrueForValidEncryption()
        {
            // Arrange
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            bool isValid = cryptographyHelper.VerifyEncyrptedText(plainText, encryptedText);

            Assert.True(isValid);
        }

        [Fact]
        public void VerifyEncyrptedText_ReturnsFalseForInvalidEncryption()
        {
            string plainText = "This is a test message.";
            string anotherPlainText = "This is a different message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            bool isValid = cryptographyHelper.VerifyEncyrptedText(anotherPlainText, encryptedText);

            Assert.False(isValid);
        }

        [Fact]
        public void AutoCompleteSecurityKey_CompletesKeyWhenSizeIsInsufficient()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider(),
                securityKey: "mykey",
                autoCompleteForSecurityKey: true
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ParameterConstructor_ThrowsExceptionWhenSizeIsInsufficientAndAutoCompleteFalse()
        {

            string plainText = "This is a test message.";

            Assert.Throws<ArgumentException>(() =>
            {
                CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                    symmetricAlgorithm: new AesCryptoServiceProvider(),
                    securityKey: "mykey",
                    autoCompleteForSecurityKey: false
                );
            });
        }

        [Fact]
        public void ValidateEncryptionAlgorithm()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new RijndaelManaged(),
                securityKey: "mykey"
            );

            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ValidateEncryptionMode()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider(),
                securityKey: "mykey"
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ValidatePaddingMode()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider(),
                securityKey: "mykey"
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ValidateEncryptionAlgorithmKeySize()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider { KeySize = 128 },
                securityKey: "mykey",
                autoCompleteForSecurityKey: true
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ValidateEncryptionAlgorithmKeySize_ExceedsMaximumSize()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider { KeySize = 256 },
                securityKey: "mykey"
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);
            Assert.Equal(plainText, decryptedText);
        }

        [Fact]
        public void ValidateEncryptionSecurityKeySize_LongerThanExpected()
        {
            string plainText = "This is a test message.";

            CryptographyHelper cryptographyHelper = CryptographyHelper.CreateInstance(
                symmetricAlgorithm: new AesCryptoServiceProvider { KeySize = 256 },
                securityKey: "BucokuzunbirsecuritykeyolacakmaalesefBucokuzunbirsecuritykeyolacakmaalesefBucokuzunbirsecuritykeyolacakmaalesef"
            );
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);

            // Assert
            Assert.Equal(plainText, decryptedText);
        }
    }
}
