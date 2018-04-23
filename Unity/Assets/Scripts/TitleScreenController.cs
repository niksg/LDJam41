namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using UnityEngine.SceneManagement;

	public class TitleScreenController : MonoBehaviour {

		#region Serialized Fields



		#endregion


		#region Public Properties



		#endregion


		#region Private Properties



		#endregion


		#region Monobehaviour

		private void Update() {

			if (Input.anyKeyDown) {
				GameSceneController.SceneIndex = 1;
				SceneManager.LoadSceneAsync("Transition", LoadSceneMode.Additive);
			}
		}

		#endregion


		#region Public Methods



		#endregion


		#region Private Methods



		#endregion
	}
}