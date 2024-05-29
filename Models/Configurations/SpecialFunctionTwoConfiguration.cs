namespace PickToLight.Core.Models.Configurations {
    public class SpecialFunctionTwoConfiguration {
        public bool IsDefaultReservedOneEnabled { get; private set; } = false;
        public bool IsDefaultReservedTwoEnabled { get; private set; } = true;
        public bool IsDefaultReservedThreeEnabled { get; private set; } = false;
        public bool IsAliveIndicatorEnabled { get; set; } = true;
        public bool IsDefaultReservedFourEnabled { get; private set; } = true;
        public bool IsDefaultReservedFiveEnabled { get; private set; } = false;
        public bool IsDefaultReservedSixEnabled { get; private set; } = false;
    }
}