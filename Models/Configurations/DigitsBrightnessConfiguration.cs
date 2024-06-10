namespace PickToLight.Core.Models.Configurations {
	public class DigitsBrightnessConfiguration {
		public bool ShouldDimFirstDigitBrightness { get; set; } = false;
		public bool ShouldDimSecondDigitBrightness { get; set; } = false;
		public bool ShouldDimThirdDigitBrightness { get; set; } = false;
		public bool ShouldDimFourthDigitBrightness { get; set; } = false;
		public bool ShouldDimFifthDigitBrightness { get; set; } = false;
		public bool ShouldDimSixthDigitBrightness { get; set; } = false;
		public static DigitsBrightnessConfiguration Default() {
			return new DigitsBrightnessConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(ShouldDimFirstDigitBrightness ? 0 : 1 << 0);
			result |= (byte)(ShouldDimSecondDigitBrightness ? 0 : 1 << 1);
			result |= (byte)(ShouldDimThirdDigitBrightness ? 0 : 1 << 2);
			result |= (byte)(ShouldDimFourthDigitBrightness ? 0 : 1 << 3);
			result |= (byte)(ShouldDimFifthDigitBrightness ? 0 : 1 << 4);
			result |= (byte)(ShouldDimSixthDigitBrightness ? 0 : 1 << 5);
			return result;
		}
	}
}