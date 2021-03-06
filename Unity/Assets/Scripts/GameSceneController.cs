﻿namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using UnityEngine.SceneManagement;

	public class GameSceneController : MonoBehaviour {

		#region Static Fields

        public static int SceneIndex;

        #endregion


		#region Serialized Fields

		[Header("Managers")]
		
		[SerializeField]
		public AudioManager AudioManager;

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

			if (GameSceneController.SceneIndex == 0) {
				GameSceneController.SceneIndex = 1;
			}

			this.AudioManager.OnMusicStarted += SubscribeToBeats;

			foreach (Track t in this.TrackManager.Tracks) {
				t.OnHit += OnHit;
				t.OnMistimed += OnHit;
				t.OnMiss += OnMiss;
				t.OnFail += OnFail;
			}

			this.CharacterManager.OnCharacterKnockout += EndRound;
		}

		private void OnDisable() {

			this.AudioManager.OnMusicStarted -= SubscribeToBeats;

			foreach (Track t in this.TrackManager.Tracks) {
				t.OnHit -= OnHit;
				t.OnMistimed -= OnHit;
				t.OnMiss -= OnMiss;
				t.OnFail -= OnFail;
			}

			this.CharacterManager.OnCharacterKnockout -= EndRound;
		}

		private void Start() {

			this.AudioManager.PlayMusic(3.0f);
		}

		private void Update() {

			this.AudioManager.UpdateAudioManager();

			if (!this.RoundOver) {
				// UpdateCommandSpawning();
				this.TrackManager.UpdateTrackManager();
			}

			this.CharacterManager.UpdateCharacterManager();
		}

		#endregion


		#region Private Methods

		private void SubscribeToBeats() {

			this.AudioManager.OnMusicBeat += OnBeat;
		}

		private void OnBeat() {

			if (!this.RoundOver) {
				SpawnCommand();
			}
		}

		private void OnHit(Track track, TrackCommand trackCommand) {

			int dmg = 1;
			if (Mathf.Abs(trackCommand.Progress) < 0.01f) {
				dmg = 2;
			}
			this.CharacterManager.PlayerStrike(dmg);
		}

		private void OnMiss(Track track, TrackCommand trackCommand) {

			this.CharacterManager.EnemyStrike();
		}

		private void OnFail(Track track, TrackCommand trackCommand) {

			this.CharacterManager.EnemyStrike();
		}

		private void UpdateCommandSpawning() {
			
			this.TotalTime += Time.deltaTime;
			if (this.TotalTime >= this.Rate) {
				this.TotalTime -= this.Rate;
				SpawnCommand();
			}
		}

		private void SpawnCommand() {

			TrackCommand nextCommand = this.CommandManager.GetNext();
			if (nextCommand == null) {
				return;
			}

			bool uppcase = Random.Range(0, 10) < 3;
			if (uppcase) {
				this.TrackManager.Track1.Add(nextCommand);
			} else {
				this.TrackManager.Track2.Add(nextCommand);
			}
		}

		private void EndRound(Character character) {

			this.CharacterManager.OnCharacterKnockout -= EndRound;

			Debug.Log("Round Over");
			this.RoundOver = true;

			StartCoroutine(LoadNextSceneAsync());
		}

		private IEnumerator LoadNextSceneAsync() {

			bool pressed;
			pressed = false;
			
			float dt = 0;

			while(!pressed || dt < 3.0f) {
				if (dt > 1.0f && Input.anyKeyDown) {
					pressed = true;
				}
				dt += Time.deltaTime;
				yield return null;
			}
			// GameSceneController.SceneIndex++;

			Debug.Log("Going to next scene....");
			SceneManager.LoadSceneAsync("Transition", LoadSceneMode.Additive);
		}

		#endregion
	}
}