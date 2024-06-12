namespace PickToLight.Core.Models.Actions {
	using System;
	public class OnReadActions {
		#region Properties
		public Action OnButtonLocked { get; set; } = () => { };
		public Action OnConfirmationButtonPressed { get; set; } = () => { };
		public Action OnConnectedPickTagsReceived { get; set; } = () => { };
		public Action OnIllegal { get; set; } = () => { };
		public Action OnMalfunction { get; set; } = () => { };
		public Action OnOldPickTagResetOrConnect { get; set; } = () => { };
		public Action OnQuantityInStockReceived { get; set; } = () => { };
		public Action OnShortageButtonPressed { get; set; } = () => { };
		public Action OnSpecialReceived { get; set; } = () => { };
		public Action OnTimeout { get; set; } = () => { };
		#endregion
		#region Methods
		public static OnReadActions Default() {
			return new OnReadActions();
		}
		#endregion
	}
}