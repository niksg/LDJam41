namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;
	using NikUtils;

	public class Track : MonoBehaviour, IControlButtonObserver {

		#region Actions

        public System.Action<Track, TrackCommand> OnHit;
		public System.Action<Track, TrackCommand> OnMiss;
		public System.Action<Track, TrackCommand> OnFail;
		public System.Action<Track, TrackCommand> OnMistimed;

        #endregion


		#region Serialized Fields

		[Header("Sounds")]

		[SerializeField]
		public List<AudioSource> Sounds;

		[Header("Button")]

		[SerializeField]
		public bool IsUpperCase;

		[Header("Limits")]

		[SerializeField]
		public Transform TrackStart;

		[SerializeField]
		public Transform TrackEnd;

		[SerializeField]
		public GameObject TrackDot;

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

		[Header("Bouncing")]
		
		[SerializeField]
		public BounceScale Bouncer;

		#endregion


		#region Public Properties

		public List<TrackCommand> TrackCommands {
			get {
				m_TrackCommands = m_TrackCommands ?? new List<TrackCommand>();
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

		private TrackCommand HeldCommand {
			get;
			set;
		}

		#endregion


		#region Fields

		List<TrackCommand> m_TrackCommands;

		#endregion


		#region Methods

		public void UpdateTrack(float dt) {

			for (int i = this.TrackCommands.Count-1; i >= 0; i--) {
				TrackCommand tc = this.TrackCommands[i];
				tc.UpdateTrackCommand(dt);
				SetCommandPosition(tc);
				CheckFailure(tc);
			}
		}

		#endregion


		#region IControlButtonObserver

		public InputResponse OnButtonDown(ControlButton button) {

			Debug.Log(gameObject.name + " Button down: " + button.Type);
			if (this.IsUpperCase && !Input.GetKey(KeyCode.LeftShift)) {
				return InputResponse.None;
			}
			else if (!this.IsUpperCase && Input.GetKey(KeyCode.LeftShift)) {
				return InputResponse.None;
			}

			this.Bouncer.Bounce();

			List<TrackCommand> eligibleCommands = new List<TrackCommand>();

			for (int i = this.TrackCommands.Count-1; i >= 0; i--) {
				TrackCommand tc = this.TrackCommands[i];
				if (tc.Progress < this.Activatable) {
					eligibleCommands.Add(tc);
				}
			}

			TrackCommand nextCommand;
			nextCommand = null;

			float minProgress;
			minProgress = 1.0f;

			if (eligibleCommands.Count > 0) {
				foreach (TrackCommand command in eligibleCommands) {
					if (command.Progress < minProgress && command.Progress > this.LateGrace) {
						nextCommand = command;
						minProgress = nextCommand.Progress;
					}
				}
			}

			if (nextCommand != null) {
				return CheckCommand(nextCommand, button);
			}

			return InputResponse.None;
		}

		public void OnButtonUp(ControlButton button) {
			
		}

		private InputResponse CheckCommand(TrackCommand tc, ControlButton button) {
	
			if (tc.Type != button.Type ) {
				FailedCommand(tc);
				return InputResponse.TrackAndSwallow;
			}
			if (tc.Progress < this.EarlyGrace * 0.35f && tc.Progress > this.LateGrace * 0.35f) {
				Debug.Log("PERFECT HIT! " + tc.Progress);
				HitCommand(tc);
			}
			else if (tc.Progress < this.EarlyGrace && tc.Progress > this.LateGrace) {
				Debug.Log("Mistimed Hit " + tc.Progress);
				MistimedCommand(tc);
			}
			else {
				FailedCommand(tc);
				return InputResponse.TrackAndSwallow;
			}

			return InputResponse.None;
		}

		#endregion


		#region Public Methods

		public void Add(TrackCommand command) {

			command.GameObject.transform.parent = transform;
			command.Progress = 1.0f;
			this.TrackCommands.Add(command);
			SetCommandPosition(command, true);
		}

		#endregion


		#region Private Methods

		private void SetCommandPosition(TrackCommand tc, bool immediate = false) {

			Vector2 targetPos;
			if (tc.Progress >= 0) {
				targetPos = Vector2.Lerp(this.TrackStart.position, this.TrackEnd.position, tc.Progress);
			}
			else {
				targetPos = Vector2.Lerp(this.TrackStart.position - (this.TrackEnd.position - this.TrackStart.position), this.TrackStart.position, tc.Progress + 1.0f);
			}

			if (immediate) {
				tc.Position = targetPos;
			}
			else {
				tc.Position += (targetPos - tc.Position) * 0.9f;
			}
		}

		private void CheckFailure(TrackCommand tc) {
			
			if (tc.Progress < this.LateGrace) {
				FailedCommand(tc);
			}
		}

		private void HitCommand(TrackCommand command) {

			if (this.OnHit != null) {
				OnHit(this, command);
			}

			command.Hit();
			PlaySound(false);
			this.TrackCommands.Remove(command);
		}

		private void MistimedCommand(TrackCommand command) {

			if (this.OnMistimed != null) {
				OnMistimed(this, command);
			}
			command.MistimedHit();
			PlaySound(false);
			this.TrackCommands.Remove(command);
		}

		private void MissedCommand(TrackCommand command) {

			if (this.OnMiss != null) {
				OnMiss(this, command);
			}
			command.Miss();
			PlaySound(true);
			this.TrackCommands.Remove(command);
		}

		private void FailedCommand(TrackCommand command) {

			if (this.OnFail != null) {
				OnFail(this, command);
			}
			command.Fail();
			PlaySound(true);
			this.TrackCommands.Remove(command);
		}

		private void PlaySound(bool pitch) {

			AudioSource source;
			source = this.Sounds[Random.Range(0, this.Sounds.Count)];

			if (pitch) {
				source.pitch -= Random.Range(0.2f, 0.4f);
			}
			else {
				source.pitch = 1.0f;
			}
			source.Play();
		}
		#endregion


		#region Monobehaviour

		private void Awake() {

			for (int i = 1; i < 8; i++) {
				GameObject newDot = Instantiate(this.TrackDot) as GameObject;
				newDot.transform.parent = transform;
				newDot.transform.position = Vector2.Lerp(this.TrackStart.position, this.TrackEnd.position, i / 8.0f);
			}

			this.TrackDot.gameObject.SetActive(false);
		}

		private void OnEnable() {

			ServiceProvider.InputHandler.Add(this, 0);	
		}

		private void OnDisable() {

			ServiceProvider.InputHandler.Remove(this);	
		}

		#endregion
	}
}