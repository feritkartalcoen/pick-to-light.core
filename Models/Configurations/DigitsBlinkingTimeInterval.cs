namespace PickToLight.Core.Models.Configurations {
	using PickToLight.Core.Models.Enums;
	using System.Collections.Generic;
	public class DigitsBlinkingTimeInterval {
		#region Properties
		public BlinkingTimeInterval FifthDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public BlinkingTimeInterval FirstDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public BlinkingTimeInterval FourthDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public BlinkingTimeInterval SecondDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public BlinkingTimeInterval SixthDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public BlinkingTimeInterval ThirdDigitBlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		#endregion
		#region Methods
		public static DigitsBlinkingTimeInterval Default() {
			return new();
		}
		#endregion
		public List<byte> ToBytes() {
			byte firstByte = (byte)(((int)FifthDigitBlinkingTimeInterval << 4) | (int)SixthDigitBlinkingTimeInterval);
			byte secondByte = (byte)(((int)ThirdDigitBlinkingTimeInterval << 4) | (int)FourthDigitBlinkingTimeInterval);
			byte thirdByte = (byte)(((int)FirstDigitBlinkingTimeInterval << 4) | (int)SecondDigitBlinkingTimeInterval);
			return [firstByte, secondByte, thirdByte];
		}
	}
}