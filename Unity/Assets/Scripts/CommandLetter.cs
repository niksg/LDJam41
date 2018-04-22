namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public class CommandLetter : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public ControlButtonType Type;

		#endregion


		#region Public Methods

		public void Activate(ControlButtonType type) {
			gameObject.SetActive(this.Type == type);
		}

		#endregion
	}
}