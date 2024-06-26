namespace PickToLight.Core.Services.Interfaces {
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Enums;
	#region Interfaces
	public interface IPickTagService {
		#region Methods
		public void ChangeAvailableDigitsForCounting(int availableDigitsCount);
		public void ChangeBlinkingTimeInterval(BlinkingTimeInterval blinkingTimeInterval);
		public void ChangeColor(Color color);
		// TODO: Implement ChangeDigitsBlinkingTimeInterval
		public void ChangeDigitsBrightness(DigitsBrightnessConfiguration digitsBrightnessConfiguration);
		public void ChangeFlashingTimeInterval(FlashingTimeInterval flashingTimeInterval);
		public void ChangePickTagConfigurationWithSpecialFunctionOne(SpecialFunctionOneConfiguration specialFunctionOneConfiguration);
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration);
		public void ChangePickTagModeConfiguration(PickTagModeConfiguration pickTagModeConfiguration);
		public void ChangeValidDigitsForCounting(ValidDigitsConfiguration validDigitsConfiguration);
		public void Clear();
		public void Connect();
		public void DisableConfirmationButton();
		public void DisableShortageButton();
		public void Disconnect();
		public void Display(string value, string dotsPosition, bool shouldFlash);
		public void DisplayNodeAddress();
		public void EmulateConfirmationButtonPressing();
		public void EmulateShortageButtonPressing();
		public void EnableConfirmationButton();
		public void EnableShortageButton();
		public void Flash();
		public void OnButtonsLocked();
		public void OnConfirmationButtonPressed(string value, string dotsPosition);
		public void OnIllegal();
		public void OnMalfunction();
		public void OnOldPickTagResetOrConnect();
		public void OnQuantityInStockReceived(string value, string dotsPosition);
		public void OnShortageButtonPressed(string value, string dotsPosition);
		public void OnSpecialReceived();
		public void OnTimeout();
		public void RequestPickTagDetails();
		public void RequestPickTagModel();
		public void Reset();
		public void SwitchToPickingMode();
		public void SwitchToStockMode();
		public void TurnBuzzerOff();
		public void TurnBuzzerOn();
		public void TurnLedOff();
		public void TurnLedOn();
		#endregion
	}
	#endregion
}