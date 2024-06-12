namespace PickToLight.Core.Models.Configurations {
	using PickToLight.Core.Models.Enums;
	public class SpecialFunctionOneConfiguration {
		#region Properties
		public bool IsCompletionMarkDisplayingEnabled { get; set; } = false;
		public bool IsCycleEditFunctionEnabled { get; set; } = false;
		public bool IsDeviceSelfTestingFunctionEnabled { get; set; } = true;
		public bool IsDummyKeyEnabled { get; set; } = false;
		public bool IsStockModeQuickCompilationEnabled { get; set; } = false;
		public PickTagMode PickTagMode { get; set; } = PickTagMode.Picking;
		#endregion
		#region Methods
		public static SpecialFunctionOneConfiguration Default() {
			return new SpecialFunctionOneConfiguration();
		}
		public byte ToByte() {
			byte result = 0;
			result |= (byte)(PickTagMode == PickTagMode.Picking ? 0 : 1 << 0);
			result |= (byte)(IsStockModeQuickCompilationEnabled ? 1 << 1 : 0);
			result |= (byte)(IsCompletionMarkDisplayingEnabled ? 1 << 2 : 0);
			result |= (byte)(IsDeviceSelfTestingFunctionEnabled ? 1 << 3 : 0);
			result |= (byte)(IsCycleEditFunctionEnabled ? 1 << 4 : 0);
			result |= (byte)(IsDummyKeyEnabled ? 1 << 5 : 0);
			return result;
		}
		#endregion
	}
}