namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class Poolable : MonoBehaviour {

		#region Actions

		public System.Action<Poolable> ShouldRepool;
		public System.Action<Poolable> OnRepooled;

		#endregion


		#region Public Properties


		#endregion


		#region Private Properties



		#endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public void Repool() {

			if (this.ShouldRepool != null) {
				ShouldRepool(this);
			}
		}

		#endregion


		#region Private Methods



		#endregion
	}
}