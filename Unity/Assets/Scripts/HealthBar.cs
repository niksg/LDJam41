namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class HealthBar : MonoBehaviour {

		#region Serialized Fields

		[Header("Character")]

		[SerializeField]
		public Character Character;

		[Header("Pieces")]

		[SerializeField]
		private Transform HealthMeter;

		[SerializeField]
		private Transform HealthMeterBackground;

		[Header("Feel")]

		[SerializeField]
		[Range(0, 1)]
		private float Response;

		#endregion


		#region Public Properties



		#endregion


		#region Private Properties

		private float CurrentAmount {
			get;
			set;
		}

		private float TargetAmount {
			get;
			set;
		}

		#endregion


		#region Monobehaviour

		private void OnEnable() {

			this.Character.OnHealthChanged += UpdateTargetHealth;
		}

		private void OnDisable() {

			this.Character.OnHealthChanged -= UpdateTargetHealth;
		}

		#endregion


		#region Public Methods

		public void UpdateHealthBar() {

			UpdateAmount();
			UpdateScale();
		}

		#endregion


		#region Private Methods

		private void UpdateTargetHealth(Character character) {

			this.TargetAmount = character.CurrentHealth / character.MaxHealth;
		}

		private void UpdateAmount() {
			
			this.CurrentAmount += (this.TargetAmount - this.CurrentAmount) * this.Response;
			this.CurrentAmount = Mathf.Clamp01(this.CurrentAmount);
		}

		private void UpdateScale() {

			Vector3 scale = this.HealthMeter.localScale;
			scale.x = this.CurrentAmount;
			this.HealthMeter.localScale = scale;
		}

		#endregion
	}
}