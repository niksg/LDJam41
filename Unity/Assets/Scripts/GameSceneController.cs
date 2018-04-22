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

		[SerializeField]
		public CharacterManager CharacterManager;
		
		#endregion


		#region Public Properties

		public float Rate = 1;

		#endregion


		#region Private Properties

		private float TotalTime {
			get;
			set;
		}

		private bool RoundOver {
			get;
			set;
		}

		#endregion


		#region Monobehaviour

		private void OnEnable() {

			foreach (Track t in this.TrackManager.Tracks) {
				t.OnHit += OnHit;
				t.OnMiss += OnMiss;
				t.OnFail += OnFail;
			}

			this.CharacterManager.OnCharacterKnockout += EndRound;
		}

		private void OnHit(Track track, TrackCommand trackCommand) {

			this.CharacterManager.PlayerStrike();
		}

		private void OnMiss(Track track, TrackCommand trackCommand) {

			this.CharacterManager.EnemyStrike();
		}

		private void OnFail(Track track, TrackCommand trackCommand) {

			this.CharacterManager.EnemyStrike();
		}

		private void Update() {
			
			if (!this.RoundOver) {
				UpdateCommandSpawning();
				this.TrackManager.UpdateTrackManager();
			}

			this.CharacterManager.UpdateCharacterManager();
		}

		#endregion


		#region Private Methods

		private void UpdateCommandSpawning() {
			
			this.TotalTime += Time.deltaTime;
			if (this.TotalTime >= this.Rate) {
				this.TotalTime -= this.Rate;
				SpawnCommand();
			}
		}

		private void SpawnCommand() {

			this.TrackManager.Tracks[Random.Range(0, this.TrackManager.Tracks.Count)].Add(this.CommandManager.GetRandomCommand());
		}

		private void EndRound(Character character) {

			this.RoundOver = true;
		}

		#endregion
	}
}