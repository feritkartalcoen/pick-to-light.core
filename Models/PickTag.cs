using System.Collections.Generic;
namespace PickToLight.Core.Models {
	public class PickTag {
		public bool IsConnected { get; set; } = false;
		public int NodeAddress { get; set; }
		public string Value { get; set; } = string.Empty;
		public static List<PickTag> PickTags(int pickTagsCount) {
			List<PickTag> pickTags = [];
			for (int i = 0; i < pickTagsCount; i++) {
				pickTags.Add(new() {
					NodeAddress = i + 1
				});
			}
			return pickTags;
		}
	}
}