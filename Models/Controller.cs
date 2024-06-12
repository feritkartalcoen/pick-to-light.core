namespace PickToLight.Core.Models {
	using PickToLight.Core.Models.Actions;
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Enums;
	using PickToLight.Core.Services;
	using System.Collections.Generic;
	public class Controller {
		#region Fields
		private readonly ControllerService _controllerService;
		#endregion
		#region Constructors
		public Controller(ControllerModel controllerModel, int pickTagsCount, string ipAddress = "10.0.50.100", int port = 4660) {
			ChannelsCount = controllerModel == ControllerModel.AT400 ? 1 : 4;
			ControllerModel = controllerModel;
			ControllerPortsCount = controllerModel == ControllerModel.AT400 ? 2 : 1;
			IpAddress = ipAddress;
			PickTagsCount = pickTagsCount;
			PickTagsCapacity = ChannelsCount * PickTagsCapacityPerChannel;
			PollingRange = PickTagsCapacity;
			Port = port;
			for (int i = 0; i < pickTagsCount; i++) {
				PickTags.Add(new PickTag() {
					NodeAddress = i + 1
				});
			}
			_controllerService = new ControllerService(PickTags, OnReadActions);
			_controllerService.Update += new ControllerService.OnUpdate(() => {
				Update?.Invoke();
			});
		}
		#endregion
		#region Delegates
		public delegate void OnUpdate();
		#endregion
		#region Events
		public event OnUpdate Update;
		#endregion
		#region Properties
		public int ChannelsCount { get; private set; }
		public ControllerModel ControllerModel { get; }
		public int ControllerPortsCount { get; private set; }
		public string IpAddress { get; }
		public bool IsConnected { get; private set; } = false;
		public MessageType MessageType { get; set; } = MessageType.Port1;
		public OnReadActions OnReadActions { get; set; } = OnReadActions.Default();
		public List<PickTag> PickTags { get; private set; } = new List<PickTag>();
		public int PickTagsCapacity { get; private set; }
		public int PickTagsCapacityPerChannel { get; } = 30;
		public int PickTagsCount { get; private set; }
		public int PollingRange { get; private set; }
		public int Port { get; }
		#endregion
		#region Methods
		public void ChangeAvailableDigitsForCounting(int availableDigitsCount, int? nodeAddress = null) {
			_controllerService.ChangeAvailableDigitsForCounting(MessageType, nodeAddress, availableDigitsCount);
		}
		public void ChangeBlinkingTimeInterval(BlinkingTimeInterval blinkingTimeInterval, int? nodeAddress = null) {
			_controllerService.ChangeBlinkingTimeInterval(MessageType, nodeAddress, blinkingTimeInterval);
		}
		public void ChangeColor(Color color, int? nodeAddress = null, bool shouldStore = false) {
			_controllerService.ChangeColor(MessageType, nodeAddress, color, shouldStore);
		}
		public void ChangeDigitsBrightness(DigitsBrightnessConfiguration digitsBrightnessConfiguration, int? nodeAddress = null) {
			_controllerService.ChangeDigitsBrightness(MessageType, nodeAddress, digitsBrightnessConfiguration);
		}
		public void ChangeFlashingTimeInterval(FlashingTimeInterval flashingTimeInterval, int? nodeAddress = null) {
			_controllerService.ChangeFlashingTimeInterval(MessageType, nodeAddress, flashingTimeInterval);
		}
		public void ChangeNodeAddress(int nodeAddress, int newNodeAddress) {
			_controllerService.ChangeNodeAddress(MessageType, nodeAddress, newNodeAddress);
		}
		public void ChangePickTagConfigurationWithSpecialFunctionOne(SpecialFunctionOneConfiguration specialFunctionOneConfiguration, int? nodeAddress = null) {
			_controllerService.ChangePickTagConfigurationWithSpecialFunctionOne(MessageType, nodeAddress, specialFunctionOneConfiguration);
		}
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration, int? nodeAddress = null) {
			_controllerService.ChangePickTagConfigurationWithSpecialFunctionTwo(MessageType, nodeAddress, specialFunctionTwoConfiguration);
		}
		public void ChangePickTagModeConfiguration(PickTagModeConfiguration pickTagModeConfiguration, int? nodeAddress = null, bool isPermanent = false) {
			if (isPermanent) {
				_controllerService.ChangePickTagModeConfigurationPermanently(MessageType, nodeAddress, pickTagModeConfiguration);
			} else {
				_controllerService.ChangePickTagModeConfigurationTemporarily(MessageType, nodeAddress, pickTagModeConfiguration);
			}
		}
		public void ChangePollingRange(int pollingRange) {
			_controllerService.ChangePollingRange(MessageType, pollingRange);
		}
		public void ChangeValidDigitsForCounting(ValidDigitsConfiguration validDigitsConfiguration, int? nodeAddress = null) {
			_controllerService.ChangeValidDigitsForCounting(MessageType, nodeAddress, validDigitsConfiguration);
		}
		public void Clear(int? nodeAddress = null) {
			_controllerService.Clear(MessageType, nodeAddress);
		}
		public void Connect() {
			IsConnected = _controllerService.Connect(IpAddress, Port);
			if (IsConnected) {
				_controllerService.RequestConnectedPickTags(MessageType);
			}
		}
		public void DisableConfirmationButton(int? nodeAddress = null) {
			_controllerService.DisableConfirmationButton(MessageType, nodeAddress);
		}
		public void DisableShortageButton(int? nodeAddress = null) {
			_controllerService.DisableShortageButton(MessageType, nodeAddress);
		}
		public void Disconnect() {
			IsConnected = !_controllerService.Disconnect();
		}
		public void Display(string value, int? nodeAddress = null, bool shouldFlash = false) {
			_controllerService.Display(MessageType, nodeAddress, value, shouldFlash);
		}
		public void DisplayNodeAddress(int? nodeAddress = null) {
			_controllerService.DisplayNodeAddress(MessageType, nodeAddress);
		}
		public void EmulateConfirmationButtonPressing(int? nodeAddress = null) {
			_controllerService.EmulateConfirmationButtonPressing(MessageType, nodeAddress);
		}
		public void EmulateShortageButtonPressing(int? nodeAddress = null) {
			_controllerService.EmulateShortageButtonPressing(MessageType, nodeAddress);
		}
		public void EnableConfirmationButton(int? nodeAddress = null) {
			_controllerService.EnableConfirmationButton(MessageType, nodeAddress);
		}
		public void EnableShortageButton(int? nodeAddress = null) {
			_controllerService.EnableShortageButton(MessageType, nodeAddress);
		}
		public void Flash(int? nodeAddress = null) {
			_controllerService.Flash(MessageType, nodeAddress);
		}
		public void RequestConnectedPickTags() {
			_controllerService.RequestConnectedPickTags(MessageType);
		}
		public void RequestPickTagDetails(int? nodeAddress = null) {
			_controllerService.RequestPickTagDetails(MessageType, nodeAddress);
		}
		public void RequestPickTagModel(int? nodeAddress = null) {
			_controllerService.RequestPickTagModel(MessageType, nodeAddress);
		}
		public void Reset(int? nodeAddress = null) {
			_controllerService.Reset(MessageType, nodeAddress);
		}
		public void SwitchToPickingMode(int? nodeAddress = null) {
			_controllerService.SwitchToPickingMode(MessageType, nodeAddress);
		}
		public void SwitchToStockMode(int? nodeAddress = null) {
			_controllerService.SwitchToStockMode(MessageType, nodeAddress);
		}
		public void TurnBuzzerOff(int? nodeAddress = null) {
			_controllerService.TurnBuzzerOff(MessageType, nodeAddress);
		}
		public void TurnBuzzerOn(int? nodeAddress = null) {
			_controllerService.TurnBuzzerOn(MessageType, nodeAddress);
		}
		public void TurnLedOff(int? nodeAddress = null) {
			_controllerService.TurnLedOff(MessageType, nodeAddress);
		}
		public void TurnLedOn(int? nodeAddress = null) {
			_controllerService.TurnLedOn(MessageType, nodeAddress);
		}
		#endregion
	}
}