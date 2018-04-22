namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using DoodleStudio95;
	using UnityEngine.SceneManagement;

	public class SceneTransition : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public float MinLoadingTime = 1.0f;

		[SerializeField]
		public DoodleAnimator TransitionAnimation;

		#endregion


		#region Monobehaviour

		private void Awake() {
			
			StartCoroutine(LoadNextSceneAsync());
		}

		#endregion


		#region Private Methods

		private IEnumerator LoadNextSceneAsync() {

			yield return this.TransitionAnimation.PlayAndPauseAt(0, -1);

			float startTime = Time.time;

			Scene oldScene = SceneManager.GetActiveScene();

			AsyncOperation loadOp = SceneManager.LoadSceneAsync(GameSceneController.SceneIndex, LoadSceneMode.Additive);
			while (!loadOp.isDone) {
				yield return null;
			}

			AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(oldScene);
			while (!unloadOp.isDone) {
				yield return null;
			}

			float loadingTime = Time.time - startTime;
			if (loadingTime < this.MinLoadingTime) {
				yield return new WaitForSecondsRealtime(this.MinLoadingTime - loadingTime);
			}

			yield return this.TransitionAnimation.PlayAndPauseAt(-1, 0);
			SceneManager.UnloadSceneAsync("Transition");
		}

		#endregion
	}
}