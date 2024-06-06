using PickToLight.Core.Models.Enums;
using System.Collections.Generic;
namespace PickToLight.Core {
    public class PickTag {
        public byte Node { get; set; }
        public byte[] Data { get; set; } = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public bool IsConnected { get; set; } = false;
        public bool IsLedOn { get; set; } = false;
        public bool IsBuzzerOn { get; set; } = false;
        public bool IsFlashing { get; set; } = false;
        public Color Color { get; set; } = Color.Red;
        public static List<PickTag> CreatePickTags(int numberOfInstances) {
            List<PickTag> pickTags = new List<PickTag>();
            for (int i = 1; i <= numberOfInstances; i++) {
                pickTags.Add(new PickTag { Node = (byte)i });
            }
            return pickTags;
        }
    }
}