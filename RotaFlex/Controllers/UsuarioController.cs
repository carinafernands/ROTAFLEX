using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace RotaFlex.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string? PasswordHash { get; private set; }
        public byte[]? Salt { get; private set; }

        public Usuario()
        {
        }

        public Usuario(string nome, string email, string cpf, string estado, string cidade)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Estado = estado;
            Cidade = cidade;
        }

        public void GerarHash(string senha)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            PasswordHash = hashed;
            Salt = salt;
        }

        public static bool VerificarSenha(string senhaDigitada, string hashSalvo, byte[]? saltSalvo)
        {
            string hashDigitado = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senhaDigitada,
                salt: saltSalvo,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashDigitado == hashSalvo;
        }
    }
}