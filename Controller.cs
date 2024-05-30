using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Utilities.Converters;
using System.Diagnostics;
using System.Net.Sockets;
namespace PickToLight.Core {
    public class Controller {
        public byte MessageType { get; set; } = 0x60;
        public event Action<byte[]>? ReadAction;
        private TcpClient? _tcpClient;
        private NetworkStream? _networkStream;
        private CancellationTokenSource? _cancellationTokenSource;
        private void Write(byte communicationControlBlockLength, byte subCommand, byte? subNode = null, byte[]? data = null) {
            int baseLength = 7 + (subNode == null ? 0 : 1);
            byte[] communicationControlBlock = new byte[communicationControlBlockLength];
            communicationControlBlock[0] = (byte)communicationControlBlockLength;
            communicationControlBlock[1] = 0x00;
            communicationControlBlock[2] = MessageType;
            communicationControlBlock[3] = 0x00;
            communicationControlBlock[4] = 0x00;
            communicationControlBlock[5] = 0x00;
            communicationControlBlock[6] = subCommand;
            if (subNode != null) {
                communicationControlBlock[7] = (byte)subNode;
            }
            if (data != null) {
                Array.Copy(data, 0, communicationControlBlock, baseLength, data.Length);
            }
            try {
                if (_networkStream != null && _networkStream.CanWrite) {
                    _networkStream.Write(communicationControlBlock, 0, communicationControlBlock.Length);
                    Debug.WriteLine($"Wrote: {CommunicationControlBlockConverter.ToString(bytes: communicationControlBlock)}");
                } else {
                    Debug.WriteLine("Cannot write to the network stream");
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception.Message);
            }
        }
        private void Read() {
            if (_networkStream!.DataAvailable) {
                byte firstByteOfCommunicationControlBlock = (byte)_networkStream.ReadByte();
                byte[] remainingBytesOfCommunicationControlBlock = new byte[firstByteOfCommunicationControlBlock - 1];
                _networkStream.Read(remainingBytesOfCommunicationControlBlock, 0, remainingBytesOfCommunicationControlBlock.Length);
                byte[] communicationControlBlock = new byte[1 + remainingBytesOfCommunicationControlBlock.Length];
                communicationControlBlock[0] = firstByteOfCommunicationControlBlock;
                Array.Copy(remainingBytesOfCommunicationControlBlock, 0, communicationControlBlock, 1, remainingBytesOfCommunicationControlBlock.Length);
                Debug.WriteLine($"Read: {CommunicationControlBlockConverter.ToString(bytes: communicationControlBlock)}");
                ReadInvoke(communicationControlBlock);
            }
        }
        private void ReadContinuously(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                Read();
            }
        }
        private void ReadInvoke(byte[] communicationControlBlock) {
            ReadAction!.Invoke(communicationControlBlock);
        }
        public void Connect(string ipAddress, int port) {
            try {
                if (_tcpClient == null || !_tcpClient.Connected) {
                    _tcpClient = new TcpClient(ipAddress, port);
                    _networkStream = _tcpClient.GetStream();
                    _cancellationTokenSource = new CancellationTokenSource();
                    Task.Run(() => ReadContinuously(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
                    Debug.WriteLine($"Connected to {ipAddress}:{port}");
                    Debug.WriteLine($"Read thread started");
                } else {
                    Debug.WriteLine($"Already connected to {ipAddress}:{port}");
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception.Message);
            }
        }
        public void Disconnect() {
            try {
                if (_tcpClient != null && _tcpClient.Connected && _networkStream != null) {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = null;
                    Debug.WriteLine($"Read thread finished");
                    _networkStream.Close();
                    _networkStream = null;
                    _tcpClient.Close();
                    _tcpClient = null;
                    Debug.WriteLine("Disconnected");
                } else {
                    Debug.WriteLine("No active connection to disconnect");
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception.Message);
            }
        }
        public void Display(string value, byte subNode = 0xFC, bool shouldFlash = false) {
            byte[] data = new byte[7];
            byte[] valueBytes = ValueConverter.StringDisplayValueToBytes(value);
            Array.Copy(valueBytes, 0, data, 0, valueBytes.Length);
            data[6] = 0x00;
            Write(communicationControlBlockLength: 0x0F, subCommand: (byte)(!shouldFlash ? 0x00 : 0x10), subNode: subNode, data: data);
        }
        public void Clear(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x01, subNode: subNode);
        }
        public void TurnLedOn(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x02, subNode: subNode);
        }
        public void TurnLedOff(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x03, subNode: subNode);
        }
        public void TurnBuzzerOn(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x04, subNode: subNode);
        }
        public void TurnBuzzerOff(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x05, subNode: subNode);
        }
        public void Flash(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x11, subNode: subNode);
        }
        public void SetFlashingTimeInterval(byte subNode = 0xFc, FlashingTimeInterval flashingTimeInterval = FlashingTimeInterval.QuarterSecond) {
            byte[] data = [0x00, (byte)flashingTimeInterval];
            Write(communicationControlBlockLength: 0x0A, subCommand: 0x12, subNode: subNode, data: data);
        }
        public void DisplayNodeAddress(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x13, subNode: subNode);
        }
        public void DisableShortageButton(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x15, subNode: subNode);
        }
        public void EnableShortageButton(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x16, subNode: subNode);
        }
        public void EmulateConfirmationButtonPressing(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x17, subNode: subNode);
        }
        public void EmulateShortageButtonPressing(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x18, subNode: subNode);
        }
        public void SwitchToStockMode(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x19, subNode: subNode);
        }
        public void SwitchToPickMode(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x1A, subNode: subNode);
        }
        public void DisableConfirmationButton(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x1B, subNode: subNode);
        }
        public void EnableConfirmationButton(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x1C, subNode: subNode);
        }
        public void SetAvailableDigitsForCounting(byte subNode = 0xFC, int availableDigits = 6) {
            byte[] data = [(byte)availableDigits];
            Write(communicationControlBlockLength: 0x09, subCommand: 0x1E, subNode: subNode, data: data);
        }
        public void SetColor(Color color, byte subNode = 0xFC, bool shouldStore = false) {
            byte[] data = shouldStore ? [0x00, (byte)color, 0x55] : [0x00, (byte)color];
            Write(communicationControlBlockLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void SetValidDigitsForCounting(byte subNode = 0xFC, ValidDigitsConfiguration? validDigitsConfiguration = null) {
            validDigitsConfiguration ??= new ValidDigitsConfiguration();
            byte validDigits = ConfigurationConverter.ValidDigitsConfigurationToByte(validDigitsConfiguration);
            byte[] data = [0x01, validDigits];
            Write(communicationControlBlockLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureMode(byte subNode = 0xFC, PickTagModeConfiguration? pickTagModeConfiguration = null, bool shouldStore = false) {
            pickTagModeConfiguration ??= new PickTagModeConfiguration();
            byte mode = ConfigurationConverter.PickTagModeConfigurationToByte(pickTagModeConfiguration);
            byte[] data = shouldStore ? [0x02, mode, 0x55] : [0x03, mode];
            Write(communicationControlBlockLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void SetBlinkingTimeInterval(byte subNode = 0xFC, BlinkingTimeInterval blinkingTimeInterval = BlinkingTimeInterval.QuarterSecond) {
            byte[] data = [0x04, (byte)blinkingTimeInterval];
            Write(communicationControlBlockLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        // TODO: Implement SetDigitsBlinkingTimeInterval
        public void SetDigitsBrightness(byte subNode = 0xFC, DigitsBrightnessConfiguration? digitsBrightnessConfiguration = null) {
            digitsBrightnessConfiguration ??= new DigitsBrightnessConfiguration();
            byte brightness = ConfigurationConverter.DigitsBrightnessConfigurationToByte(digitsBrightnessConfiguration);
            byte[] data = [0x06, brightness];
            Write(communicationControlBlockLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureSpecialFunctionOne(byte subNode = 0xFC, SpecialFunctionOneConfiguration? specialFunctionOneConfiguration = null, bool shouldStore = false) {
            specialFunctionOneConfiguration ??= new SpecialFunctionOneConfiguration();
            byte configuration = ConfigurationConverter.SpecialFunctionOneConfigurationToByte(specialFunctionOneConfiguration);
            byte[] data = shouldStore ? [0x0A, configuration, 0x55] : [0x0A, configuration];
            Write(communicationControlBlockLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureSpecialFunctionTwo(byte subNode = 0xFC, SpecialFunctionTwoConfiguration? specialFunctionTwoConfiguration = null, bool shouldStore = false) {
            specialFunctionTwoConfiguration ??= new SpecialFunctionTwoConfiguration();
            byte configuration = ConfigurationConverter.SpecialFunctionTwoConfigurationToByte(specialFunctionTwoConfiguration);
            byte[] data = shouldStore ? [0x0F, configuration, 0x55] : [0x0F, configuration];
            Write(communicationControlBlockLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void GetDeviceFirmwareModelInformation(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0xFA, subNode: subNode);
        }
        public void GetDeviceDetailConfiguredInformation(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0xFC, subNode: subNode);
        }
        public void SetControllerPollingRange(byte pollingRange = 0xFA) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x08, subNode: pollingRange);
        }
        public void GetDeviceStatus() {
            Write(communicationControlBlockLength: 0x07, subCommand: 0x09);
        }
        public void Reset(byte subNode = 0xFC) {
            Write(communicationControlBlockLength: 0x08, subCommand: 0x14, subNode: subNode);
        }
        public void SetNodeAddress(byte oldSubNode, byte newSubNode) {
            byte[] data = [0x40, 0x1B, 0x1B, 0x10, newSubNode];
            Write(communicationControlBlockLength: 0x0D, subCommand: 0x3A, subNode: oldSubNode, data: data);
        }
    }
}