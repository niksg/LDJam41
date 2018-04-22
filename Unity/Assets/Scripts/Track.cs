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
		public System.Action<Track, ITrackCommand> OnFail;
		public System.Action<Track, ITrackCommand> OnHoldBegan;
		public System.Action<Track, ITrackCommand> OnHoldEnded;

        #endregion


		#region Serialized Fields

		[Header("Button")]

		[SerializeField]
		public ControlButton Button;

		[Header("Limits")]

		[SerializeField]
		public Transform TrackStart;

		[SerializeField]
		public Transform TrackEnd;

		[Header("Activation")]

		[SerializeField]
		[Range(0, 0.5f)]
		private float Activatable;

		[SerializeField]
		[Range(0, 0.5f)]
		private float EarlyGrace;

		[SerializeField]
		[Range(-.5f, 0)]
		private float LateGrace;

		[SerializeField]
		[Range(-1, 0)]
		private float MissValue;

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

		public bool IsHoldingCommand {
			get { return this.HeldCommand != null; }
		}

		#endregion


		#region Private Properties

		private ITrackCommand HeldCommand {
			get;
			set;
		}

		#endregion


		#region Fields

		List<ITrackCommand> m_TrackCommands;

		#endregion


		#region Methods

		public void UpdateTrack(float dt) {

			for (int i = this.TrackCommands.Count-1; i >= 0; i--) {
				ITrackCommand tc = this.TrackCommands[i];
				tc.UpdateTrackCommand(dt);
				SetCommandPosition(tc);
				CheckFailure(tc);
			}
		}

		#endregion


		#region IControlButtonObserver

		public InputResponse OnButtonDown(ControlButton button) {

			if (button.Type != this.Button.Type) {
				return InputResponse.None;
			}

			for (int i = this.TrackCommands.Count-1; i >= 0; i--) {
				ITrackCommand tc = this.TrackCommands[i];
				if (tc.Progress < this.EarlyGrace && tc.Progress > this.LateGrace) {
					HitCommand(tc);
				} else if (tc.Progress < this.MissValue) {
					MissedCommand(tc);
				}
			}

			return InputResponse.TrackAndSwallow;
		}

		public void OnButtonUp(ControlButton button) {
			
			if (this.HeldCommand != null) {
				if (this.OnHoldEnded != null) {
					OnHoldEnded(this, this.HeldCommand);
				}
				this.TrackCommands.Remove(this.HeldCommand);
				this.HeldCommand = null;
			}
		}

		#endregion


		#region Public Methods

		public void Add(ITrackCommand command) {

			command.GameObject.transform.parent = transform;
			command.Progress = 1.0f;
			this.TrackCommands.Add(command);
			SetCommandPosition(command);
		}

		#endregion


		#region Private Methods

		private void SetCommandPosition(ITrackCommand tc) {

			Vector2 targetPos;
			if (tc.Progress >= 0) {
				targetPos = Vector2.Lerp(this.TrackStart.position, this.TrackEnd.position, tc.Progress);
			}
			else {
				targetPos = Vector2.Lerp(this.TrackStart.position - (this.TrackEnd.position - this.TrackStart.position), this.TrackStart.position, tc.Progress + 1.0f);
			}

			tc.Position += (targetPos - tc.Position) * 0.2f;
		}

		private void CheckFailure(ITrackCommand tc) {
			
			if (tc.Progress < this.LateGrace) {
				FailedCommand(tc);
			}
		}

		private void HitCommand(ITrackCommand command) {

			if (this.OnHit != null) {
				OnHit(this, command);
			}

			if (command.IsHold) {
				this.HeldCommand = command;
			} else {
				command.Hit();
				this.TrackCommands.Remove(command);
			}
		}

		private void MissedCommand(ITrackCommand command) {
			
			if (this.OnMiss != null) {
				OnMiss(this, command);
			}
			command.Miss();
			this.TrackCommands.Remove(command);
		}

		private void FailedCommand(ITrackCommand command) {

			if (this.OnFail != null) {
				OnFail(this, command);
			}
			command.Fail();
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