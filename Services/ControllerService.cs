using PickToLight.Core.Models.Datas;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Services.Interfaces;
using PickToLight.Core.Utilities.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;

namespace PickToLight.Core.Services {
	public class ControllerService : IControllerService {
		private TcpClient? _tcpClient;
		private NetworkStream? _networkStream;
		public ControllerService() {
			_tcpClient = new();
		}

		public void Clear(ControllerPort controllerPort, int? nodeAddress) {
			byte subCommand = 0x01;
			Write(new CommunicationControlBlock() { HasData = false, MessageType = (byte)controllerPort, SubNode = nodeAddress, SubCommand = subCommand });
		}

		public bool Connect(string ipAddress, int port) {
			try {
				if (_tcpClient == null || !_tcpClient.Connected) {
					_tcpClient = new TcpClient(ipAddress, port);
					_networkStream = _tcpClient.GetStream();
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

		public void Display(ControllerPort controllerPort, string value, int? nodeAddress, bool shouldFlash) {
			byte subCommand = (byte)(shouldFlash ? 0x10 : 0x00);
			List<byte> data = [.. ValueConverter.ToBytes(value), 0x00];
			Write(new() { MessageType = (byte)controllerPort, SubNode = nodeAddress, Data = data, SubCommand = subCommand });
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
	}
}
