using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Enums;
namespace PickToLight.Core.Utilities.Converters {
    public partial class ConfigConverter {
        public static byte PickTagModeConfigurationToByte(PickTagModeConfig config) {
            byte result = 0;
            if (config.IsConfirmationButtonEnabled) result |= 1 << 0;
            if (config.IsShortageButtonEnabled) result |= 1 << 1;
            if (config.IsUpDownCountEnabled) result |= 1 << 2;
            if (config.IsKeyCodeReturnEnabled) result |= 1 << 3;
            if (config.IsDisplayingCommunicationTimeoutEnabled) result |= 1 << 4;
            if (config.IsNodeAddressConfigurationEnabled) result |= 1 << 5;
            if (config.IsRedisplayingEnabled) result |= 1 << 6;
            return result;
        }
        public static byte ValidDigitsConfigurationToByte(ValidDigitsConfig config) {
            byte result = 0;
            if (!config.IsFirstValid) result |= 1 << 0;
            if (!config.IsSecondValid) result |= 1 << 1;
            if (!config.IsThirdValid) result |= 1 << 2;
            if (!config.IsFourthValid) result |= 1 << 3;
            if (!config.IsFifthValid) result |= 1 << 4;
            if (!config.IsSixthValid) result |= 1 << 5;
            if (!config.IsSeventhValid) result |= 1 << 6;
            if (!config.IsEighthValid) result |= 1 << 7;
            return result;
        }
        public static byte DigitsBrightnessConfigurationToByte(DigitsBrightnessConfig config) {
            byte result = 0;
            if (!config.ShouldDimFirst) result |= 1 << 0;
            if (!config.ShouldDimSecond) result |= 1 << 1;
            if (!config.ShouldDimThird) result |= 1 << 2;
            if (!config.ShouldDimFourth) result |= 1 << 3;
            if (!config.ShouldDimFifth) result |= 1 << 4;
            if (!config.ShouldDimSixth) result |= 1 << 5;
            return result;
        }
        public static byte SpecialFunctionOneConfigurationToByte(SpecialFunctionOneConfig config) {
            byte result = 0;
            if (config.PickTagMode == PickTagMode.Stock) result |= 1 << 0;
            if (config.IsStockModeQuickCompilationEnabled) result |= 1 << 1;
            if (config.IsCompletionMarkDisplayingEnabled) result |= 1 << 2;
            if (config.IsDeviceSelfTestingFunctionEnabled) result |= 1 << 3;
            if (config.IsCycleEditFunctionEnabled) result |= 1 << 4;
            if (config.IsDummyKeyEnabled) result |= 1 << 5;
            return result;
        }
        public static byte SpecialFunctionTwoConfigurationToByte(SpecialFunctionTwoConfig config) {
            byte result = 0;
            if (config.IsDefaultReservedOneEnabled) result |= 1 << 0;
            if (config.IsDefaultReservedTwoEnabled) result |= 1 << 1;
            if (config.IsDefaultReservedThreeEnabled) result |= 1 << 2;
            if (config.IsAliveIndicatorEnabled) result |= 1 << 3;
            if (config.IsDefaultReservedFourEnabled) result |= 1 << 4;
            if (config.IsDefaultReservedFiveEnabled) result |= 1 << 5;
            if (config.IsDefaultReservedSixEnabled) result |= 1 << 6;
            return result;
        }
    }
}