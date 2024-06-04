namespace PickToLight.Core.Utilities.Converters {
    public class CCBConverter {
        public static byte[] ToByte(string bytesString) {
            string[] strings = bytesString.Split(" ");
            byte[] bytes = new byte[strings.Length];
            for (int i = 0; i < strings.Length; i++) {
                bytes[i] = Convert.ToByte(strings[i], 16);
            }
            return bytes;
        }
        public static string ToString(byte[] bytes) {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }
    }
}