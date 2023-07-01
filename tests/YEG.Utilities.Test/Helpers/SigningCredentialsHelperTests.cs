using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yeg.Utilities.Helpers;

namespace YEG.Utilities.Test.Helpers
{
    public class SigningCredentialsHelperTests
    {
        [Fact]
        public void CreateSigningCredentials_WithNullSecurityKey_ThrowsArgumentNullException()
        {
            SecurityKey securityKey = null;
            Assert.Throws<ArgumentNullException>(() => SigningCredentialsHelper.CreateSigningCredentials(securityKey));
        }
        [Fact]
        public void CreateSigningCredentials_ReturnsValidSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_security_key_here"));
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            Assert.NotNull(signingCredentials);
            Assert.Equal(securityKey, signingCredentials.Key);
            Assert.Equal(SecurityAlgorithms.HmacSha512Signature, signingCredentials.Algorithm);
        }

    }
}
