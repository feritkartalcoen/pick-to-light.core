namespace PickToLight.Core.Models.Configurations {
	public class ValidDigitsConfiguration {
		#region Properties
		public bool IsEighthValid { get; set; } = false;
		public bool IsFifthDigitValid { get; set; } = true;
		public bool IsFirstDigitValid { get; set; } = true;
		public bool IsFourthDigitValid { get; set; } = true;
		public bool IsSecondDigitValid { get; set; } = true;
		public bool IsSeventhValid { get; set; } = false;
		public bool IsSixthDigitValid { get; set; } = true;
		public bool IsThirdDigitValid { get; set; } = true;
		#endregion
		#region Methods
		public static ValidDigitsConfiguration Default() {
			return new ValidDigitsConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(IsFirstDigitValid ? 0 << 0 : 1 << 0);
			result |= (byte)(IsSecondDigitValid ? 0 << 1 : 1 << 1);
			result |= (byte)(IsThirdDigitValid ? 0 << 2 : 1 << 2);
			result |= (byte)(IsFourthDigitValid ? 0 << 3 : 1 << 3);
			result |= (byte)(IsFifthDigitValid ? 0 << 4 : 1 << 4);
			result |= (byte)(IsSixthDigitValid ? 0 << 5 : 1 << 5);
			result |= (byte)(IsSeventhValid ? 0 << 6 : 1 << 6);
			result |= (byte)(IsEighthValid ? 0 << 7 : 1 << 7);
			return result;
		}
		#endregion
	}
}