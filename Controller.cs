using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Utilities.Converters;
using System.Diagnostics;
using System.Net.Sockets;
namespace PickToLight.Core {
    public class Controller {
        public byte MessageType { get; set; } = 0x60;
        private TcpClient? _client;
        private NetworkStream? _stream;
        private void Write(byte ccbLength, byte subCommand, byte? subNode = null, byte[]? data = null) {
            int baseLength = 7 + (subNode == null ? 0 : 1);
            byte[] ccb = new byte[ccbLength];
            ccb[0] = (byte)ccbLength;
            ccb[1] = 0x00;
            ccb[2] = MessageType;
            ccb[3] = 0x00;
            ccb[4] = 0x00;
            ccb[5] = 0x00;
            ccb[6] = subCommand;
            if (subNode != null) {
                ccb[7] = (byte)subNode;
            }
            if (data != null) {
                Array.Copy(data, 0, ccb, baseLength, data.Length);
            }
            try {
                if (_stream != null && _stream.CanWrite) {
                    _stream.Write(ccb, 0, ccb.Length);
                    Debug.WriteLine($"Wrote: {CCBConverter.ToString(bytes: ccb)}");
                } else {
                    Debug.WriteLine("Cannot write to the network stream");
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception.Message);
            }
        }
        private void Read() {
            if (_stream!.DataAvailable) {
                byte ccbFirstByte = (byte)_stream.ReadByte();
                byte[] ccbRemainingBytes = new byte[ccbFirstByte - 1];
                _stream.Read(ccbRemainingBytes, 0, ccbRemainingBytes.Length);
                byte[] ccb = new byte[1 + ccbRemainingBytes.Length];
                ccb[0] = ccbFirstByte;
                Array.Copy(ccbRemainingBytes, 0, ccb, 1, ccbRemainingBytes.Length);
                Debug.WriteLine($"Read: {CCBConverter.ToString(bytes: ccb)}");
            }
        }
        public void Connect(string ip, int port) {
            try {
                if (_client == null || !_client.Connected) {
                    _client = new TcpClient(ip, port);
                    _stream = _client.GetStream();
                    Debug.WriteLine($"Connected to {ip}:{port}");
                } else {
                    Debug.WriteLine($"Already connected to {ip}:{port}");
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception.Message);
            }
        }
        public void Disconnect() {
            try {
                if (_client != null && _client.Connected && _stream != null) {
                    _stream.Close();
                    _stream = null;
                    _client.Close();
                    _client = null;
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
            byte[] valueBytes = ValueConverter.ToBytes(value);
            Array.Copy(valueBytes, 0, data, 0, valueBytes.Length);
            data[6] = 0x00;
            Write(ccbLength: 0x0F, subCommand: (byte)(!shouldFlash ? 0x00 : 0x10), subNode: subNode, data: data);
        }
        public void Clear(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x01, subNode: subNode);
        }
        public void TurnLedOn(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x02, subNode: subNode);
        }
        public void TurnLedOff(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x03, subNode: subNode);
        }
        public void TurnBuzzerOn(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x04, subNode: subNode);
        }
        public void TurnBuzzerOff(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x05, subNode: subNode);
        }
        public void Flash(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x11, subNode: subNode);
        }
        public void SetFlashingTimeInterval(byte subNode = 0xFc, FlashingTimeInterval interval = FlashingTimeInterval.QuarterSecond) {
            byte[] data = [0x00, (byte)interval];
            Write(ccbLength: 0x0A, subCommand: 0x12, subNode: subNode, data: data);
        }
        public void DisplayNodeAddress(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x13, subNode: subNode);
        }
        public void DisableShortageButton(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x15, subNode: subNode);
        }
        public void EnableShortageButton(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x16, subNode: subNode);
        }
        public void EmulateConfirmationButtonPressing(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x17, subNode: subNode);
        }
        public void EmulateShortageButtonPressing(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x18, subNode: subNode);
        }
        public void SwitchToStockMode(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x19, subNode: subNode);
        }
        public void SwitchToPickMode(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x1A, subNode: subNode);
        }
        public void DisableConfirmationButton(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x1B, subNode: subNode);
        }
        public void EnableConfirmationButton(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x1C, subNode: subNode);
        }
        public void SetAvailableDigitsForCounting(byte subNode = 0xFC, int availableDigits = 6) {
            byte[] data = [(byte)availableDigits];
            Write(ccbLength: 0x09, subCommand: 0x1E, subNode: subNode, data: data);
        }
        public void SetColor(Color color, byte subNode = 0xFC, bool shouldStore = false) {
            byte[] data = shouldStore ? [0x00, (byte)color, 0x55] : [0x00, (byte)color];
            Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void SetValidDigitsForCounting(byte subNode = 0xFC, ValidDigitsConfig? config = null) {
            config ??= new ValidDigitsConfig();
            byte validDigits = ConfigConverter.ValidDigitsConfigurationToByte(config);
            byte[] data = [0x01, validDigits];
            Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureMode(byte subNode = 0xFC, PickTagModeConfig? config = null, bool shouldStore = false) {
            config ??= new PickTagModeConfig();
            byte mode = ConfigConverter.PickTagModeConfigurationToByte(config);
            byte[] data = shouldStore ? [0x02, mode, 0x55] : [0x03, mode];
            Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void SetBlinkingTimeInterval(byte subNode = 0xFC, BlinkingTimeInterval interval = BlinkingTimeInterval.QuarterSecond) {
            byte[] data = [0x04, (byte)interval];
            Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        // TODO: Implement SetDigitsBlinkingTimeInterval
        public void SetDigitsBrightness(byte subNode = 0xFC, DigitsBrightnessConfig? config = null) {
            config ??= new DigitsBrightnessConfig();
            byte brightness = ConfigConverter.DigitsBrightnessConfigurationToByte(config);
            byte[] data = [0x06, brightness];
            Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureSpecialFunctionOne(byte subNode = 0xFC, SpecialFunctionOneConfig? config = null, bool shouldStore = false) {
            config ??= new SpecialFunctionOneConfig();
            byte configuration = ConfigConverter.SpecialFunctionOneConfigurationToByte(config);
            byte[] data = shouldStore ? [0x0A, configuration, 0x55] : [0x0A, configuration];
            Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void ConfigureSpecialFunctionTwo(byte subNode = 0xFC, SpecialFunctionTwoConfig? config = null, bool shouldStore = false) {
            config ??= new SpecialFunctionTwoConfig();
            byte configuration = ConfigConverter.SpecialFunctionTwoConfigurationToByte(config);
            byte[] data = shouldStore ? [0x0F, configuration, 0x55] : [0x0F, configuration];
            Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data);
        }
        public void GetDeviceFirmwareModelInformation(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0xFA, subNode: subNode);
        }
        public void GetDeviceDetailConfiguredInformation(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0xFC, subNode: subNode);
        }
        public void SetControllerPollingRange(byte pollingRange = 0xFA) {
            Write(ccbLength: 0x08, subCommand: 0x08, subNode: pollingRange);
        }
        public void GetConnectedPickTagsNodeAddresses() {
            Write(ccbLength: 0x07, subCommand: 0x09);
        }
        public void Reset(byte subNode = 0xFC) {
            Write(ccbLength: 0x08, subCommand: 0x14, subNode: subNode);
        }
        public void SetNodeAddress(byte oldSubNode, byte newSubNode) {
            byte[] data = [0x40, 0x1B, 0x1B, 0x10, newSubNode];
            Write(ccbLength: 0x0D, subCommand: 0x3A, subNode: oldSubNode, data: data);
        }
    }
}