namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class Character : MonoBehaviour {

		#region Actions

		public System.Action<Character> OnHealthChanged;
		public System.Action<Character, float> OnTookDamage;
		public System.Action<Character> OnKnockedOut;

		#endregion


		#region Serialized Fields

        [SerializeField]
		private float m_MaxHealth;

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

		public void TakeDamage(float amount) {

			this.CurrentHealth -= amount;
			

			if (this.OnTookDamage != null) {
				OnTookDamage(this, amount);
			}

			if (this.CurrentHealth <= 0) {
				Knockout();
			}
		}

		public void Play(string animName) {

			// play animation
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