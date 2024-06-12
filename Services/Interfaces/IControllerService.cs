namespace PickToLight.Core.Services.Interfaces {
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Datas;
	using PickToLight.Core.Models.Enums;
	#region Interfaces
	public interface IControllerService {
		#region Methods
		void ChangeAvailableDigitsForCounting(MessageType messageType, int? nodeAddress, int availableDigitsCount);
		void ChangeBlinkingTimeInterval(MessageType messageType, int? nodeAddress, BlinkingTimeInterval blinkingTimeInterval);
		void ChangeColor(MessageType messageType, int? nodeAddress, Color color, bool shouldStore);
		// TODO: Implement ChangeDigitsBlinkingTimeInterval
		void ChangeDigitsBrightness(MessageType messageType, int? nodeAddress, DigitsBrightnessConfiguration digitsBrightnessConfiguration);
		void ChangeFlashingTimeInterval(MessageType messageType, int? nodeAddress, FlashingTimeInterval flashingTimeInterval);
		void ChangeNodeAddress(MessageType messageType, int nodeAddress, int newNodeAddress);
		void ChangePickTagConfigurationWithSpecialFunctionOne(MessageType messageType, int? nodeAddress, SpecialFunctionOneConfiguration specialFunctionOneConfiguration);
		void ChangePickTagConfigurationWithSpecialFunctionTwo(MessageType messageType, int? nodeAddress, SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration);
		void ChangePickTagModeConfigurationPermanently(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration);
		void ChangePickTagModeConfigurationTemporarily(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration);
		void ChangePollingRange(MessageType messageType, int pollingRange);
		void ChangeValidDigitsForCounting(MessageType messageType, int? nodeAddress, ValidDigitsConfiguration validDigitsConfiguration);
		void Clear(MessageType messageType, int? nodeAddress);
		bool Connect(string ipAddress, int port);
		void DisableConfirmationButton(MessageType messageType, int? nodeAddress);
		void DisableShortageButton(MessageType messageType, int? nodeAddress);
		bool Disconnect();
		void Display(MessageType messageType, int? nodeAddress, string value, bool shouldFlash);
		void DisplayNodeAddress(MessageType messageType, int? nodeAddress);
		void EmulateConfirmationButtonPressing(MessageType messageType, int? nodeAddress);
		void EmulateShortageButtonPressing(MessageType messageType, int? nodeAddress);
		void EnableConfirmationButton(MessageType messageType, int? nodeAddress);
		void EnableShortageButton(MessageType messageType, int? nodeAddress);
		void Flash(MessageType messageType, int? nodeAddress);
		void OnButtonsLocked(CommunicationControlBlock communicationControlBlock);
		void OnConfirmationButtonPressed(CommunicationControlBlock communicationControlBlock);
		void OnConnectedPickTagsReceived(CommunicationControlBlock communicationControlBlock);
		void OnIllegal(CommunicationControlBlock communicationControlBlock);
		void OnMalfunction(CommunicationControlBlock communicationControlBlock);
		void OnOldPickTagResetOrConnect(CommunicationControlBlock communicationControlBlock);
		void OnQuantityInStockReceived(CommunicationControlBlock communicationControlBlock);
		void OnShortageButtonPressed(CommunicationControlBlock communicationControlBlock);
		void OnSpecialReceived(CommunicationControlBlock communicationControlBlock);
		void OnTimeout(CommunicationControlBlock communicationControlBlock);
		void RequestConnectedPickTags(MessageType messageType);
		void RequestPickTagDetails(MessageType messageType, int? nodeAddress);
		void RequestPickTagModel(MessageType messageType, int? nodeAddress);
		void Reset(MessageType messageType, int? nodeAddress);
		void SwitchToPickingMode(MessageType messageType, int? nodeAddress);
		void SwitchToStockMode(MessageType messageType, int? nodeAddress);
		void TurnBuzzerOff(MessageType messageType, int? nodeAddress);
		void TurnBuzzerOn(MessageType messageType, int? nodeAddress);
		void TurnLedOff(MessageType messageType, int? nodeAddress);
		void TurnLedOn(MessageType messageType, int? nodeAddress);
		#endregion
	}
	#endregion
}