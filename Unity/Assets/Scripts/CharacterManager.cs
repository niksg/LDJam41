namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class CharacterManager : MonoBehaviour {

		#region Serialized Fields



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

		public void UpdateCharacterManager() {

			foreach (HealthBar hb in this.HealthBars) {
				hb.UpdateHealthBar();
			}
		}

		#endregion


		#region Public Methods

		public void PlayerStrike() {

		}

		public void EnemyStrike() {
			
		}

		#endregion


		#region Private Methods



		#endregion
	}
}