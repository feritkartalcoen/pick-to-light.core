using PickToLight.Core.Models.Enums;
namespace PickToLight.Core {
    public class PickTag {
        public byte NodeAddress { get; set; }
        public byte[] Data { get; set; } = [];
        public bool IsConnected { get; set; } = false;
        public bool IsLedOn { get; set; } = false;
        public bool IsBuzzerOn { get; set; } = false;
        public bool IsFlashing { get; set; } = false;
        public Color Color { get; set; } = Color.Red;
    }
}