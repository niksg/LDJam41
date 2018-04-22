namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public enum HitTypes {
		None,
		Hit,
		Fail,
		Miss,
		Mistimed
	}

	public class HitType : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public HitTypes Type;

		#endregion


		#region Public Methods

		public void Activate(HitTypes type) {
			gameObject.SetActive(this.Type == type);
		}

		#endregion
	}
}