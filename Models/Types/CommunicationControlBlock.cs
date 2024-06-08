using System;
using System.Collections.Generic;
using System.Linq;

namespace PickToLight.Core.Models.Datas {
	public class CommunicationControlBlock {
		public bool HasSubNode { get; set; } = true;
		public bool HasData { get; set; } = true;
		public byte CommunicationControlBlockLength { get; private set; } = default;
		public byte MessageType { get; set; }
		public byte SubCommand { get; set; }
		public int? SubNode { get; set; }
		public List<byte> Data { get; set; } = [];

		public void FromBytes(byte[] communicationControlBlockBytes) {
			CommunicationControlBlockLength = communicationControlBlockBytes[0];
			MessageType = communicationControlBlockBytes[1];
			SubCommand = communicationControlBlockBytes[5];
			if (communicationControlBlockBytes.Length > 6) {
				HasSubNode = true;
				SubNode = communicationControlBlockBytes[6];
				if (communicationControlBlockBytes.Length > 7) {
					HasData = true;
					Data = [.. communicationControlBlockBytes[7..]];
				}
			}
		}

		public void FromHexadecimalString(string communicationControlBlockString) {
			string[] communicationControlBlockByteStrings = communicationControlBlockString.Split(' ');
			byte[] communicationControlBlockBytes = communicationControlBlockByteStrings.Select(hexadecimalString => Convert.ToByte(hexadecimalString, 16)).ToArray();
			FromBytes(communicationControlBlockBytes);
		}

		public byte[] ToBytes() {
			List<byte> bytes = [0x00, MessageType, 0x00, 0x00, 0x00, SubCommand];
			if (HasSubNode) {
				SubNode ??= 0xFC;
				bytes.Add((byte)SubNode);
				if (HasData) {
					bytes.AddRange(Data);
				}
			}
			CommunicationControlBlockLength = (byte)(bytes.Count + 1);
			bytes.Insert(0, CommunicationControlBlockLength);
			return [.. bytes];
		}

		public string ToHexadecimalString() {
			return BitConverter.ToString(ToBytes()).Replace("-", " ");
		}
	}
}
