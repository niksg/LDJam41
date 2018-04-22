namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikComponents;

	public class Character : MonoBehaviour {

		#region Actions

		public System.Action<Character> OnHealthChanged;
		public System.Action<Character, float> OnTookDamage;
		public System.Action<Character> OnKnockedOut;

		#endregion


		#region Serialized Fields

		[Header("Stats")]

        [SerializeField]
		private float m_MaxHealth;

		[Header("FX")]

		[SerializeField]
		public ParticleSystem HitParticles;

		[Header("Feel")]

		[SerializeField]
		private float JiggleForce;

		[SerializeField]
		private float KnockbackForce;

		[Header("Hands")]

		[SerializeField]
		public Transform PunchTarget;

		[SerializeField]
		private BodyPart LeftHand;

		[SerializeField]
		private BodyPart RightHand;

		[Header("Body")]

		[SerializeField]
		private BodyPart Body;

		[Header("Head")]

		[SerializeField]
		private BodyPart Head;

        #endregion


		#region Public Properties

		public float MaxHealth {
			get { return m_MaxHealth; }
		}

		public float CurrentHealth {
			get { return m_CurrentHealth; }
			private set {
				m_CurrentHealth = value;
				if (this.OnHealthChanged != null) {
					OnHealthChanged(this);
				}
			}
		}

		#endregion


		#region Private Properties

		private float m_CurrentHealth;

		#endregion


		#region Monobehaviour

		private void OnEnable() {
			
			this.CurrentHealth = this.MaxHealth;
		}

		#endregion


		#region Public Methods

		public void UpdateCharacter() {

			this.Head.AddForce(Random.insideUnitCircle * this.JiggleForce * Time.deltaTime);
			this.Body.AddForce(Random.insideUnitCircle * this.JiggleForce * Time.deltaTime);
			this.LeftHand.AddForce(Random.insideUnitCircle * this.JiggleForce * Time.deltaTime);
			this.RightHand.AddForce(Random.insideUnitCircle * this.JiggleForce * Time.deltaTime);

			this.Head.UpdateBodyPart();
			this.Body.UpdateBodyPart();
			this.LeftHand.UpdateBodyPart();
			this.RightHand.UpdateBodyPart();
		}

		public void TakeDamage(float amount) {

			this.CurrentHealth -= amount;

			if (this.OnTookDamage != null) {
				OnTookDamage(this, amount);
			}

			if (this.CurrentHealth <= 0) {
				Knockout();
			}
		}

		public void Punch() {

			BodyPart hand;
			hand = null;

			int rand = Random.Range(0, 10);
			if (rand < 5) {
				hand = this.LeftHand;
			} else {
				hand = this.RightHand;
			}

			hand.AddForce((Vector2)this.PunchTarget.position - hand.Movable.Position);
		}

		public void TakeHit() {

			Vector2 force;
			force = Random.insideUnitCircle;
			force.x = this.KnockbackForce;

			// this.Body.AddForce(Random.insideUnitCircle);
			this.Head.AddForce(force);
			this.Body.AddForce(force * 0.75f);
			// this.LeftHand.AddForce(Random.insideUnitCircle);
			// this.RightHand.AddForce(Random.insideUnitCircle);

			this.HitParticles.Play();
		}

		#endregion


		#region Private Methods

		private void Knockout() {

			if (this.OnKnockedOut != null) {
				OnKnockedOut(this);
			}
		}

		#endregion
	}
}