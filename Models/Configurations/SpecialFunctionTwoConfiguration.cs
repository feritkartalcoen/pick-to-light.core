namespace PickToLight.Core.Models.Configurations {
	public class SpecialFunctionTwoConfiguration {
		#region Properties
		public bool IsAliveIndicatorEnabled { get; set; } = true;
		public bool IsDefaultReservedFiveEnabled { get; private set; } = false;
		public bool IsDefaultReservedFourEnabled { get; private set; } = true;
		public bool IsDefaultReservedOneEnabled { get; private set; } = false;
		public bool IsDefaultReservedSixEnabled { get; private set; } = false;
		public bool IsDefaultReservedThreeEnabled { get; private set; } = false;
		public bool IsDefaultReservedTwoEnabled { get; private set; } = true;
		#endregion
		#region Methods
		public static SpecialFunctionTwoConfiguration Default() {
			return new SpecialFunctionTwoConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(IsDefaultReservedOneEnabled ? 1 << 0 : 0);
			result |= (byte)(IsDefaultReservedTwoEnabled ? 1 << 1 : 0);
			result |= (byte)(IsDefaultReservedThreeEnabled ? 1 << 2 : 0);
			result |= (byte)(IsAliveIndicatorEnabled ? 1 << 3 : 0);
			result |= (byte)(IsDefaultReservedFourEnabled ? 1 << 4 : 0);
			result |= (byte)(IsDefaultReservedFiveEnabled ? 1 << 5 : 0);
			result |= (byte)(IsDefaultReservedSixEnabled ? 1 << 6 : 0);
			return result;
		}
		#endregion
	}
}