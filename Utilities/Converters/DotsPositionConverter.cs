using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PickToLight.Core.Utilities.Converters {
	public class DotsPositionConverter {
		public static byte ToByte(string dotsPosition) {
			byte result = 0;
			List<char> dotsPositionChars = [.. dotsPosition.ToCharArray()];
			dotsPositionChars.Reverse();
			for (int i = 0; i < dotsPositionChars.Count; i++) {
				result |= (byte)(int.Parse(dotsPositionChars[i].ToString()) << i);
			}
			return result;
		}
		public static string ToString(byte value) {
			char[] dotsPositionChars = new char[6];
			for (int i = 0; i < 6; i++) {
				dotsPositionChars[i] = ((value & (1 << i)) != 0) ? '1' : '0';
			}
			Array.Reverse(dotsPositionChars);
			return new string(dotsPositionChars);
		}
	}
}