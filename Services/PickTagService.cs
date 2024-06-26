namespace PickToLight.Core.Services {
	using PickToLight.Core.Models;
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Enums;
	using PickToLight.Core.Services.Interfaces;
	using System.Diagnostics;
	public class PickTagService(PickTag pickTag) : IPickTagService {
		#region Methods
		public void ChangeAvailableDigitsForCounting(int availableDigitsCount) {
			pickTag.AvailableDigitsCount = availableDigitsCount;
		}
		public void ChangeBlinkingTimeInterval(BlinkingTimeInterval blinkingTimeInterval) {
			pickTag.BlinkingTimeInterval = blinkingTimeInterval;
			pickTag.Flash();
		}
		public void ChangeColor(Color color) {
			pickTag.Color = color;
		}
		public void ChangeDigitsBrightness(DigitsBrightnessConfiguration digitsBrightnessConfiguration) {
			Debug.WriteLine($"{nameof(ChangeDigitsBrightness)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void ChangeFlashingTimeInterval(FlashingTimeInterval flashingTimeInterval) {
			pickTag.FlashingTimeInterval = flashingTimeInterval;
		}
		public void ChangePickTagConfigurationWithSpecialFunctionOne(SpecialFunctionOneConfiguration specialFunctionOneConfiguration) {
			pickTag.IsCompletionMarkDisplayingEnabled = specialFunctionOneConfiguration.IsCompletionMarkDisplayingEnabled;
			pickTag.IsCycleEditFunctionEnabled = specialFunctionOneConfiguration.IsCycleEditFunctionEnabled;
			pickTag.IsDeviceSelfTestingFunctionEnabled = specialFunctionOneConfiguration.IsDeviceSelfTestingFunctionEnabled;
			pickTag.IsDummyKeyEnabled = specialFunctionOneConfiguration.IsDummyKeyEnabled;
			pickTag.IsStockModeQuickCompilationEnabled = specialFunctionOneConfiguration.IsStockModeQuickCompilationEnabled;
			pickTag.PickTagMode = specialFunctionOneConfiguration.PickTagMode;
		}
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration) {
			pickTag.IsAliveIndicatorEnabled = specialFunctionTwoConfiguration.IsAliveIndicatorEnabled;
		}
		public void ChangePickTagModeConfiguration(PickTagModeConfiguration pickTagModeConfiguration) {
			pickTag.IsConfirmationButtonEnabled = pickTagModeConfiguration.IsConfirmationButtonEnabled;
			pickTag.IsDisplayingCommunicationTimeoutEnabled = pickTagModeConfiguration.IsDisplayingCommunicationTimeoutEnabled;
			pickTag.IsKeyCodeReturnEnabled = pickTagModeConfiguration.IsKeyCodeReturnEnabled;
			pickTag.IsNodeAddressConfigurationEnabled = pickTagModeConfiguration.IsNodeAddressConfigurationEnabled;
			pickTag.IsRedisplayingEnabled = pickTagModeConfiguration.IsRedisplayingEnabled;
			pickTag.IsShortageButtonEnabled = pickTagModeConfiguration.IsShortageButtonEnabled;
			pickTag.IsUpDownCountEnabled = pickTagModeConfiguration.IsUpDownCountEnabled;
		}
		public void ChangeValidDigitsForCounting(ValidDigitsConfiguration validDigitsConfiguration) {
			Debug.WriteLine($"{nameof(ChangeValidDigitsForCounting)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void Clear() {
			pickTag.Value = string.Empty;
			pickTag.ConfirmedValue = string.Empty;
			pickTag.ShortageValue = string.Empty;
			pickTag.DotsPosition = "000000";
			pickTag.IsLedOn = false;
			pickTag.IsBuzzerOn = false;
			pickTag.IsFlashing = false;
		}
		public void Connect() {
			pickTag.IsConnected = true;
		}
		public void DisableConfirmationButton() {
			pickTag.IsConfirmationButtonEnabled = false;
		}
		public void DisableShortageButton() {
			pickTag.IsShortageButtonEnabled = false;
		}
		public void Disconnect() {
			pickTag.IsConnected = false;
		}
		public void Display(string value, string dotsPosition, bool shouldFlash) {
			pickTag.Value = value;
			pickTag.DotsPosition = dotsPosition;
			pickTag.IsFlashing = shouldFlash;
			pickTag.IsLedOn = true;
			pickTag.IsBuzzerOn = true;
		}
		public void DisplayNodeAddress() {
			pickTag.Value = pickTag.NodeAddress.ToString("D3");
		}
		public void EmulateConfirmationButtonPressing() {
			pickTag.ConfirmedValue = pickTag.Value;
			pickTag.Value = string.Empty;
		}
		public void EmulateShortageButtonPressing() {
			pickTag.ConfirmedValue = pickTag.Value;
			pickTag.ShortageValue = "0";
			pickTag.Value = string.Empty;
		}
		public void EnableConfirmationButton() {
			pickTag.IsConfirmationButtonEnabled = true;
		}
		public void EnableShortageButton() {
			pickTag.IsShortageButtonEnabled = true;
		}
		public void Flash() {
			pickTag.IsFlashing = true;
		}
		public void OnButtonsLocked() {
			Debug.WriteLine($"{nameof(OnButtonsLocked)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void OnConfirmationButtonPressed(string value, string dotsPosition) {
			pickTag.ConfirmedValue = value;
			pickTag.Value = string.Empty;
			pickTag.DotsPosition = dotsPosition;
		}
		public void OnIllegal() {
			Debug.WriteLine($"{nameof(OnIllegal)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void OnMalfunction() {
			Debug.WriteLine($"{nameof(OnMalfunction)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void OnOldPickTagResetOrConnect() {
			Debug.WriteLine($"{nameof(OnOldPickTagResetOrConnect)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void OnQuantityInStockReceived(string value, string dotsPosition) {
			pickTag.ConfirmedValue = value;
			pickTag.Value = string.Empty;
			pickTag.DotsPosition = dotsPosition;
		}
		public void OnShortageButtonPressed(string value, string dotsPosition) {
			pickTag.ShortageValue = value;
			pickTag.Value = string.Empty;
			pickTag.DotsPosition = dotsPosition;
		}
		public void OnSpecialReceived() {
			Debug.WriteLine($"{nameof(OnSpecialReceived)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void OnTimeout() {
			Debug.WriteLine($"{nameof(OnTimeout)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void RequestPickTagDetails() {
			Debug.WriteLine($"{nameof(RequestPickTagDetails)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void RequestPickTagModel() {
			Debug.WriteLine($"{nameof(RequestPickTagModel)} on {nameof(PickTagService)} is not implemented yet.");
		}
		public void Reset() {
			pickTag.Revert();
		}
		public void SwitchToPickingMode() {
			pickTag.PickTagMode = PickTagMode.Picking;
		}
		public void SwitchToStockMode() {
			pickTag.PickTagMode = PickTagMode.Stock;
		}
		public void TurnBuzzerOff() {
			pickTag.IsBuzzerOn = false;
		}
		public void TurnBuzzerOn() {
			pickTag.IsBuzzerOn = true;
		}
		public void TurnLedOff() {
			pickTag.IsLedOn = false;
		}
		public void TurnLedOn() {
			pickTag.IsLedOn = true;
		}
		#endregion
	}
}