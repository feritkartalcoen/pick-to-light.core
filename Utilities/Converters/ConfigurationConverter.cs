using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Enums;
namespace PickToLight.Core.Utilities.Converters {
    public partial class ConfigurationConverter {
        public static byte PickTagModeConfigurationToByte(PickTagModeConfiguration pickTagModeConfiguration) {
            byte result = 0;
            if (pickTagModeConfiguration.IsConfirmationButtonEnabled) result |= 1 << 0;
            if (pickTagModeConfiguration.IsShortageButtonEnabled) result |= 1 << 1;
            if (pickTagModeConfiguration.IsUpDownCountEnabled) result |= 1 << 2;
            if (pickTagModeConfiguration.IsKeyCodeReturnEnabled) result |= 1 << 3;
            if (pickTagModeConfiguration.IsDisplayingCommunicationTimeoutEnabled) result |= 1 << 4;
            if (pickTagModeConfiguration.IsNodeAddressConfigurationEnabled) result |= 1 << 5;
            if (pickTagModeConfiguration.IsRedisplayingEnabled) result |= 1 << 6;
            return result;
        }
        public static byte ValidDigitsConfigurationToByte(ValidDigitsConfiguration validDigitsConfiguration) {
            byte result = 0;
            if (!validDigitsConfiguration.IsFirstDigitValid) result |= 1 << 0;
            if (!validDigitsConfiguration.IsSecondDigitValid) result |= 1 << 1;
            if (!validDigitsConfiguration.IsThirdDigitValid) result |= 1 << 2;
            if (!validDigitsConfiguration.IsFourthDigitValid) result |= 1 << 3;
            if (!validDigitsConfiguration.IsFifthDigitValid) result |= 1 << 4;
            if (!validDigitsConfiguration.IsSixthDigitValid) result |= 1 << 5;
            if (!validDigitsConfiguration.IsSeventhDigitValid) result |= 1 << 6;
            if (!validDigitsConfiguration.IsEighthDigitValid) result |= 1 << 7;
            return result;
        }
        public static byte DigitsBrightnessConfigurationToByte(DigitsBrightnessConfiguration digitsBrightnessConfiguration) {
            byte result = 0;
            if (!digitsBrightnessConfiguration.ShouldDimFirstDigit) result |= 1 << 0;
            if (!digitsBrightnessConfiguration.ShouldDimSecondDigit) result |= 1 << 1;
            if (!digitsBrightnessConfiguration.ShouldDimThirdDigit) result |= 1 << 2;
            if (!digitsBrightnessConfiguration.ShouldDimFourthDigit) result |= 1 << 3;
            if (!digitsBrightnessConfiguration.ShouldDimFifthDigit) result |= 1 << 4;
            if (!digitsBrightnessConfiguration.ShouldDimSixthDigit) result |= 1 << 5;
            return result;
        }
        public static byte SpecialFunctionOneConfigurationToByte(SpecialFunctionOneConfiguration specialFunctionOneConfiguration) {
            byte result = 0;
            if (specialFunctionOneConfiguration.PickTagMode == PickTagMode.Stock) result |= 1 << 0;
            if (specialFunctionOneConfiguration.IsStockModeQuickCompilationEnabled) result |= 1 << 1;
            if (specialFunctionOneConfiguration.IsCompletionMarkDisplayingEnabled) result |= 1 << 2;
            if (specialFunctionOneConfiguration.IsDeviceSelfTestingFunctionEnabled) result |= 1 << 3;
            if (specialFunctionOneConfiguration.IsCycleEditFunctionEnabled) result |= 1 << 4;
            if (specialFunctionOneConfiguration.IsDummyKeyEnabled) result |= 1 << 5;
            return result;
        }
        public static byte SpecialFunctionTwoConfigurationToByte(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration) {
            byte result = 0;
            if (specialFunctionTwoConfiguration.IsDefaultReservedOneEnabled) result |= 1 << 0;
            if (specialFunctionTwoConfiguration.IsDefaultReservedTwoEnabled) result |= 1 << 1;
            if (specialFunctionTwoConfiguration.IsDefaultReservedThreeEnabled) result |= 1 << 2;
            if (specialFunctionTwoConfiguration.IsAliveIndicatorEnabled) result |= 1 << 3;
            if (specialFunctionTwoConfiguration.IsDefaultReservedFourEnabled) result |= 1 << 4;
            if (specialFunctionTwoConfiguration.IsDefaultReservedFiveEnabled) result |= 1 << 5;
            if (specialFunctionTwoConfiguration.IsDefaultReservedSixEnabled) result |= 1 << 6;
            return result;
        }
    }
}