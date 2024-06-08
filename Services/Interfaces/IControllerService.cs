using PickToLight.Core.Models.Enums;

namespace PickToLight.Core.Services.Interfaces {
	public interface IControllerService {
		public bool Connect(string ipAddress, int port);
		public bool Disconnect();
		public void Display(ControllerPort controllerPort, string value, int? NodeAddress = null, bool shouldFlash = false);
		public void Clear(ControllerPort controllerPort, int? NodeAddress);
	}
}
