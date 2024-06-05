using PickToLight.Core.Models.Configurations;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Utilities.Converters;
using System.Net.Sockets;
using System.Text;
namespace PickToLight.Core {
    public class Controller {
        public byte MessageType { get; set; } = 0x60;
        private TcpClient? _client;
        private NetworkStream? _stream;
        public event Action<byte[]>? ReadAction;
        private CancellationTokenSource? _cancellationTokenSource;
        #region Connection Methods
        public string Connect(string ip, int port) {
            try {
                if (_client == null || !_client.Connected) {
                    _client = new TcpClient(ip, port);
                    _stream = _client.GetStream();
                    _cancellationTokenSource = new CancellationTokenSource();
                    Task.Run(() => ReadContinuously(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
                    return ($"Connected to {ip}:{port}.");
                } else {
                    return ($"Already connected to {ip}:{port}.");
                }
            } catch (Exception exception) {
                return (exception.Message);
            }
        }
        public string Disconnect() {
            try {
                if (_client != null && _client.Connected && _stream != null) {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = null;
                    _stream.Close();
                    _stream = null;
                    _client.Close();
                    _client = null;
                    return ("Disconnected.");
                } else {
                    return ("No active connection to disconnect.");
                }
            } catch (Exception exception) {
                return (exception.Message);
            }
        }
        #endregion
        #region Write Methods
        private string Write(byte ccbLength, byte subCommand, byte? subNode = null, byte[]? data = null, string result = "") {
            List<byte> ccbList = [ccbLength, 0x00, MessageType, 0x00, 0x00, 0x00, subCommand];
            if (subNode != null) ccbList.Add((byte)subNode);
            if (data != null) ccbList.AddRange(data);
            byte[] ccb = [.. ccbList];
            try {
                if (_stream != null && _stream.CanWrite) {
                    _stream.Write(ccb, 0, ccb.Length);
                    return result == "" ? ($"Wrote: {CCBConverter.ToString(bytes: ccb)}.") : result;
                } else {
                    return ("Cannot write to the network stream.");
                }
            } catch (Exception exception) {
                return (exception.Message);
            }
        }
        public string Display(string value, byte subNode = 0xFC, bool shouldFlash = false) {
            byte[] data = new byte[7];
            byte[] valueBytes = ValueConverter.ToBytes(value);
            Array.Copy(valueBytes, 0, data, 0, valueBytes.Length);
            data[6] = 0x00;
            return Write(ccbLength: 0x0F, subCommand: (byte)(!shouldFlash ? 0x00 : 0x10), subNode: subNode, data: data, result: $"Displayed {value}.");
        }
        public string Clear(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x01, subNode: subNode, result: $"Cleared data.");
        }
        public string TurnLedOn(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x02, subNode: subNode, result: $"Turned LED on.");
        }
        public string TurnLedOff(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x03, subNode: subNode, result: $"Turned LED off.");
        }
        public string TurnBuzzerOn(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x04, subNode: subNode, result: $"Turned buzzer on.");
        }
        public string TurnBuzzerOff(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x05, subNode: subNode, result: $"Turned buzzer off.");
        }
        public string Flash(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x11, subNode: subNode, result: $"Flashing.");
        }
        public string SetFlashingTimeInterval(byte subNode = 0xFc, FlashingTimeInterval interval = FlashingTimeInterval.QuarterSecond) {
            byte[] data = [0x00, (byte)interval];
            return Write(ccbLength: 0x0A, subCommand: 0x12, subNode: subNode, data: data, result: $"Flashing time interval changed to {interval.ToString().ToLowerInvariant()}.");
        }
        public string DisplayNodeAddress(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x13, subNode: subNode, result: $"Displayed node address.");
        }
        public string DisableShortageButton(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x15, subNode: subNode, result: $"Shortage button disabled.");
        }
        public string EnableShortageButton(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x16, subNode: subNode, result: $"Shortage button enabled.");
        }
        public string EmulateConfirmationButtonPressing(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x17, subNode: subNode, result: $"Emulated confirmation button pressing.");
        }
        public string EmulateShortageButtonPressing(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x18, subNode: subNode, result: $"Emulated shortage button pressing.");
        }
        public string SwitchToStockMode(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x19, subNode: subNode, result: $"Switched to stock mode.");
        }
        public string SwitchToPickMode(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x1A, subNode: subNode, result: $"Switched to pick mode.");
        }
        public string DisableConfirmationButton(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x1B, subNode: subNode, result: $"Confirmation button disabled.");
        }
        public string EnableConfirmationButton(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x1C, subNode: subNode, result: $"Confirmation button enabled.");
        }
        public string SetAvailableDigitsForCounting(byte subNode = 0xFC, int availableDigits = 6) {
            byte[] data = [(byte)availableDigits];
            return Write(ccbLength: 0x09, subCommand: 0x1E, subNode: subNode, data: data, result: $"Available digits for counting are set to {availableDigits}.");
        }
        public string SetColor(Color color, byte subNode = 0xFC, bool shouldStore = false) {
            byte[] data = shouldStore ? [0x00, (byte)color, 0x55] : [0x00, (byte)color];
            return Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Color changed to {color.ToString().ToLower()}.");
        }
        public string SetValidDigitsForCounting(byte subNode = 0xFC, ValidDigitsConfig? config = null) {
            config ??= new ValidDigitsConfig();
            byte validDigits = ConfigConverter.ValidDigitsConfigurationToByte(config);
            byte[] data = [0x01, validDigits];
            return Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Valid digits for counting are set.");
        }
        public string ConfigureMode(byte subNode = 0xFC, PickTagModeConfig? config = null, bool shouldStore = false) {
            config ??= new PickTagModeConfig();
            byte mode = ConfigConverter.PickTagModeConfigurationToByte(config);
            byte[] data = shouldStore ? [0x02, mode, 0x55] : [0x03, mode];
            return Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Pick tag mode is configured.");
        }
        public string SetBlinkingTimeInterval(byte subNode = 0xFC, BlinkingTimeInterval interval = BlinkingTimeInterval.QuarterSecond) {
            byte[] data = [0x04, (byte)interval];
            return Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Blinking time interval changed to {interval.ToString().ToLowerInvariant()}.");
        }
        // TODO: Implement SetDigitsBlinkingTimeInterval.
        public string SetDigitsBrightness(byte subNode = 0xFC, DigitsBrightnessConfig? config = null) {
            config ??= new DigitsBrightnessConfig();
            byte brightness = ConfigConverter.DigitsBrightnessConfigurationToByte(config);
            byte[] data = [0x06, brightness];
            return Write(ccbLength: 0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Digits brightness are changed.");
        }
        public string ConfigureSpecialFunctionOne(byte subNode = 0xFC, SpecialFunctionOneConfig? config = null, bool shouldStore = false) {
            config ??= new SpecialFunctionOneConfig();
            byte configuration = ConfigConverter.SpecialFunctionOneConfigurationToByte(config);
            byte[] data = shouldStore ? [0x0A, configuration, 0x55] : [0x0A, configuration];
            return Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Special function 1 is configured.");
        }
        public string ConfigureSpecialFunctionTwo(byte subNode = 0xFC, SpecialFunctionTwoConfig? config = null, bool shouldStore = false) {
            config ??= new SpecialFunctionTwoConfig();
            byte configuration = ConfigConverter.SpecialFunctionTwoConfigurationToByte(config);
            byte[] data = shouldStore ? [0x0F, configuration, 0x55] : [0x0F, configuration];
            return Write(ccbLength: shouldStore ? (byte)0x0B : (byte)0x0A, subCommand: 0x1F, subNode: subNode, data: data, result: $"Special function 2 is configured.");
        }
        public string GetDeviceFirmwareModelInformation(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0xFA, subNode: subNode, result: $"Getting device firmware model information.");
        }
        public string GetDeviceDetailConfiguredInformation(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0xFC, subNode: subNode, result: $"Getting device detail configured information.");
        }
        public string SetControllerPollingRange(byte pollingRange = 0xFA) {
            return Write(ccbLength: 0x08, subCommand: 0x08, subNode: pollingRange, result: $"Controller polling range is set to {pollingRange}.");
        }
        public string GetConnectedPickTagsNodeAddresses() {
            return Write(ccbLength: 0x07, subCommand: 0x09, result: $"Getting connected pick tags.");
        }
        public string Reset(byte subNode = 0xFC) {
            return Write(ccbLength: 0x08, subCommand: 0x14, subNode: subNode, result: $"Pick tags resetted.");
        }
        public string SetNodeAddress(byte oldSubNode, byte newSubNode) {
            byte[] data = [0x40, 0x1B, 0x1B, 0x10, newSubNode];
            return Write(ccbLength: 0x0D, subCommand: 0x3A, subNode: oldSubNode, data: data, result: $"Changed {oldSubNode} to {newSubNode}.");
        }
        #endregion
        #region Read Methods
        private void Read() {
            if (_stream!.DataAvailable) {
                List<byte> ccb = [];
                byte ccbFirstByte = (byte)_stream.ReadByte();
                ccb.Add(ccbFirstByte);
                for (int i = 0; i < ccbFirstByte - 1; i++) {
                    byte nextByte = (byte)_stream.ReadByte();
                    ccb.Add(nextByte);
                }
                ReadInvoke([.. ccb]);
            }
        }
        public static string ReadControl(byte[] cbb) {
            byte subCommand = cbb[6];
            byte subNode = cbb[7];
            byte[]? data = cbb.Length > 8 ? cbb[8..] : null;
            return subCommand switch {
                0x06 => ReadConfirmationButtonPressing(subNode: subNode, data: data!),
                0x07 => ReadShortageButtonPressing(subNode: subNode, data: data!),
                0x09 => ReadConnectedPickTagsNodeAddresses(data: data!),
                0x0A => ReadTimeout(subNode: subNode),
                0x0B => ReadMalfunction(subNode: subNode),
                0x0C => ReadIllegalCommand(subNode: subNode),
                0x0D => ReadConfirmationButtonLocked(subNode: subNode),
                0x0E => ReadOldDeviceResettedOrConnected(subNode: subNode),
                0x0F => ReadQuantity(subNode: subNode, data: data!),
                0xFA => ReadPickTagModelName(subNode: subNode, data: data!),
                0xFC => ReadDeviceDetailConfiguredInformation(subNode: subNode/*, data: data!*/),
                /*0x64 => ReadSpecial(subNode: subNode, data: data!),*/
                _ => "This return message is not implemented yet",
            };
        }
        private static string ReadConfirmationButtonPressing(byte subNode, byte[] data) {
            byte[] valueBytes = new byte[6];
            Array.Copy(data, 0, valueBytes, 0, 6);
            string value = ValueConverter.ToString(valueBytes);
            return $"Confirmation button pressed on {subNode:D3} with {value}";
        }
        private static string ReadShortageButtonPressing(byte subNode, byte[] data) {
            byte[] valueBytes = new byte[6];
            Array.Copy(data, 0, valueBytes, 0, 6);
            string value = ValueConverter.ToString(valueBytes);
            return $"Shortage button pressed on {subNode:D3} with {value}";
        }
        private static string ReadTimeout(byte subNode) {
            return $"Timeout on {subNode:D3}";
        }
        private static string ReadMalfunction(byte subNode) {
            return $"Malfunction on {subNode:D3}";
        }
        private static string ReadIllegalCommand(byte subNode) {
            return $"IllegalCommand on {subNode:D3}";
        }
        private static string ReadConfirmationButtonLocked(byte subNode) {
            return $"Confirmation Button Locked on {subNode:D3}";
        }
        private static string ReadOldDeviceResettedOrConnected(byte subNode) {
            return $"{subNode:D3} resetted / connected";
        }
        private static string ReadQuantity(byte subNode, byte[] data) {
            byte[] valueBytes = new byte[6];
            Array.Copy(data, 0, valueBytes, 0, 6);
            string value = ValueConverter.ToString(valueBytes);
            return $"Quantity on {subNode:D3} is {value}";
        }
        private static string ReadConnectedPickTagsNodeAddresses(byte[] data) {
            byte[] dataOfNodeAddresses = data[3..];
            List<byte> nodeAddresses = [];
            for (int byteIndex = 0; byteIndex < dataOfNodeAddresses.Length; byteIndex++) {
                byte currentByte = dataOfNodeAddresses[byteIndex];
                for (int bitIndex = 0; bitIndex < 8; bitIndex++) {
                    if ((currentByte & (1 << bitIndex)) == 0) {
                        byte nodeAddress = (byte)(byteIndex * 8 + bitIndex + 1);
                        nodeAddresses.Add(nodeAddress);
                    }
                }
            }
            return $"Connected pick tags: {string.Join(", ", nodeAddresses)}";
        }
        private static string ReadDeviceDetailConfiguredInformation(byte subNode/*, byte[] data*/) {
            // Will be implemented for full functionality in the future. Right now only returning device status.
            return $"{subNode:D3} resetted / connected";
        }
        // TODO: Implement ReadSpecial.
        private static string ReadPickTagModelName(byte subNode, byte[] data) {
            string value = Encoding.ASCII.GetString(data);
            return $"Pick tag {subNode:D3} is {value}";
        }
        private void ReadContinuously(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                Read();
            }
        }
        private void ReadInvoke(byte[] ccb) {
            ReadAction!.Invoke(ccb);
        }
        #endregion
    }
}