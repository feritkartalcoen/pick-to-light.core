using PickToLight.Core.Models.Enums;
namespace PickToLight.Core.Models.Configurations {
	public class SpecialFunctionOneConfiguration {
		public PickTagMode PickTagMode { get; set; } = PickTagMode.Picking;
		public bool IsStockModeQuickCompilationEnabled { get; set; } = false;
		public bool IsCompletionMarkDisplayingEnabled { get; set; } = false;
		public bool IsDeviceSelfTestingFunctionEnabled { get; set; } = true;
		public bool IsCycleEditFunctionEnabled { get; set; } = false;
		public bool IsDummyKeyEnabled { get; set; } = false;
		public static SpecialFunctionOneConfiguration Default() {
			return new SpecialFunctionOneConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(PickTagMode == PickTagMode.Picking ? 0 : 1 << 0);
			result |= (byte)(IsStockModeQuickCompilationEnabled ? 1 : 0 << 1);
			result |= (byte)(IsCompletionMarkDisplayingEnabled ? 1 : 0 << 2);
			result |= (byte)(IsDeviceSelfTestingFunctionEnabled ? 1 : 0 << 3);
			result |= (byte)(IsCycleEditFunctionEnabled ? 1 : 0 << 4);
			result |= (byte)(IsDummyKeyEnabled ? 1 : 0 << 5);
			return result;
		}
	}
}