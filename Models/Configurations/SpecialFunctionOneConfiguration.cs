using PickToLight.Core.Models.Enums;
namespace PickToLight.Core.Models.Configurations {
    public class SpecialFunctionOneConfiguration {
        public PickTagMode PickTagMode { get; set; } = PickTagMode.Pick;
        public bool IsStockModeQuickCompilationEnabled { get; set; } = false;
        public bool IsCompletionMarkDisplayingEnabled { get; set; } = false;
        public bool IsDeviceSelfTestingFunctionEnabled { get; set; } = true;
        public bool IsCycleEditFunctionEnabled { get; set; } = false;
        public bool IsDummyKeyEnabled { get; set; } = false;
    }
}