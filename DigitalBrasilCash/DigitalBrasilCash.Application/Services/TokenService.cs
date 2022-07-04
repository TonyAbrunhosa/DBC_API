using DigitalBrasilCash.Domain.Command;
using DigitalBrasilCash.Domain.Contracts.Command;
using DigitalBrasilCash.Domain.Contracts.Services;
using DigitalBrasilCash.Domain.Token.Input;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Application.Services
{
    public class TokenService : ITokenService
    {
        public async Task<ICommandResult> ObterToken(TokenInput input)
        {
            return await Task.Run(() =>
            {
                return new CommandResult(true, "Usuario encontrado com sucesso", GeraToken("Teste-API", "teste@12345"));
            });
            
        }
        private object GeraToken(string name, string password)
        {
            DateTime DataExpiracao = DateTime.Now.AddHours(6);
            var claims = new[] { new Claim(ClaimTypes.Name, name)};

            var creds = new SigningCredentials(Loadkey(), SecurityAlgorithms.HmacSha256Signature);

            var jwtoken = new JwtSecurityToken(
             issuer: "DigitalBrasilCash",
             audience: "DigitalBrasilCash",
             claims: claims,
             expires: DataExpiracao,
             signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtoken);

            return new
            {
                Token = token,
                DataCriacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DataExp = DataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                NomeUsuario = name,
                LoginUsuario = name
            };
        }
        private byte[] GenerateKey(int bytes)
        {
            RandomNumberGenerator Rng = RandomNumberGenerator.Create();
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }

        private SecurityKey Loadkey()
        {
            string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "supersecretkey.json");

            if (File.Exists(MyJwkLocation))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));

            var newKey = CreateJWK();
            File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }
    }
}
