namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using System;

	[Serializable]
	public class Combo {
		
		public List<ITrackCommand> ComboCommands;
	}

	public class ComboManager : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public List<Combo> Combos;

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



		#endregion


		#region Private Methods


		#endregion
	}
}