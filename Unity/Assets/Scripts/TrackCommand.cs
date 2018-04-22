﻿namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;
	using NikUtils;
	using System;

	public class TrackCommand : MonoBehaviour, IPoolable {

		#region Public Properties

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
		}

		public bool Check(params ControlButton[] heldButtons) {


			// if (heldButtons.Length != this.HeldButtons.Count) {
			// 	return false;
			// }

			// int matches = 0;
			// foreach (ControlButton button in this.HeldButtons) {
			// 	for (int i = 0; i < heldButtons.Length; i++) {
			// 		if (button.Type == heldButtons[i].Type) {
			// 			matches++;
			// 		}
			// 	}
			// }

			// if (matches == this.HeldButtons.Count) {
			// 	return true;
			// }

			return false;
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