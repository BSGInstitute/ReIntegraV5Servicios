using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICryptoService
    {
        string GenerateSalt(int byteLength = 16);
        string Hash(string input, string algorithm = "sha256");
        string Hash(byte[] input, string algorithm = "sha256");
        string SHA1(string input);
        string SHA256(string input);
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
