namespace PickToLight.Core.Models.Configurations {
	public class ValidDigitsConfiguration {
		public bool IsFirstDigitValid { get; set; } = true;
		public bool IsSecondDigitValid { get; set; } = true;
		public bool IsThirdDigitValid { get; set; } = true;
		public bool IsFourthDigitValid { get; set; } = true;
		public bool IsFifthDigitValid { get; set; } = true;
		public bool IsSixthDigitValid { get; set; } = true;
		public bool IsSeventhValid { get; set; } = false;
		public bool IsEighthValid { get; set; } = false;
		public static ValidDigitsConfiguration Default() {
			return new ValidDigitsConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(IsFirstDigitValid ? 0 : 1 << 0);
			result |= (byte)(IsSecondDigitValid ? 0 : 1 << 1);
			result |= (byte)(IsThirdDigitValid ? 0 : 1 << 2);
			result |= (byte)(IsFourthDigitValid ? 0 : 1 << 3);
			result |= (byte)(IsFifthDigitValid ? 0 : 1 << 4);
			result |= (byte)(IsSixthDigitValid ? 0 : 1 << 5);
			result |= (byte)(IsSeventhValid ? 0 : 1 << 6);
			result |= (byte)(IsEighthValid ? 0 : 1 << 7);
			return result;
		}
	}
}