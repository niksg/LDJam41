namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;
	using NikUtils;

	public class TrackCommand : MonoBehaviour {

		#region Serialize Fields

		[SerializeField]
		private bool m_IsHold;

		[SerializeField]
		private float m_Duration;
		
		[SerializeField]
		public List<ControlButton> HeldButtons;

		#endregion


		#region Public Properties

		public BounceScale BounceScale {
	       get {
		      m_BounceScale = m_BounceScale ?? GetComponent<BounceScale>();
		      return m_BounceScale;
	       }
        }

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


		#region Fields

		private BounceScale m_BounceScale;

        #endregion


		#region Monobehaviour

        private void OnEnable() {
			
			this.BounceScale.OnFinishedBounce += Die;
		}

		private void OnDisable() {
			
			this.BounceScale.OnFinishedBounce -= Die;
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

			this.BounceScale.SetTargetScale(Vector3.zero);
		}

		public void Hit() {

			this.BounceScale.SetTargetScale(Vector3.zero);
		}

		public void Fail() {

			this.BounceScale.SetTargetScale(Vector3.zero);
		}

		private void Die() {

			gameObject.SetActive(false);
		}

		#endregion
	}
}