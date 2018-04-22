namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class TrackManager : MonoBehaviour {

		#region Serialized Fields

		[Header("Difficulty")]

		[SerializeField]
		private float m_Speed;

		[SerializeField]
		private float m_Rate;

		[Header("Tracks")]

		[SerializeField]
		public Track Track1;

		[SerializeField]
		public Track Track2;

		[SerializeField]
		public Track Track3;

		[SerializeField]
		public Track Track4;

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
			get { return m_Speed; }
		}

		private float Rate {
			get { return m_Rate; }
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
				track.UpdateTrack(Time.fixedDeltaTime * this.Speed);
			}
		}

		private void SpawnCommand() {
			
		}

		private void AddCombo(Combo combo) {

		}

		#endregion
	}
}