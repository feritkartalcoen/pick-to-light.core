namespace PickToLight.Core.Models.Configurations {
    public class PickTagModeConfiguration {
        public bool IsConfirmationButtonEnabled { get; set; } = true;
        public bool IsShortageButtonEnabled { get; set; } = true;
        public bool IsUpDownCountEnabled { get; set; } = false;
        public bool IsKeyCodeReturnEnabled { get; set; } = false;
        public bool IsDisplayingCommunicationTimeoutEnabled { get; set; } = true;
        public bool IsNodeAddressConfigurationEnabled { get; set; } = true;
        public bool IsRedisplayingEnabled { get; set; } = true;
    }
}