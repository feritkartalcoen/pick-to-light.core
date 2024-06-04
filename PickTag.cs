using PickToLight.Core.Models.Enums;
namespace PickToLight.Core {
    public class PickTag {
        public byte NodeAddress { get; set; }
        public byte[] Data { get; set; } = [0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
        public bool IsConnected { get; set; } = false;
        public bool IsLedOn { get; set; } = false;
        public bool IsBuzzerOn { get; set; } = false;
        public bool IsFlashing { get; set; } = false;
        public Color Color { get; set; } = Color.Red;
        public static List<PickTag> CreatePickTags(int numberOfInstances) {
            List<PickTag> pickTags = [];
            for (int i = 1; i <= numberOfInstances; i++) {
                pickTags.Add(new PickTag { NodeAddress = (byte)i });
            }
            return pickTags;
        }
    }
}