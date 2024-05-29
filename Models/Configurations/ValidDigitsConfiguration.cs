namespace PickToLight.Core.Models.Configurations {
    public class ValidDigitsConfiguration {
        public bool IsFirstDigitValid { get; set; } = true;
        public bool IsSecondDigitValid { get; set; } = true;
        public bool IsThirdDigitValid { get; set; } = true;
        public bool IsFourthDigitValid { get; set; } = true;
        public bool IsFifthDigitValid { get; set; } = true;
        public bool IsSixthDigitValid { get; set; } = true;
        public bool IsSeventhDigitValid { get; set; } = false;
        public bool IsEighthDigitValid { get; set; } = false;
    }
}