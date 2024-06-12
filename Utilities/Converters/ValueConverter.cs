namespace PickToLight.Core.Utilities.Converters {
	using System;
	using System.Text;
	public class ValueConverter {
		#region Methods
		public static byte[] ToBytes(string value) {
			byte[] bytes = new byte[6];
			byte[] inputBytes = Encoding.ASCII.GetBytes(value);
			int startIndex = 6 - inputBytes.Length;
			for (int i = 0; i < inputBytes.Length; i++) {
				bytes[startIndex + i] = inputBytes[i];
			}
			return bytes;
		}
		public static string ToString(byte[] value) {
			int startIndex = 0;
			while (startIndex < 6 && value[startIndex] == 0) {
				startIndex++;
			}
			byte[] trimmedBytes = new byte[6 - startIndex];
			Array.Copy(value, startIndex, trimmedBytes, 0, 6 - startIndex);
			return Encoding.ASCII.GetString(trimmedBytes);
		}
		#endregion
	}
}