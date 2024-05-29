using System.Text;
namespace PickToLight.Core.Utilities.Converters {
    public partial class ValueConverter {
        public static byte[] StringDisplayValueToBytes(string value) {
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
    }
}