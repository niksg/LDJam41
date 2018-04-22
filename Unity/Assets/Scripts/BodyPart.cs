namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikComponents;

	public class BodyPart : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		[Range(0, 1)]
		public float Response;

		[SerializeField]
		[Range(0, 1)]
		public float Damping;

		#endregion


		#region Public Properties

		public Movable Movable {
	       get {
		      m_Movable = m_Movable ?? GetComponent<Movable>();
		      return m_Movable;
	       }
        }

		private float GroundY {
			get { return -1.0f; }
		}

        private Movable m_Movable;

		#endregion


		#region Private Properties

		private Vector2 StartPosition {
			get;
			set;
		}

		#endregion


		#region Monobehaviour

		private void OnEnable() {
			
			this.StartPosition = this.Movable.Position;
		}

		#endregion


		#region Public Methods

		public void UpdateBodyPart(bool isDead) {

			if (!isDead) {
				transform.eulerAngles += Vector3.forward * this.Movable.Velocity.x * -20;
				this.Movable.Velocity -= Vector2.up * 0.01f;
				this.Movable.ApplyVelocity();
				if (this.Movable.Position.y < this.GroundY && this.Movable.Velocity.y < 0) {
					this.Movable.Position = new Vector2(this.Movable.Position.x, this.GroundY);
					this.Movable.Velocity = new Vector2(this.Movable.Velocity.x * 0.4f, -this.Movable.Velocity.y * 0.8f);
				}
				return;
			}

			this.Movable.Velocity += (this.StartPosition - this.Movable.Position) * this.Response;
			this.Movable.ApplyVelocity();
			this.Movable.Velocity *= (1.0f - this.Damping);
		}

		public void AddForce(Vector2 force) {

			this.Movable.Velocity += force;
		}

		#endregion
	}
}