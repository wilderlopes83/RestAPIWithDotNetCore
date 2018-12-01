using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace RestWithASPNetCoreUdemy.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key  {get;set;}
        public SigningCredentials SigningCredentials {get;}

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));

            }

            this.SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}