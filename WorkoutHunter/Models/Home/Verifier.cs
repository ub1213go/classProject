using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Models.Home
{
    public class Verifier
    {
        public byte[] createSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string createHash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public string UID()
        {
            List<string> ch = new List<string>();

            Random r = new Random();

            string id = "";

            // 產生A-Z a-z 0-9的 字串List
            for (int i = 48 ; i < 123 ; i++)
            {
                if ((i >= 58 && i < 65) || (i >= 91 && i < 97))
                {
                    continue;
                }
                ch.Add(Convert.ToChar(i).ToString());
            }

            // 產生UID
            for (int i = 0 ; i < 10 ; i++)
            {
                id += ch[r.Next(0, 62)];
            }

            return id;
        }

    }
}
