namespace PickToLight.Core.Models {
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Enums;
	using PickToLight.Core.Services;
	using System.Collections.Generic;
	public class PickTag {
		#region Fields
		private readonly PickTagService _pickTagService;
		#endregion
		#region Constructors
		public PickTag() {
			_pickTagService = new(this);
		}
		#endregion
		#region Properties
		public int AvailableDigitsCount { get; set; } = 6;
		public BlinkingTimeInterval BlinkingTimeInterval { get; set; } = BlinkingTimeInterval.HalfSecond;
		public Color Color { get; set; } = Color.Red;
		public string ConfirmedValue { get; set; } = string.Empty;
		public string DotsPosition { get; set; } = "000000";
		public FlashingTimeInterval FlashingTimeInterval { get; set; } = FlashingTimeInterval.HalfSecond;
		public bool IsAliveIndicatorEnabled { get; set; } = true;
		public bool IsBuzzerOn { get; set; } = false;
		public bool IsCompletionMarkDisplayingEnabled { get; set; } = false;
		public bool IsConfirmationButtonEnabled { get; set; } = true;
		public bool IsConnected { get; set; } = false;
		public bool IsCycleEditFunctionEnabled { get; set; } = false;
		public bool IsDeviceSelfTestingFunctionEnabled { get; set; } = true;
		public bool IsDisplayingCommunicationTimeoutEnabled { get; set; } = true;
		public bool IsDummyKeyEnabled { get; set; } = false;
		public bool IsFlashing { get; set; } = false;
		public bool IsKeyCodeReturnEnabled { get; set; } = false;
		public bool IsLedOn { get; set; } = false;
		public bool IsNodeAddressConfigurationEnabled { get; set; } = true;
		public bool IsRedisplayingEnabled { get; set; } = true;
		public bool IsShortageButtonEnabled { get; set; } = true;
		public bool IsStockModeQuickCompilationEnabled { get; set; } = false;
		public bool IsUpDownCountEnabled { get; set; } = false;
		public int NodeAddress { get; set; }
		public PickTagMode PickTagMode { get; set; } = PickTagMode.Picking;
		public string PickTagModel { get; set; } = string.Empty;
		public string ShortageValue { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
		#endregion
		#region Methods
		public static PickTag GetPickTag(int? nodeAddress, List<PickTag> pickTags) {
			return pickTags.Find(pickTag => pickTag.NodeAddress == nodeAddress && pickTag.IsConnected)!;
		}
		public static List<PickTag> GetPickTags(List<PickTag> pickTags) {
			return pickTags.FindAll(pickTag => pickTag.IsConnected);
		}
		public void ChangeAvailableDigitsForCounting(int availableDigitsCount) {
			_pickTagService.ChangeAvailableDigitsForCounting(availableDigitsCount);
		}
		public void ChangeBlinkingTimeInterval(BlinkingTimeInterval blinkingTimeInterval) {
			_pickTagService.ChangeBlinkingTimeInterval(blinkingTimeInterval);
		}
		public void ChangeColor(Color color) {
			_pickTagService.ChangeColor(color);
		}
		public void ChangeDigitsBrightness(DigitsBrightnessConfiguration digitsBrightnessConfiguration) {
			_pickTagService.ChangeDigitsBrightness(digitsBrightnessConfiguration);
		}
		public void ChangeFlashingTimeInterval(FlashingTimeInterval flashingTimeInterval) {
			_pickTagService.ChangeFlashingTimeInterval(flashingTimeInterval);
		}
		public void ChangePickTagConfigurationWithSpecialFunctionOne(SpecialFunctionOneConfiguration specialFunctionOneConfiguration) {
			_pickTagService.ChangePickTagConfigurationWithSpecialFunctionOne(specialFunctionOneConfiguration);
		}
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration) {
			_pickTagService.ChangePickTagConfigurationWithSpecialFunctionTwo(specialFunctionTwoConfiguration);
		}
		public void ChangePickTagModeConfiguration(PickTagModeConfiguration pickTagModeConfiguration) {
			_pickTagService.ChangePickTagModeConfiguration(pickTagModeConfiguration);
		}
		public void ChangeValidDigitsForCounting(ValidDigitsConfiguration validDigitsConfiguration) {
			_pickTagService.ChangeValidDigitsForCounting(validDigitsConfiguration);
		}
		public void Clear() {
			_pickTagService.Clear();
		}
		public void Connect() {
			_pickTagService.Connect();
		}
		public void DisableConfirmationButton() {
			_pickTagService.DisableConfirmationButton();
		}
		public void DisableShortageButton() {
			_pickTagService.DisableShortageButton();
		}
		public void Disconnect() {
			_pickTagService.Disconnect();
		}
		public void Display(string value, string dotsPosition, bool shouldFlash) {
			_pickTagService.Display(value, dotsPosition, shouldFlash);
		}
		public void DisplayNodeAddress() {
			_pickTagService.DisplayNodeAddress();
		}
		public void EmulateConfirmationButtonPressing() {
			_pickTagService.EmulateConfirmationButtonPressing();
		}
		public void EmulateShortageButtonPressing() {
			_pickTagService.EmulateShortageButtonPressing();
		}
		public void EnableConfirmationButton() {
			_pickTagService.EnableConfirmationButton();
		}
		public void EnableShortageButton() {
			_pickTagService.EnableShortageButton();
		}
		public void Flash() {
			_pickTagService.Flash();
		}
		public void OnButtonsLocked() {
			_pickTagService.OnButtonsLocked();
		}
		public void OnConfirmationButtonPressed(string value, string dotsPosition) {
			_pickTagService.OnConfirmationButtonPressed(value, dotsPosition);
		}
		public void OnIllegal() {
			_pickTagService.OnIllegal();
		}
		public void OnMalfunction() {
			_pickTagService.OnMalfunction();
		}
		public void OnOldPickTagResetOrConnect() {
			_pickTagService.OnOldPickTagResetOrConnect();
		}
		public void OnQuantityInStockReceived(string value, string dotsPosition) {
			_pickTagService.OnQuantityInStockReceived(value, dotsPosition);
		}
		public void OnShortageButtonPressed(string value, string dotsPosition) {
			_pickTagService.OnShortageButtonPressed(value, dotsPosition);
		}
		public void OnSpecialReceived() {
			_pickTagService.OnSpecialReceived();
		}
		public void OnTimeout() {
			_pickTagService.OnTimeout();
		}
		public void RequestPickTagDetails() {
			_pickTagService.RequestPickTagDetails();
		}
		public void RequestPickTagModel() {
			_pickTagService.RequestPickTagModel();
		}
		public void Reset() {
			_pickTagService.Reset();
		}
		public void Revert() {
			AvailableDigitsCount = 6;
			BlinkingTimeInterval = BlinkingTimeInterval.HalfSecond;
			Color = Color.Red;
			ConfirmedValue = string.Empty;
			DotsPosition = "000000";
			FlashingTimeInterval = FlashingTimeInterval.HalfSecond;
			IsAliveIndicatorEnabled = true;
			IsBuzzerOn = false;
			IsCompletionMarkDisplayingEnabled = false;
			IsConfirmationButtonEnabled = true;
			IsCycleEditFunctionEnabled = false;
			IsDeviceSelfTestingFunctionEnabled = true;
			IsDisplayingCommunicationTimeoutEnabled = true;
			IsDummyKeyEnabled = false;
			IsFlashing = false;
			IsKeyCodeReturnEnabled = false;
			IsLedOn = false;
			IsNodeAddressConfigurationEnabled = true;
			IsRedisplayingEnabled = true;
			IsShortageButtonEnabled = true;
			IsStockModeQuickCompilationEnabled = false;
			IsUpDownCountEnabled = false;
			PickTagMode = PickTagMode.Picking;
			ShortageValue = string.Empty;
			Value = string.Empty;
		}
		public void SwitchToPickingMode() {
			_pickTagService.SwitchToPickingMode();
		}
		public void SwitchToStockMode() {
			_pickTagService.SwitchToStockMode();
		}
		public void TurnBuzzerOff() {
			_pickTagService.TurnBuzzerOff();
		}
		public void TurnBuzzerOn() {
			_pickTagService.TurnBuzzerOn();
		}
		public void TurnLedOff() {
			_pickTagService.TurnLedOff();
		}
		public void TurnLedOn() {
			_pickTagService.TurnLedOn();
		}
		#endregion
	}
}