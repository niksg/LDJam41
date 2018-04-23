namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;
	using NikUtils;
	using System;

	public class TrackCommand : MonoBehaviour, IPoolable {

		#region Public Properties

	public bool Logging;

		public ControlButtonType Type {
			get;
			set;
		}

		public BounceScale BounceScale {
	       get {
		      m_BounceScale = m_BounceScale ?? GetComponent<BounceScale>();
		      return m_BounceScale;
	       }
        }

		public Poolable Poolable {
	       get {
		      m_Poolable = m_Poolable ?? GetComponent<Poolable>();
		      return m_Poolable;
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

		public GameObject GameObject {
			get { return gameObject; }
		}

		#endregion


		#region Fields

		private BounceScale m_BounceScale;
		private Poolable m_Poolable;

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

		public void Init(ControlButtonType type) {

			this.Progress = 1.0f;
			this.Type = type;

			foreach (CommandLetter letter in GetComponentsInChildren<CommandLetter>(true)) {
				letter.Activate(this.Type);
			}

			foreach (HitType hitType in GetComponentsInChildren<HitType>(true)) {
				hitType.Activate(HitTypes.None);
			}
		}

		public void UpdateTrackCommand(float dt) {

			this.Progress -= dt;

			if (this.Logging) {
				Debug.Log("Progress: " + this.Progress);
			}
		}

		public void Miss() {

			this.BounceScale.SetTargetScale(Vector3.zero);
			foreach (HitType hitType in GetComponentsInChildren<HitType>(true)) {
				hitType.Activate(HitTypes.Miss);
			}
		}

		public void Hit() {

			this.BounceScale.SetTargetScale(Vector3.zero);
			foreach (HitType hitType in GetComponentsInChildren<HitType>(true)) {
				hitType.Activate(HitTypes.Hit);
			}
		}

		public void MistimedHit() {

			this.BounceScale.SetTargetScale(Vector3.zero);
			foreach (HitType hitType in GetComponentsInChildren<HitType>(true)) {
				hitType.Activate(HitTypes.Mistimed);
			}
		}

		public void Fail() {

			this.BounceScale.SetTargetScale(Vector3.zero);
			foreach (HitType hitType in GetComponentsInChildren<HitType>(true)) {
				hitType.Activate(HitTypes.Fail);
			}
		}

		private void Die() {

			this.Poolable.Repool();
		}

		public void Reset() {

			transform.localPosition = Vector3.zero;
			this.BounceScale.SetTargetScale(Vector3.one, true);
		}

		#endregion
	}
}