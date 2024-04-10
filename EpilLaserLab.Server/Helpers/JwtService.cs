using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EpilLaserLab.Server.Helpers
{
    public class JwtService
    {
        private string secureKey = "X^7DGS9tG1-jVjaPp#PX65cJfFrb$AZ0ttx@9GbZyOd4WtggW$t4Ej" +
            "$P2f0I8hI$1Vav99Rsksir!*suk2mPjns+-Tl5@0+om2E@dTEaQFwUAQj8nFLzZk%oy!kSAUYFpkI" +
            "b%mZ^8h4q^#+*WUEqP1FJDt3$HDMZH*tSWQQR*R4jJM5&3Sc9Lts@DYZ^mY-DdWPPb3wNgXcr0mSg" +
            "S%kse5Ng5rrOScD42FDsZiNe-a*cczxy-Yjc!a9xt&87b^PI";
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(30));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            { 
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken) validatedToken;
        }
    }
}
