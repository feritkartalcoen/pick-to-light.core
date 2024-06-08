namespace PickToLight.Core.Models {
	public class PickTag {
		public bool IsConnected { get; set; } = false;
		public int NodeAddress { get; set; }
		public string Value { get; set; } = string.Empty;
	}
}
