namespace PickToLight.Core.Models.Configurations {
	public class PickTagModeConfiguration {
		#region Properties
		public bool IsConfirmationButtonEnabled { get; set; } = true;
		public bool IsDisplayingCommunicationTimeoutEnabled { get; set; } = true;
		public bool IsKeyCodeReturnEnabled { get; set; } = false;
		public bool IsNodeAddressConfigurationEnabled { get; set; } = true;
		public bool IsRedisplayingEnabled { get; set; } = true;
		public bool IsShortageButtonEnabled { get; set; } = true;
		public bool IsUpDownCountEnabled { get; set; } = false;
		#endregion
		#region Methods
		public static PickTagModeConfiguration Default() {
			return new PickTagModeConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(IsConfirmationButtonEnabled ? 1 << 0 : 0);
			result |= (byte)(IsShortageButtonEnabled ? 1 << 1 : 0);
			result |= (byte)(IsUpDownCountEnabled ? 1 << 2 : 0);
			result |= (byte)(IsKeyCodeReturnEnabled ? 1 << 3 : 0);
			result |= (byte)(IsDisplayingCommunicationTimeoutEnabled ? 1 << 4 : 0);
			result |= (byte)(IsNodeAddressConfigurationEnabled ? 1 << 5 : 0);
			result |= (byte)(IsRedisplayingEnabled ? 1 << 6 : 0);
			return result;
		}
		#endregion
	}
}