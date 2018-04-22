namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class TrackManager : MonoBehaviour {

		#region Serialized Fields

		[Header("Audio Manager")]

		[SerializeField]
		public AudioManager AudioManager;

		[Header("Difficulty")]

		[SerializeField]
		private int m_Speed;

		[Header("Tracks")]

		[SerializeField]
		public Track Track1;

		[SerializeField]
		public Track Track2;

		#endregion


		#region Public Properties

		public List<Track> Tracks {
	       get {
		      m_Tracks = m_Tracks ?? new List<Track>(GetComponentsInChildren<Track>());
		      return m_Tracks;
	       }
        }

		#endregion


		#region Private Properties

		private float Speed {
			get { return (this.AudioManager.BPM / 60.0f) * (1.0f / m_Speed); }
		}

		#endregion

		
		#region Fields

		private List<Track> m_Tracks;

        #endregion


		#region Public Methods

		public void UpdateTrackManager() {

			UpdateTracks();
		}

		#endregion


		#region Private Methods

		private void UpdateTracks() {
			
			foreach (Track track in this.Tracks) {
				track.UpdateTrack(Time.deltaTime * this.Speed);
			}
		}

		private void SpawnCommand() {
			
		}

		// private void AddCombo(Combo combo) {

		// }

		#endregion
	}
}