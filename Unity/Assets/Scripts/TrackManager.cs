namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class TrackManager : MonoBehaviour {

		#region Serialized Fields

		[Header("Managers")]

		[SerializeField]
		public CommandManager CommandManager;

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
			get { return 1.0f; }
		}

		#endregion

		
		#region Fields

		private List<Track> m_Tracks;

        #endregion


		#region Monobehaviour

		private void Update() {

			UpdateTracks();

			if (Input.GetKeyDown("f")) {
				this.Tracks[Random.Range(0, this.Tracks.Count)].Add(this.CommandManager.GetRandomCommand());
			}
		}

		#endregion


		#region Public Methods



		#endregion


		#region Private Methods

		private void UpdateTracks() {
			
			foreach (Track track in this.Tracks) {
				track.UpdateTrack(Time.deltaTime * this.Speed);
			}
		}

		private void AddCombo(Combo combo) {

		}

		#endregion
	}
}