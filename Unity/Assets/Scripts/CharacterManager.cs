namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class CharacterManager : MonoBehaviour {

		#region Actions

        public System.Action<Character> OnCharacterKnockout;

        #endregion



		#region Serialized Fields

		[SerializeField]
		public Character PlayerCharacter;

		[SerializeField]
		public Character EnemyCharacter;

		#endregion


		#region Public Properties

		public List<HealthBar> HealthBars {
	       get {
		      m_HealthBars = m_HealthBars ?? new List<HealthBar>(GetComponentsInChildren<HealthBar>());
		      return m_HealthBars;
	       }
        }

        private List<HealthBar> m_HealthBars;

		#endregion


		#region Private Properties



		#endregion


		#region Monobehaviour

		private void OnEnable() {
			
			this.PlayerCharacter.OnKnockedOut += CharacterKnockout;
			this.EnemyCharacter.OnKnockedOut += CharacterKnockout;
			
			this.EnemyCharacter.OnKnockedOut += PlayerWon;
			this.PlayerCharacter.OnKnockedOut += EnemyWon;
		}

		private void OnDisable() {
			
			this.PlayerCharacter.OnKnockedOut -= CharacterKnockout;
			this.EnemyCharacter.OnKnockedOut -= CharacterKnockout;
		}
		#endregion


		#region Public Methods

		public void UpdateCharacterManager() {

			this.PlayerCharacter.UpdateCharacter();
			this.EnemyCharacter.UpdateCharacter();

			foreach (HealthBar hb in this.HealthBars) {
				hb.UpdateHealthBar();
			}
		}

		public void PlayerStrike() {

			this.PlayerCharacter.Punch();
			this.EnemyCharacter.TakeDamage(1);
			this.EnemyCharacter.TakeHit();
		}

		public void EnemyStrike() {
		
			this.EnemyCharacter.Punch();
			this.PlayerCharacter.TakeDamage(1);
			this.PlayerCharacter.TakeHit();
		}

		#endregion


		#region Private Methods

		private void PlayerWon(Character character) {

			GameSceneController.SceneIndex++;
			this.PlayerCharacter.DeclareWinner();
		}

		private void EnemyWon(Character character) {

			GameSceneController.SceneIndex = 1;
			this.EnemyCharacter.DeclareWinner();
		}

		private void CharacterKnockout(Character character) {

			if (this.OnCharacterKnockout != null) {
				OnCharacterKnockout(character);
			}
		}

		#endregion
	}
}