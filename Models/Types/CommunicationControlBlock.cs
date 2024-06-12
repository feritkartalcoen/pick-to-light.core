namespace PickToLight.Core.Models.Datas {
	using PickToLight.Core.Models.Enums;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public class CommunicationControlBlock {
		#region Properties
		public byte CommunicationControlBlockLength { get; private set; } = default;
		public List<byte> Data { get; set; } = new List<byte>();
		public bool HasData { get; set; } = true;
		public bool HasSubNode { get; set; } = true;
		public MessageType MessageType { get; set; }
		public SubCommand SubCommand { get; set; }
		public int? SubNode { get; set; }
		#endregion
		#region Methods
		public static CommunicationControlBlock FromBytes(byte[] communicationControlBlockBytes) {
			CommunicationControlBlock communicationControlBlock = new CommunicationControlBlock() {
				CommunicationControlBlockLength = communicationControlBlockBytes[0],
				MessageType = (MessageType)communicationControlBlockBytes[2],
				SubCommand = (SubCommand)communicationControlBlockBytes[6],
			};
			if (communicationControlBlockBytes.Length > 6) {
				communicationControlBlock.HasSubNode = true;
				communicationControlBlock.SubNode = communicationControlBlockBytes[7];
				if (communicationControlBlockBytes.Length > 7) {
					communicationControlBlock.HasData = true;
					communicationControlBlock.Data = communicationControlBlockBytes.Skip(8).ToList();
				}
			}
			return communicationControlBlock;
		}
		public static CommunicationControlBlock FromHexadecimalString(string communicationControlBlockString) {
			string[] communicationControlBlockByteStrings = communicationControlBlockString.Split(' ');
			byte[] communicationControlBlockBytes = communicationControlBlockByteStrings.Select(hexadecimalString => Convert.ToByte(hexadecimalString, 16)).ToArray();
			return FromBytes(communicationControlBlockBytes);
		}
		public byte[] ToBytes() {
			List<byte> bytes = new List<byte> { (byte)DefaultByte.Reserved, (byte)MessageType, (byte)DefaultByte.Reserved, (byte)DefaultByte.Reserved, (byte)DefaultByte.Reserved, (byte)SubCommand };
			if (HasSubNode) {
				SubNode = SubNode ?? (byte)DefaultByte.Broadcast;
				bytes.Add((byte)SubNode);
				if (HasData) {
					bytes.AddRange(Data);
				}
			}
			CommunicationControlBlockLength = (byte)(bytes.Count + 1);
			bytes.Insert(0, CommunicationControlBlockLength);
			return bytes.ToArray();
		}
		public string ToHexadecimalString() {
			return BitConverter.ToString(ToBytes()).Replace("-", " ");
		}
		#endregion
	}
}