using PickToLight.Core.Models.Enums;
using PickToLight.Core.Services;
using System.Collections.Generic;

namespace PickToLight.Core.Models {
	public class Controller {
		public bool IsConnected { get; private set; } = false;
		public ControllerModel ControllerModel { get; init; }
		public ControllerPort ControllerPort { get; set; } = ControllerPort.Port1;
		public int ChannelsCount { get; private set; }
		public int ControllerPortsCount { get; private set; }
		public int PickTagsCapacity { get; private set; }
		public int PickTagsCapacityPerChannel { get; } = 30;
		public int PickTagsCount { get; private set; }
		public int Port { get; init; }
		public List<PickTag> PickTags { get; private set; } = [];
		public string IpAddress { get; init; }

		public Controller(ControllerModel controllerModel, string ipAddress = "10.0.50.100", int port = 4660) {
			switch (controllerModel) {
				case ControllerModel.AT400: {
					ChannelsCount = 1;
					ControllerPortsCount = 2;
					break;
				}
				case ControllerModel.AT500 or ControllerModel.AT500Plus: {
					ChannelsCount = 4;
					ControllerPortsCount = 1;
					break;
				}
			}
			ControllerModel = controllerModel;
			IpAddress = ipAddress;
			PickTagsCapacity = ChannelsCount * PickTagsCapacityPerChannel;
			Port = port;
		}

		private readonly ControllerService _controllerService = new();

		public void Clear(PickTag? pickTag = null) {
			_controllerService.Clear(ControllerPort, pickTag?.NodeAddress);
		}

		public void Connect() {
			IsConnected = _controllerService.Connect(IpAddress, Port);
		}

		public void Disconnect() {
			IsConnected = !_controllerService.Disconnect();
		}

		public void Display(string value, PickTag? pickTag = null, bool shouldFlash = false) {
			_controllerService.Display(ControllerPort, value, pickTag?.NodeAddress, shouldFlash);
		}
	}
}
