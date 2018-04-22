namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class GameSceneController : MonoBehaviour {

		#region Serialized Fields

		[Header("Managers")]
		
		[SerializeField]
		public TrackManager TrackManager;

		[SerializeField]
		public CommandManager CommandManager;

		#endregion


		#region Public Properties

		public float Rate = 1;

		#endregion


		#region Private Properties

		private float TotalTime {
			get;
			set;
		}

		#endregion


		#region Monobehaviour

		private void Update() {
			
			this.TotalTime += Time.deltaTime;

			if (this.TotalTime > this.Rate) {
				this.TotalTime -= this.Rate;
				SpawnCommand();
			}

			this.TrackManager.UpdateTrackManager();
			// this.CommandManager.UpdateCommandManager();
		}

		#endregion


		#region Public Methods



		#endregion


		#region Private Methods

		private void SpawnCommand() {

			this.TrackManager.Tracks[Random.Range(0, this.TrackManager.Tracks.Count)].Add(this.CommandManager.GetRandomCommand());
		}

		#endregion
	}
}