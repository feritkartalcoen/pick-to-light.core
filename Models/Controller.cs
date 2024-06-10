using PickToLight.Core.Models.Datas;
using PickToLight.Core.Models.Enums;
using PickToLight.Core.Services;
using System.Collections.Generic;
using System.Diagnostics;
namespace PickToLight.Core.Models {
	public class Controller {
		private readonly ControllerService _controllerService;
		public bool IsConnected { get; private set; } = false;
		public ControllerModel ControllerModel { get; init; }
		public MessageType MessageType { get; set; } = MessageType.Port1;
		public int ChannelsCount { get; private set; }
		public int ControllerPortsCount { get; private set; }
		public int PickTagsCapacity { get; private set; }
		public int PickTagsCapacityPerChannel { get; } = 30;
		public int PickTagsCount { get; private set; }
		public int PollingRange { get; private set; }
		public int Port { get; init; }
		public List<PickTag> PickTags { get; private set; } = [];
		public string IpAddress { get; init; }
		public Controller(ControllerModel controllerModel, int pickTagsCount, string ipAddress = "10.0.50.100", int port = 4660) {
			ChannelsCount = controllerModel == ControllerModel.AT400 ? 1 : 4;
			ControllerModel = controllerModel;
			ControllerPortsCount = controllerModel == ControllerModel.AT400 ? 2 : 1;
			IpAddress = ipAddress;
			PickTags = PickTag.PickTags(pickTagsCount);
			PickTagsCount = pickTagsCount;
			PickTagsCapacity = ChannelsCount * PickTagsCapacityPerChannel;
			PollingRange = PickTagsCapacity;
			Port = port;
			_controllerService = new();
			_controllerService.OnRead += Read;
		}
		public void Connect() {
			IsConnected = _controllerService.Connect(IpAddress, Port);
			if (IsConnected) {
				_controllerService.RequestConnectedPickTags(MessageType);
			}
		}
		public void Disconnect() {
			IsConnected = !_controllerService.Disconnect();
		}
		private void Read(CommunicationControlBlock communicationControlBlock) {
			Debug.WriteLine($"<= {communicationControlBlock.ToHexadecimalString()}");
			switch (communicationControlBlock.SubCommand) {
				case SubCommand.RequestConnectedPickTagsAndOnConnectedPickTagsReceived: {
					List<int> nodeAddresses = _controllerService.OnConnectedPickTagsReceived(communicationControlBlock);
					Debug.WriteLine($"Connected pick tags: {string.Join(", ", nodeAddresses)}");
					PickTags.ForEach((pickTag) => {
						if (nodeAddresses.Contains(pickTag.NodeAddress)) {
							pickTag.IsConnected = true;
						} else {
							pickTag.IsConnected = false;
						}
					});
					break;
				}
				default: break;
			}
		}
	}
}