namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public class Track : MonoBehaviour, IControlButtonObserver {

		#region Actions

        public System.Action<Track, ITrackCommand> OnHit;
		public System.Action<Track, ITrackCommand> OnMiss;

        #endregion



		#region Serialized Fields

		[SerializeField]
		public ControlButton Button;

		#endregion


		#region Public Properties

		public List<ITrackCommand> TrackCommands {
			get {
				m_TrackCommands = m_TrackCommands ?? new List<ITrackCommand>();
				return m_TrackCommands;
			}
		}

		public int Priority {
			get { return 0; }
		}

		public bool IsEmpty {
			get { return this.TrackCommands.Count == 0; }
		}

		#endregion


		#region Private Properties

		private float Activatable {
			get { return 0.15f; }
		}

		private float EarlyGrace {
			get { return 0.05f; }
		}
		
		private float LateGrace {
			get { return -0.1f; }
		}

		private float MissValue {
			get { return -0.5f; }
		}

		#endregion


		#region Fields

		List<ITrackCommand> m_TrackCommands;

		#endregion


		#region Methods

		public void UpdateTrack(float dt) {

			foreach (ITrackCommand tc in this.TrackCommands) {
				tc.UpdateTrackCommand(dt);
			}
		}

		#endregion


		#region IControlButtonObserver

		public InputResponse OnButtonDown(ControlButton button) {

			if (button != this.Button) {
				return InputResponse.None;
			}

			for (int i = this.TrackCommands.Count-1; i >= 0; i--) {
				ITrackCommand tc = this.TrackCommands[i];
				if (tc.Progress < this.EarlyGrace && tc.Progress > this.LateGrace) {
					HitCommand(tc);
				} else if (tc.Progress < this.MissValue) {
					MissedCommand(tc);
				} else if (tc.Progress > this.EarlyGrace && tc.Progress < this.Activatable) {
					MissedCommand(tc);
				}
			}

			return InputResponse.None;
		}

		public void OnButtonUp(ControlButton button) {
			
		}

		#endregion


		#region Public Methods

		public void Add(ITrackCommand command) {

			this.TrackCommands.Add(command);
		}

		#endregion


		#region Private Methods

		private void HitCommand(ITrackCommand command) {

			if (this.OnHit != null) {
				OnHit(this, command);
			}
			this.TrackCommands.Remove(command);
		}

		private void MissedCommand(ITrackCommand command) {
			
			if (this.OnMiss != null) {
				OnMiss(this, command);
			}
			this.TrackCommands.Remove(command);
		}

		#endregion


		#region Monobehaviour

		private void OnEnable() {

			ServiceProvider.InputHandler.Add(this, 0);	
		}

		private void OnDisable() {

			ServiceProvider.InputHandler.Remove(this);	
		}

		#endregion
	}
}