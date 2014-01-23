using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Data.Security
{
	public static class SaltedHash
	{
		public static byte[] GenerateSalt()
		{
			var rng = new RNGCryptoServiceProvider();
			const int minSaltSize = 16;
			const int maxSaltSize = 32;
			var random = new Random();
			var saltSize = random.Next(minSaltSize, maxSaltSize);

			var byteArray = new byte[saltSize];

			rng.GetBytes(byteArray);

			return byteArray;
		}

		public static byte[] GenerateSaltedHash(string passwordPlainText, byte[] saltBytes)
		{
			var clearTextBytes = Encoding.UTF8.GetBytes(passwordPlainText);

			var clearTextWithSaltBytes = new byte[clearTextBytes.Length + saltBytes.Length];

			for (var i = 0; i < clearTextBytes.Length; i++)
			{
				clearTextWithSaltBytes[i] = clearTextBytes[i];
			}

			for (var i = 0; i < saltBytes.Length; i++)
			{
				clearTextWithSaltBytes[clearTextBytes.Length + i] = saltBytes[i];
			}

			//Calculate the hash
			HashAlgorithm hash = new SHA256Managed();
			var hashBytes = hash.ComputeHash(clearTextWithSaltBytes);

			return hashBytes;
		}

		public static bool IsPasswordValid(string passwordPlainText, byte[] savedSaltBytes, byte[] savedHashBytes)
		{
			var array1 = GenerateSaltedHash(passwordPlainText, savedSaltBytes);
			var array2 = savedHashBytes;

			if (array1.Length != array2.Length)
				return false;

			for (var i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
					return false;
			}

			return true;
		}
	}
}
