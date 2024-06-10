namespace PickToLight.Core.Models.Configurations {
	public class SpecialFunctionTwoConfiguration {
		public bool IsDefaultReservedOneEnabled { get; private set; } = false;
		public bool IsDefaultReservedTwoEnabled { get; private set; } = true;
		public bool IsDefaultReservedThreeEnabled { get; private set; } = false;
		public bool IsAliveIndicatorEnabled { get; set; } = true;
		public bool IsDefaultReservedFourEnabled { get; private set; } = true;
		public bool IsDefaultReservedFiveEnabled { get; private set; } = false;
		public bool IsDefaultReservedSixEnabled { get; private set; } = false;
		public static SpecialFunctionTwoConfiguration Default() {
			return new SpecialFunctionTwoConfiguration();
		}
		public  byte ToByte() {
			byte result = 0;
			result |= (byte)(IsDefaultReservedOneEnabled ? 1 : 0 << 0);
			result |= (byte)(IsDefaultReservedTwoEnabled ? 1 : 0 << 1);
			result |= (byte)(IsDefaultReservedThreeEnabled ? 1 : 0 << 2);
			result |= (byte)(IsAliveIndicatorEnabled ? 1 : 0 << 3);
			result |= (byte)(IsDefaultReservedFourEnabled ? 1 : 0 << 4);
			result |= (byte)(IsDefaultReservedFiveEnabled ? 1 : 0 << 5);
			result |= (byte)(IsDefaultReservedSixEnabled ? 1 : 0 << 6);
			return result;
		}
	}
}