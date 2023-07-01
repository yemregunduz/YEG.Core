using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yeg.Utilities.Helpers;

namespace YEG.Utilities.Test.Helpers
{
    public class SecurityKeyHelperTests
    {
        [Fact]
        public void CreateSecurityKey_ReturnsValidSecurityKey()
        {
            string securityKey = "your_security_key_here";
            var result = SecurityKeyHelper.CreateSecurityKey(securityKey);

            Assert.NotNull(result);
            Assert.IsType<SymmetricSecurityKey>(result);
            Assert.Equal(securityKey, Encoding.UTF8.GetString(((SymmetricSecurityKey)result).Key));
        }
        [Fact]
        public void CreateSecurityKey_WithNullSecurityKey_ThrowsArgumentNullException()
        {
            string securityKey = null;
            Assert.Throws<ArgumentNullException>(() => SecurityKeyHelper.CreateSecurityKey(securityKey));
        }
    }
}
