namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public class TrackCommand : MonoBehaviour, ITrackCommand {

		#region Serialize Fields

		[SerializeField]
		private bool m_IsHold;

		[SerializeField]
		private float m_Duration;
		
		[SerializeField]
		public List<ControlButton> HeldButtons;

		#endregion


		#region Public Properties

		public Vector2 Position {
			get { return (Vector2)transform.position; }
			set {
				Vector3 pos = transform.position;
				pos.x = value.x;
				pos.y = value.y;
				transform.position = pos;
			}
		}

		public float Progress {
			get;
			set;
		}

		public bool IsHold {
			get { return m_IsHold; }
		}

		public float Duration {
			get { return m_Duration; }
		}

		public GameObject GameObject {
			get { return gameObject; }
		}

		#endregion


		#region Public Methods

		public void Init(params ControlButton[] heldButtons) {

			this.Progress = 1.0f;
			this.HeldButtons = new List<ControlButton>(heldButtons);
		}

		public void UpdateTrackCommand(float dt) {

			this.Progress -= dt;
		}

		public bool Check(params ControlButton[] heldButtons) {

			if (this.HeldButtons == null) {
				return true;
			}

			if (heldButtons.Length != this.HeldButtons.Count) {
				return false;
			}

			int matches = 0;
			foreach (ControlButton button in this.HeldButtons) {
				for (int i = 0; i < heldButtons.Length; i++) {
					if (button.Type == heldButtons[i].Type) {
						matches++;
					}
				}
			}

			if (matches == this.HeldButtons.Count) {
				return true;
			}

			return false;
		}

		public void Miss() {
			gameObject.SetActive(false);
		}

		public void Hit() {
			gameObject.SetActive(false);
		}

		public void Fail() {
			gameObject.SetActive(false);
		}

		#endregion
	}
}