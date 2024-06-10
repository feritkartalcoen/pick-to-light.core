using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Datas;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Services.Interfaces;
using PickToLight.Core.Utilities.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
namespace PickToLight.Core.Services {
	public class ControllerService : IControllerService {
		private CancellationTokenSource? _cancellationTokenSource;
		private NetworkStream? _networkStream;
		private TcpClient? _tcpClient;
		public event Action<CommunicationControlBlock>? OnRead;
		public ControllerService() {
			_tcpClient = new();
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
					OnRead!.Invoke(communicationControlBlock);
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
				} else {
					Debug.WriteLine($"No active connection to write.");
				}
			} catch (Exception exception) {
				Debug.WriteLine(exception.Message);
			}
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
		public List<int> OnConnectedPickTagsReceived(CommunicationControlBlock communicationControlBlock) {
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
			return nodeAddresses;
		}
		public void ChangeAvailableDigitsForCounting(MessageType messageType, int? nodeAddress, int availableDigitsCount) {
			List<byte> data = [(byte)availableDigitsCount];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangeAvailableDigitsForCounting,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeBlinkingTimeInterval(MessageType messageType, int? nodeAddress, BlinkingTimeInterval blinkingTimeInterval) {
			List<byte> data = [(byte)PickTagConfiguration.ChangeBlinkingTimeInterval, (byte)blinkingTimeInterval];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeColor(MessageType messageType, int? nodeAddress, Color color, bool shouldStore) {
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
			List<byte> data = [(byte)PickTagConfiguration.ChangeDigitsBrightness, digitsBrightnessConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangeFlashingTimeInterval(MessageType messageType, int? nodeAddress, FlashingTimeInterval flashingTimeInterval) {
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
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagConfigurationWithSpecialFunctionOne, specialFunctionOneConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagConfigurationWithSpecialFunctionTwo(MessageType messageType, int? nodeAddress, SpecialFunctionTwoConfiguration specialFunctionTwoConfiguration) {
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagConfigurationWithSpecialFunctionTwo, specialFunctionTwoConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagModeConfigurationPermanently(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration) {
			List<byte> data = [(byte)PickTagConfiguration.ChangePickTagModeConfigurationPermanently, pickTagModeConfiguration.ToByte(), (byte)DefaultByte.StoreIntoEEPROM];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void ChangePickTagModeConfigurationTemporarily(MessageType messageType, int? nodeAddress, PickTagModeConfiguration pickTagModeConfiguration) {
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
			List<byte> data = [(byte)PickTagConfiguration.ChangeValidDigitsForCounting, validDigitsConfiguration.ToByte()];
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.ChangePickTagConfiguration,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void Clear(MessageType messageType, int? nodeAddress) {
			Write(new CommunicationControlBlock() {
				MessageType = messageType,
				SubCommand = SubCommand.Clear,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void DisableConfirmationButton(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisableConfirmationButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void DisableShortageButton(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisableShortageButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void Display(MessageType messageType, string value, int? nodeAddress, bool shouldFlash) {
			List<byte> data = [.. ValueConverter.ToBytes(value), 0x00];
			Write(new() {
				MessageType = messageType,
				SubCommand = !shouldFlash ? SubCommand.Display : SubCommand.DisplayAndFlash,
				SubNode = nodeAddress,
				Data = data
			});
		}
		public void DisplayNodeAddress(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.DisplayNodeAddress,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EmulateConfirmationButtonPressing(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EmulateConfirmationButtonPressing,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EmulateShortageButtonPressing(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EmulateShortageButtonPressing,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EnableConfirmationButton(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EnableConfirmationButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void EnableShortageButton(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.EnableShortageButton,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void Flash(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.Flash,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void RequestConnectedPickTags(MessageType messageType) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestConnectedPickTagsAndOnConnectedPickTagsReceived,
				HasSubNode = false,
				HasData = false
			});
		}
		public void RequestPickTagDetails(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestPickTagDetailsAndOnPickTagDetailsReceived,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void RequestPickTagModel(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.RequestPickTagModelAndOnPickTagModelReceived,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void Reset(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.Reset,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void SwitchToPickingMode(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.SwitchToPickingMode,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void SwitchToStockMode(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.SwitchToStockMode,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnBuzzerOff(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnBuzzerOff,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnBuzzerOn(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnBuzzerOn,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnLedOff(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnLedOff,
				SubNode = nodeAddress,
				HasData = false
			});
		}
		public void TurnLedOn(MessageType messageType, int? nodeAddress) {
			Write(new() {
				MessageType = messageType,
				SubCommand = SubCommand.TurnLedOn,
				SubNode = nodeAddress,
				HasData = false
			});
		}
	}
}