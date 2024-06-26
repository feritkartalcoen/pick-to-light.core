namespace PickToLight.Core.Services.Interfaces {
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Datas;
	using PickToLight.Core.Models.Enums;
	#region Interfaces
	public interface IControllerService {
		#region Methods
		public void ChangeAvailableDigitsForCounting(MessageType messageType, int? nodeAddress, int availableDigitsCount);
		public void ChangeBlinkingTimeInterval(MessageType messageType, int? nodeAddress, BlinkingTimeInterval blinkingTimeInterval);
		public void ChangeColor(MessageType messageType, int? nodeAddress, Color color, bool shouldStore);
		// TODO: Implement ChangeDigitsBlinkingTimeInterval
		public void ChangeDigitsBrightness(MessageType messageType, int? nodeAddress, DigitsBrightnessConfiguration digitsBrightnessConfiguration);
		public void ChangeFlashingTimeInterval(MessageType messageType, int? nodeAddress, FlashingTimeInterval flashingTimeInterval);
		public void ChangeNodeAddress(MessageType messageType, int nodeAddress, int newNodeAddress);
		public void ChangePickTagConfigurationWithSpecialFunctionOne(MessageType messageType, int? nodeAddress, SpecialFunctionOneConfiguration specialFunctionOneConfiguration);
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(MessageType messageType, int? nodeAddress, SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration);
		public void ChangePickTagModeConfigurationPermanently(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration);
		public void ChangePickTagModeConfigurationTemporarily(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration);
		public void ChangePollingRange(MessageType messageType, int pollingRange);
		public void ChangeValidDigitsForCounting(MessageType messageType, int? nodeAddress, ValidDigitsConfiguration validDigitsConfiguration);
		public void Clear(MessageType messageType, int? nodeAddress);
		public bool Connect(string ipAddress, int port);
		public void DisableConfirmationButton(MessageType messageType, int? nodeAddress);
		public void DisableShortageButton(MessageType messageType, int? nodeAddress);
		public bool Disconnect();
		public void Display(MessageType messageType, int? nodeAddress, string value, string dotsPosition, bool shouldFlash);
		public void DisplayNodeAddress(MessageType messageType, int? nodeAddress);
		public void EmulateConfirmationButtonPressing(MessageType messageType, int? nodeAddress);
		public void EmulateShortageButtonPressing(MessageType messageType, int? nodeAddress);
		public void EnableConfirmationButton(MessageType messageType, int? nodeAddress);
		public void EnableShortageButton(MessageType messageType, int? nodeAddress);
		public void Flash(MessageType messageType, int? nodeAddress);
		public void OnButtonsLocked(CommunicationControlBlock communicationControlBlock);
		public void OnConfirmationButtonPressed(CommunicationControlBlock communicationControlBlock);
		public void OnConnectedPickTagsReceived(CommunicationControlBlock communicationControlBlock);
		public void OnIllegal(CommunicationControlBlock communicationControlBlock);
		public void OnMalfunction(CommunicationControlBlock communicationControlBlock);
		public void OnOldPickTagResetOrConnect(CommunicationControlBlock communicationControlBlock);
		public void OnQuantityInStockReceived(CommunicationControlBlock communicationControlBlock);
		public void OnShortageButtonPressed(CommunicationControlBlock communicationControlBlock);
		public void OnSpecialReceived(CommunicationControlBlock communicationControlBlock);
		public void OnTimeout(CommunicationControlBlock communicationControlBlock);
		public void RequestConnectedPickTags(MessageType messageType);
		public void RequestPickTagDetails(MessageType messageType, int? nodeAddress);
		public void RequestPickTagModel(MessageType messageType, int? nodeAddress);
		public void Reset(MessageType messageType, int? nodeAddress);
		public void SwitchToPickingMode(MessageType messageType, int? nodeAddress);
		public void SwitchToStockMode(MessageType messageType, int? nodeAddress);
		public void TurnBuzzerOff(MessageType messageType, int? nodeAddress);
		public void TurnBuzzerOn(MessageType messageType, int? nodeAddress);
		public void TurnLedOff(MessageType messageType, int? nodeAddress);
		public void TurnLedOn(MessageType messageType, int? nodeAddress);
		#endregion
	}
	#endregion
}