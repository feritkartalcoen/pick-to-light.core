namespace PickToLight.Core.Services {
	using PickToLight.Core.Models;
	using PickToLight.Core.Models.Configurations;
	using PickToLight.Core.Models.Datas;
	using PickToLight.Core.Models.Enums;
	using PickToLight.Core.Services.Interfaces;
	using PickToLight.Core.Utilities.Converters;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Net.Sockets;
	using System.Threading;
	public class ControllerService(List<PickTag> pickTags) : IControllerService {
		#region Fields
		private CancellationTokenSource? _cancellationTokenSource;
		private NetworkStream? _networkStream;
		private TcpClient? _tcpClient = new();
		#endregion
		#region Delegates
		public delegate void OnUpdate();
		#endregion
		#region Events
		public event OnUpdate? Update;
		#endregion
		#region Methods
		public void ChangeAvailableDigitsForCounting(MessageType messageType, int? nodeAddress, int availableDigitsCount) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeAvailableDigitsForCounting(availableDigitsCount);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeAvailableDigitsForCounting(availableDigitsCount);
				});
			}
			List<byte> data = [(byte)availableDigitsCount];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangeAvailableDigitsForCounting,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeBlinkingTimeInterval(MessageType messageType, int? nodeAddress, BlinkingTimeInterval blinkingTimeInterval) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeBlinkingTimeInterval(blinkingTimeInterval);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeBlinkingTimeInterval(blinkingTimeInterval);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangeBlinkingTimeInterval, (byte)blinkingTimeInterval];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeColor(MessageType messageType, int? nodeAddress, Color color, bool shouldStore) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeColor(color);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeColor(color);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangeColor, (byte)color];
			if (shouldStore) data.Add((byte)DefaultByte.StoreIntoEEPROM);
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeDigitsBrightness(MessageType messageType, int? nodeAddress, DigitsBrightnessConfiguration digitsBrightnessConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeDigitsBrightness(digitsBrightnessConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeDigitsBrightness(digitsBrightnessConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangeDigitsBrightness, digitsBrightnessConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeFlashingTimeInterval(MessageType messageType, int? nodeAddress, FlashingTimeInterval flashingTimeInterval) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeFlashingTimeInterval(flashingTimeInterval);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeFlashingTimeInterval(flashingTimeInterval);
				});
			}
			List<byte> data = [(byte)DefaultByte.Reserved, (byte)flashingTimeInterval];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangeFlashingTimeInterval,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeNodeAddress(MessageType messageType, int nodeAddress, int newNodeAddress) {
			List<byte> data = [(byte)DefaultByte.ReservedUnknown40, (byte)DefaultByte.ReservedUnknown1B, (byte)DefaultByte.ReservedUnknown1B, (byte)DefaultByte.ReservedUnknown10, (byte)newNodeAddress];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangeNodeAddress,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagConfigurationWithSpecialFunctionOne(MessageType messageType, int? nodeAddress, SpecialFunctionOneConfiguration specialFunctionOneConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangePickTagConfigurationWithSpecialFunctionOne(specialFunctionOneConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangePickTagConfigurationWithSpecialFunctionOne(specialFunctionOneConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagConfigurationWithSpecialFunctionOne, specialFunctionOneConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(MessageType messageType, int? nodeAddress, SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangePickTagConfigurationWithSpecialFunctionTwo(specialFunctionTwoConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangePickTagConfigurationWithSpecialFunctionTwo(specialFunctionTwoConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagConfigurationWithSpecialFunctionTwo, specialFunctionTwoConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagModeConfigurationPermanently(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangePickTagModeConfiguration(pickTagModeConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangePickTagModeConfiguration(pickTagModeConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagModeConfigurationPermanently, pickTagModeConfiguration.ToByte(), (byte)DefaultByte.StoreIntoEEPROM];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagModeConfigurationTemporarily(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangePickTagModeConfiguration(pickTagModeConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangePickTagModeConfiguration(pickTagModeConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagModeConfigurationTemporarily, pickTagModeConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePollingRange(MessageType messageType, int pollingRange) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePollingRange,
				SubNode = pollingRange,
				HasData = false
			});
		}
		public void ChangeValidDigitsForCounting(MessageType messageType, int? nodeAddress, ValidDigitsConfiguration validDigitsConfiguration) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).ChangeValidDigitsForCounting(validDigitsConfiguration);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.ChangeValidDigitsForCounting(validDigitsConfiguration);
				});
			}
			List<byte> data = [(byte)PickTagConfiguration.ChangeValidDigitsForCounting, validDigitsConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void Clear(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).Clear();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.Clear();
				});
			}
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.Clear,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public bool Connect(string ipAddress, int port) {
			try {
				if (_tcpClient == null || !_tcpClient.Connected) {
					_tcpClient = new TcpClient(ipAddress, port);
					_networkStream = _tcpClient.GetStream();
					_cancellationTokenSource = new();
					new Thread(() => ReadContinuously(_cancellationTokenSource.Token)).Start();
					Debug.WriteLine($"Connected to {ipAddress}:{port}.");
				} else {
					Debug.WriteLine($"Already connected to {ipAddress}:{port}.");
				}
				return true;
			} catch (Exception exception) {
				Debug.WriteLine(exception.Message);
				return false;
			}
		}
		public void DisableConfirmationButton(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).DisableConfirmationButton();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.DisableConfirmationButton();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisableConfirmationButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void DisableShortageButton(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).DisableShortageButton();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.DisableShortageButton();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisableShortageButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public bool Disconnect() {
			try {
				if (_tcpClient != null && _tcpClient.Connected && _networkStream != null) {
					_cancellationTokenSource?.Cancel();
					_cancellationTokenSource = null;
					_tcpClient.Close();
					_tcpClient = null;
					_networkStream.Close();
					_networkStream = null;
					Debug.WriteLine($"Disconnected");
				} else {
					Debug.WriteLine($"No active connection to disconnect.");
				}
				return true;
			} catch (Exception exception) {
				Debug.WriteLine(exception.Message);
				return false;
			}
		}
		public void Display(MessageType messageType, int? nodeAddress, string value, bool shouldFlash) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).Display(value, shouldFlash);
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.Display(value, shouldFlash);
				});
			}
			List<byte> data = [.. ValueConverter.ToBytes(value), 0x00];
			Write(new() {
				MessageType = messageType,
				SubCommand = !shouldFlash ? SubCommand.Display : SubCommand.DisplayAndFlash,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void DisplayNodeAddress(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).DisplayNodeAddress();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.DisplayNodeAddress();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisplayNodeAddress,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EmulateConfirmationButtonPressing(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).EmulateConfirmationButtonPressing();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.EmulateConfirmationButtonPressing();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EmulateConfirmationButtonPressing,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EmulateShortageButtonPressing(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).EmulateShortageButtonPressing();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.EmulateShortageButtonPressing();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EmulateShortageButtonPressing,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EnableConfirmationButton(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).EnableConfirmationButton();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.EnableConfirmationButton();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EnableConfirmationButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EnableShortageButton(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).EnableShortageButton();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.EnableShortageButton();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EnableShortageButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void Flash(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).Flash();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.Flash();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.Flash,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void OnButtonsLocked(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnButtonsLocked)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void OnConfirmationButtonPressed(CommunicationControlBlock communicationControlBlock) {
			List<byte> data = communicationControlBlock.Data;
			data.RemoveAt(communicationControlBlock.Data.Count - 1);
			byte[] valueBytes = [.. data];
			string value = ValueConverter.ToString(valueBytes);
			PickTag.GetPickTag(communicationControlBlock.SubNode, pickTags).OnConfirmationButtonPressed(value);
		}
		public void OnConnectedPickTagsReceived(CommunicationControlBlock communicationControlBlock) {
			byte[] dataOfNodeAddresses = [.. communicationControlBlock.Data[3..]];
			List<int> nodeAddresses = [];
			for (int byteIndex = 0; byteIndex < dataOfNodeAddresses.Length; byteIndex++) {
				byte currentByte = dataOfNodeAddresses[byteIndex];
				for (int bitIndex = 0; bitIndex < 8; bitIndex++) {
					if ((currentByte & (1 << bitIndex)) == 0) {
						int nodeAddress = (byteIndex * 8 + bitIndex + 1);
						nodeAddresses.Add(nodeAddress);
					}
				}
			}
			pickTags.ForEach((pickTag) => {
				if (nodeAddresses.Contains(pickTag.NodeAddress)) {
					pickTag.Connect();
				} else {
					pickTag.Disconnect();
				}
			});
		}
		public void OnIllegal(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnIllegal)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void OnMalfunction(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnMalfunction)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void OnOldPickTagResetOrConnect(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnOldPickTagResetOrConnect)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void OnQuantityInStockReceived(CommunicationControlBlock communicationControlBlock) {
			List<byte> data = communicationControlBlock.Data;
			data.RemoveAt(communicationControlBlock.Data.Count - 1);
			byte[] valueBytes = [.. data];
			string value = ValueConverter.ToString(valueBytes);
			PickTag.GetPickTag(communicationControlBlock.SubNode, pickTags).OnQuantityInStockReceived(value);
		}
		public void OnShortageButtonPressed(CommunicationControlBlock communicationControlBlock) {
			List<byte> data = communicationControlBlock.Data;
			data.RemoveAt(communicationControlBlock.Data.Count - 1);
			byte[] valueBytes = [.. data];
			string value = ValueConverter.ToString(valueBytes);
			PickTag.GetPickTag(communicationControlBlock.SubNode, pickTags).OnShortageButtonPressed(value);
		}
		public void OnSpecialReceived(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnSpecialReceived)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void OnTimeout(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"{nameof(OnTimeout)} on {nameof(ControllerService)} is not implemented yet.");
		}
		public void RequestConnectedPickTags(MessageType messageType) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestConnectedPickTagsAndConnectedPickTagsReceived,
				HasSubNode = false,
				HasData = false
			});
		}
		public void RequestPickTagDetails(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).RequestPickTagDetails();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.RequestPickTagDetails();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestPickTagDetailsAndOnPickTagDetailsReceived,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void RequestPickTagModel(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).RequestPickTagModel();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.RequestPickTagModel();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestPickTagModelAndOnPickTagModelReceived,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void Reset(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).Reset();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.Reset();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.Reset,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void SwitchToPickingMode(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).SwitchToPickingMode();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.SwitchToPickingMode();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.SwitchToPickingMode,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void SwitchToStockMode(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).SwitchToStockMode();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.SwitchToStockMode();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.SwitchToStockMode,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnBuzzerOff(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).TurnBuzzerOff();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.TurnBuzzerOff();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnBuzzerOff,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnBuzzerOn(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).TurnBuzzerOn();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.TurnBuzzerOn();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnBuzzerOn,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnLedOff(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).TurnLedOff();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.TurnLedOff();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnLedOff,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnLedOn(MessageType messageType, int? nodeAddress) {
			if (nodeAddress != null) {
				PickTag.GetPickTag(nodeAddress, pickTags).TurnLedOn();
			} else {
				PickTag.GetPickTags(pickTags).ToList().ForEach(pickTag => {
					pickTag.TurnLedOn();
				});
			}
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnLedOn,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		private void OnRead(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"<= {communicationControlBlock.ToHexadecimalString()}");
			switch (communicationControlBlock.SubCommand) {
				case SubCommand.ButtonsLocked: OnButtonsLocked(communicationControlBlock); break;
				case SubCommand.ConfirmationButtonPressed: OnConfirmationButtonPressed(communicationControlBlock); break;
				case SubCommand.RequestConnectedPickTagsAndConnectedPickTagsReceived: OnConnectedPickTagsReceived(communicationControlBlock); break;
				case SubCommand.Illegal: OnIllegal(communicationControlBlock); break;
				case SubCommand.Malfunction: OnMalfunction(communicationControlBlock); break;
				case SubCommand.OldPickTagResetOrConnect: OnOldPickTagResetOrConnect(communicationControlBlock); break;
				case SubCommand.QuantityInStockReceived: OnQuantityInStockReceived(communicationControlBlock); break;
				case SubCommand.ShortageButtonPressed: OnShortageButtonPressed(communicationControlBlock); break;
				case SubCommand.SpecialReceived: OnSpecialReceived(communicationControlBlock); break;
				case SubCommand.Timeout: OnTimeout(communicationControlBlock); break;
				default: break;
			}
			Update?.Invoke();
		}
		private void Read() {
			try {
				if (_networkStream!.DataAvailable) {
					List<byte> bytes = [];
					byte firstByte = (byte)_networkStream.ReadByte();
					bytes.Add(firstByte);
					for (int i = 0; i < firstByte - 1; i++) {
						byte nextByte = (byte)_networkStream.ReadByte();
						bytes.Add(nextByte);
					}
					CommunicationControlBlock communicationControlBlock = CommunicationControlBlock.FromBytes([.. bytes]);
					OnRead(communicationControlBlock);
				}
			} catch (Exception exception) {
				Debug.WriteLine(exception.Message);
			}
		}
		private void ReadContinuously(CancellationToken cancellationToken) {
			while (!cancellationToken.IsCancellationRequested) {
				Read();
			}
		}
		private void Write(CommunicationControlBlock communicationControlBlock) {
			try {
				if (_networkStream != null && _networkStream.CanWrite) {
					_networkStream.Write(communicationControlBlock.ToBytes(), 0, communicationControlBlock.CommunicationControlBlockLength);
					Debug.WriteLine($"=> {communicationControlBlock.ToHexadecimalString()}");
					Update?.Invoke();
				} else {
					Debug.WriteLine($"No active connection to write.");
				}
			} catch (Exception exception) {
				Debug.WriteLine(exception.Message);
			}
		}
		#endregion
	}
}