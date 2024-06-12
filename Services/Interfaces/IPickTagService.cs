namespace PickToLight.Core.Services.Interfaces {
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Enums;
	#region Interfaces
	public interface IPickTagService {
		#region Methods
		void ChangeAvailableDigitsForCounting(int availableDigitsCount);
		void ChangeBlinkingTimeInterval(BlinkingTimeInterval blinkingTimeInterval);
		void ChangeColor(Color color);
		// TODO: Implement ChangeDigitsBlinkingTimeInterval
		void ChangeDigitsBrightness(DigitsBrightnessConfiguration digitsBrightnessConfiguration);
		void ChangeFlashingTimeInterval(FlashingTimeInterval flashingTimeInterval);
		void ChangePickTagConfigurationWithSpecialFunctionOne(SpecialFunctionOneConfiguration specialFunctionOneConfiguration);
		void ChangePickTagConfigurationWithSpecialFunctionTwo(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration);
		void ChangePickTagModeConfiguration(PickTagModeConfiguration pickTagModeConfiguration);
		void ChangeValidDigitsForCounting(ValidDigitsConfiguration validDigitsConfiguration);
		void Clear();
		void Connect();
		void DisableConfirmationButton();
		void DisableShortageButton();
		void Disconnect();
		void Display(string value, bool shouldFlash);
		void DisplayNodeAddress();
		void EmulateConfirmationButtonPressing();
		void EmulateShortageButtonPressing();
		void EnableConfirmationButton();
		void EnableShortageButton();
		void Flash();
		void OnButtonsLocked();
		void OnConfirmationButtonPressed(string value);
		void OnIllegal();
		void OnMalfunction();
		void OnOldPickTagResetOrConnect();
		void OnQuantityInStockReceived(string value);
		void OnShortageButtonPressed(string value);
		void OnSpecialReceived();
		void OnTimeout();
		void RequestPickTagDetails();
		void RequestPickTagModel();
		void Reset();
		void SwitchToPickingMode();
		void SwitchToStockMode();
		void TurnBuzzerOff();
		void TurnBuzzerOn();
		void TurnLedOff();
		void TurnLedOn();
		#endregion
	}
	#endregion
}