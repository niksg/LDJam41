namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	// [Serializable]
	// public class Combo {
		
	// 	[SerializeField]
	// 	public string ComboString;
	// }

	public class ComboManager : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public List<string> Combos;

		#endregion


		#region Public Properties


		#endregion


		#region Private Properties



		#endregion


		#region Fields


		#endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public string GetCombo(int level) {

			return this.Combos[Random.Range(0, this.Combos.Count)];
		}

		#endregion


		#region Private Methods


		#endregion
	}
}