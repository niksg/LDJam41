namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class AudioManager : MonoBehaviour {

		#region Actions

		public System.Action OnMusicStarted;
		public System.Action OnMusicBeat;

		#endregion


		#region Serialized Fields

		[SerializeField]
        public AudioSource MusicTrack;

		[SerializeField]
        public int BPM;

		[SerializeField]
        public int BPS;

        #endregion


		#region Public Properties

		public float BeatValue {
			get { return 60.0F / this.BPM * this.BPS; }
		}

		#endregion


		#region Private Properties

		private bool IsPlaying {
			get;
			set;
		}

		private double EventTime {
			get;
			set;
		}

		#endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public void PlayMusic(float delay) {

			StartCoroutine(PlayMusicDelayed(delay));
		}

		public void UpdateAudioManager() {

			if (this.IsPlaying) {
				if (AudioSettings.dspTime >= this.EventTime) {
					if (this.OnMusicBeat != null) {
						OnMusicBeat();
					}
					this.EventTime += this.BeatValue;
				}
			}
		}

		#endregion


		#region Private Methods

		private IEnumerator PlayMusicDelayed(float delay) {

			yield return new WaitForSeconds(delay);

			this.MusicTrack.Play();
			this.IsPlaying = true;
			this.EventTime = AudioSettings.dspTime;
			if (this.OnMusicStarted != null) {
				OnMusicStarted();
			}
		}

		#endregion
	}
}