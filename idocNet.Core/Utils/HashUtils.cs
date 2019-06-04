using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace idocNet.Core.Utils
{

	internal class MyHash
	{
		const int ByteSize = 8;

		const int ByteMask = 0xff;

		public static char[] charset32 =

     {

         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',

         'I', 'J', 'K', 'L', 'M', 'N', 'X', 'P',

         'Q', 'R', 'S', 'T', 'U', 'V', 'W', '1',

         '2', '3', '4', '5', '6', '7', '8', '9'

     };







		/// <summary>

		/// Given a source string, and length of desired string,

		/// returns an almost perfect hash (even distribution).

		/// hashLength should ideally be 64 for 16 symbols,

		/// 52 for 32 symbols, or 43 for 64-symbols. However 

		/// smaller encoded strings can be used depending upon

		/// application and number of strings being hashed.

		/// </summary>

		/// <param name="source">String to obtain hash for.</param>

		/// <param name="hashLength">Length of hash in characters.</param>

		/// <param name="charset">Symbols to use for encoding.</param>

		/// <returns>Hash as an encoded string.</returns>

		public static string NormalizedHash(

					string source,

					int hashLength,

					char[] charset)
		{

			byte[] cryptographicHash = CryptographicSha256OfUtf8(source);

			return TruncatedEncode(cryptographicHash, hashLength, charset);

		}



		/// <summary>

		/// Convert a string to UTF8 and calculate the SHA256

		/// cryptographic hash of that string. This ensures that

		/// a 256-bit hash of a unicode string is calculated in

		/// a very portable way. The resulting hash is encoded

		/// in a 32-byte (256 bit) array.

		/// </summary>

		/// <param name="source">Unicode string</param>

		/// <returns>256-bit hash of string</returns>

		private static byte[] CryptographicSha256OfUtf8(string source)
		{

			//

			// Wrap a more generic method.

			//

			using (HashAlgorithm hashAlg = new SHA256Managed())
			{

				return CryptographicHashOfString(hashAlg, source, Encoding.UTF8);

			}

		}



		/// <summary>

		/// Take a string, convert to byte sequence (using

		/// specified encoding). Then convert to a byte

		/// sequence using a cryptographic hash. The entropy

		/// of the string is evenly distributed throughout

		/// the resulting byte array.

		/// </summary>

		/// <param name="hashAlg">Hashing algorithm</param>

		/// <param name="source">Source string</param>

		/// <param name="encoding">Byte encoding</param>

		/// <returns>Hash as a byte array</returns>

		public static byte[] CryptographicHashOfString(

					HashAlgorithm hashAlg,

					string source,

					Encoding encoding)
		{

			Encoder encoder = encoding.GetEncoder();

			char[] sourceSequence = source.ToCharArray();

			byte[] workBuffer = new byte[256];

			int offset = 0;

			int charsUsed;

			int bytesUsed;

			bool completed;

			//

			// In each iteration of the loop, convert characters

			// to bytes and add those bytes to the cryptographic

			// hash. 

			//

			do
			{

				encoder.Convert(

					sourceSequence,

					offset, sourceSequence.Length - offset,

					workBuffer,

					0, workBuffer.Length,

					false,

					out charsUsed,

					out bytesUsed,

					out completed);

				hashAlg.TransformBlock(

					workBuffer, 0,

					bytesUsed,

					workBuffer, 0);

				offset += charsUsed;

			}

			while (!completed);

			//

			// This tells the hashing algorithm to calculate

			// the final hash.

			//

			hashAlg.TransformFinalBlock(workBuffer, 0, 0);

			return hashAlg.Hash;

		}



		/// <summary>

		/// Calculate the shift value (number of bits encoded)

		/// for a given character set.

		/// </summary>

		/// <param name="setSize">

		/// Number of characters in character set

		/// </param>

		/// <returns>

		/// Number of bits that is encoded by

		/// the character set

		/// </returns>

		public static int CalcShiftSize(int setSize)
		{

			if (setSize <= 0)
			{

				throw new ArgumentOutOfRangeException(

						"setSize must be > 0"

						);

			}

			//

			// This is effectively a log2(n) for small n.

			//

			int shiftSize = 0;

			int v = 1;

			while (setSize != v)
			{

				if (setSize < v)
				{

					throw new ArgumentException(

							"setSize must be a positive power of 2"

							);

				}

				v += v;

				shiftSize++;

			}

			return shiftSize;

		}



		/// <summary>

		/// Take a byte sequence (say a 256-bit hash) and

		/// encode to a string using the specified character

		/// set. To keep things simple, the character set

		/// must contain a power of 2 symbols (16, 32, etc)

		/// </summary>

		/// <param name="data">

		/// Byte sequence to encode

		/// </param>

		/// <param name="encodeLength">

		/// Number of characters to return

		/// </param>

		/// <param name="charset">

		/// Array of symbols to use

		/// </param>

		/// <returns>Encoded string</returns>

		public static string TruncatedEncode(

				byte[] data,

				int encodeLength,

				char[] charset)
		{

			if (encodeLength < 1)
			{

				throw new ArgumentOutOfRangeException(

					"hashLength should be 1 or more"

					);

			}

			if (charset.Length < 2)
			{

				throw new ArgumentException(

					"charset needs to contain at least 2 characters"

					);

			}

			int shiftSize;

			try
			{

				shiftSize = CalcShiftSize(charset.Length);

			}

			catch (ArgumentException e)
			{

				throw new ArgumentException(

					"charset needs to be a power of 2", e

					);

			}

			//

			// Assume the byte sequence 0, 1, 2, 3

			// these are treated as a bit sequence:

			// ... 33333333 22222222 11111111 00000000

			// that maps to the final output sequence:

			// ... GGFFFFFE EEEEDDDD DCCCCCBB BBBAAAAA

			// where A is the first character, 

			// B is the second, etc.

			//

			int hashPos = 0;

			int accumulator = 0;

			int shiftFactor = 0;

			int mask = (1 << shiftSize) - 1;

			char[] buffer = new char[encodeLength];

			for (int charPos = 0;

					charPos < buffer.Length;

					charPos++)
			{

				if (shiftFactor < shiftSize)
				{

					if (hashPos < data.Length)
					{

						accumulator +=

							(int)data[hashPos++] << shiftFactor;

					}

					shiftFactor += ByteSize;

				}

				int offset = accumulator & mask;

				shiftFactor -= shiftSize;

				accumulator >>= shiftSize;

				buffer[charPos] = charset[offset];

			}

			return new string(buffer);

		}




		/// <summary>

		/// Reverse of TruncatedEncode to be used for validation.

		/// Can only be used where the number of encoded bits in

		/// encodedData is at last as many as the number of bits

		/// in decodedData. This exists because only a foolish

		/// person would trust TruncatedEncode() at face value.

		/// </summary>

		/// <param name="decodedData">

		/// Array to fill with decoded data.

		/// </param>

		/// <param name="encodedData">

		/// Result from TruncatedEncode.

		/// </param>

		/// <param name="charset">

		/// As passed into TruncatedEncode

		/// </param>

		public static void ValidationDecode(

				byte[] decodedData,

				string encodedData,

				char[] charset)
		{

			Array.Clear(decodedData, 0, decodedData.Length);

			if (charset.Length < 2)
			{

				throw new ArgumentException(

					"charset needs to contain at least 2 characters"

					);

			}

			int shiftSize;

			try
			{

				shiftSize = CalcShiftSize(charset.Length);

			}

			catch (ArgumentException e)
			{

				throw new ArgumentException(

					"charset needs to be a power of 2", e

					);

			}

			int decPos = 0;

			int accumulator = 0;

			int shiftFactor = 0;

			int mask = (1 << shiftSize) - 1;

			foreach (char c in encodedData)
			{

				int offset = Array.IndexOf<char>(charset, c);

				if (offset < 0)
				{

					throw new ArgumentException(

						"Unexpected character in encoded string"

						);

				}

				accumulator += ((int)offset) << shiftFactor;

				shiftFactor += shiftSize;

				if (shiftFactor >= ByteSize)
				{

					if (decPos < decodedData.Length)
					{

						decodedData[decPos++] = (byte)(accumulator & ByteMask);

					}

					accumulator >>= ByteSize;

					shiftFactor -= ByteSize;

				}

			}

		}
	}
	public static class HashUtils
	{
		public static string HashCode(string code, string salt, int length)
		{
			if (string.IsNullOrEmpty(salt))
			{
				salt = "idocnetutils201405291404zzzz";
			}
			//MD5 md5 = MD5.Create();
			byte[] codeBytes = Encoding.ASCII.GetBytes(code + salt);
			//byte[] hash1 = md5.ComputeHash(codeBytes);

			SHA512 s = SHA512.Create();
			byte[] hash = s.ComputeHash(codeBytes);
			return MyHash.TruncatedEncode(hash, length, MyHash.charset32);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="format">xxxxc-xxcc-xxxxxc</param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string AddChecksum(this string source, string format, string salt = null)
		{
			format = format.ToLower();
			int cx = 0;
			int cc = 0;
			for (int i = 0; i < format.Length; ++i)
			{
				if (format[i] == 'x') cx++;
				if (format[i] == 'c') cc++;
			}

			source = source.Substring(0, cx);

			var hash = HashCode(source, salt, cc);
			int ix = 0;
			int ic = 0;

			StringBuilder ret = new StringBuilder();
			for (int i = 0; i < format.Length; ++i)
			{
				if (format[i] == 'x')
				{
					ret.Append(source[ix++]);
				}
				else if (format[i] == 'c')
				{
					ret.Append(hash[ic++]);
				}

				else
				{
					ret.Append(format[i]);
				}
			}

			return ret.ToString();
		}
		public static bool Checksum(this string val, string format, string salt = null)
		{

			if (val.Length != format.Length) return false;

			StringBuilder source = new StringBuilder();
			StringBuilder hash = new StringBuilder();
			format = format.ToLower();

			int cc = 0;
			for (int i = 0; i < format.Length; ++i)
			{
				if (format[i] == 'x') source.Append(val[i]);
				if (format[i] == 'c')
				{
					hash.Append(val[i]);
					cc++;
				}
			}

			var chash = HashCode(source.ToString(), salt, cc);


			return chash.Equals(hash.ToString());
		}



		public static string GetChecksumSource(this string val, string format, string salt = null)
		{

			if (val.Length != format.Length) return null;

			StringBuilder source = new StringBuilder();
			format = format.ToLower();

			for (int i = 0; i < format.Length; ++i)
			{
				if (format[i] == 'x') source.Append(val[i]);

			}

			return source.ToString();



		}




		public static string AddChecksum(this string source, int length, string salt = null)
		{
			var hash = HashCode(source, salt, length);
			return source + hash;
		}

		public static bool Checksum(this string val, int length, ref string source, string salt = null)
		{

			var hash = val.Substring(val.Length - length);


			source = val.Substring(0, val.Length - length);

			var chash = HashCode(source, salt, length);


			if (chash.Equals(hash))
			{
				source = val.Substring(0, val.Length - length);

				return true;
			}

			source = "";

			return false;
		}


	}
}
