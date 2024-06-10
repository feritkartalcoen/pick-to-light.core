namespace PickToLight.Core.Models.Configurations {
	public class PickTagModeConfiguration {
		public bool IsConfirmationButtonEnabled { get; set; } = true;
		public bool IsShortageButtonEnabled { get; set; } = true;
		public bool IsUpDownCountEnabled { get; set; } = false;
		public bool IsKeyCodeReturnEnabled { get; set; } = false;
		public bool IsDisplayingCommunicationTimeoutEnabled { get; set; } = true;
		public bool IsNodeAddressConfigurationEnabled { get; set; } = true;
		public bool IsRedisplayingEnabled { get; set; } = true;
		public static PickTagModeConfiguration Default() {
			return new PickTagModeConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(IsConfirmationButtonEnabled ? 1 : 0 << 0);
			result |= (byte)(IsShortageButtonEnabled ? 1 : 0 << 1);
			result |= (byte)(IsUpDownCountEnabled ? 1 : 0 << 2);
			result |= (byte)(IsKeyCodeReturnEnabled ? 1 : 0 << 3);
			result |= (byte)(IsDisplayingCommunicationTimeoutEnabled ? 1 : 0 << 4);
			result |= (byte)(IsNodeAddressConfigurationEnabled ? 1 : 0 << 5);
			result |= (byte)(IsRedisplayingEnabled ? 1 : 0 << 6);
			return result;
		}
	}
}