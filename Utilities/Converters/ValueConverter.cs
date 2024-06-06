using System;
using System.Text;
namespace PickToLight.Core.Utilities.Converters {
    public partial class ValueConverter {
        public static byte[] ToBytes(string value) {
            if (value.Length > 6) {
                throw new ArgumentException("Input string can be at most 6 characters long.");
            }
            byte[] bytes = new byte[6];
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            int startIndex = 6 - inputBytes.Length;
            for (int i = 0; i < inputBytes.Length; i++) {
                bytes[startIndex + i] = inputBytes[i];
            }
            return bytes;
        }
        public static string ToString(byte[] bytes) {
            if (bytes.Length != 6) {
                throw new ArgumentException("Input byte array must be exactly 6 bytes long.");
            }
            int startIndex = 0;
            while (startIndex < 6 && bytes[startIndex] == 0) {
                startIndex++;
            }
            byte[] trimmedBytes = new byte[6 - startIndex];
            Array.Copy(bytes, startIndex, trimmedBytes, 0, 6 - startIndex);
            return Encoding.ASCII.GetString(trimmedBytes);
        }
    }
}