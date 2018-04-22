namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class CommandManager : MonoBehaviour {

		#region Serialized Fields

		public List<TrackCommand> TrackCommands {
	       get {
		      m_TrackCommands = m_TrackCommands ?? new List<TrackCommand>(GetComponentsInChildren<TrackCommand>(true));
		      return m_TrackCommands;
	       }
        }

		#endregion


		#region Public Properties



		#endregion


		#region Private Properties



		#endregion


		#region Fields

		private List<TrackCommand> m_TrackCommands;

        #endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public TrackCommand GetRandomCommand() {

			GameObject tcObject = Instantiate(this.TrackCommands[Random.Range(0, this.TrackCommands.Count)].GameObject) as GameObject;
			tcObject.SetActive(true);
			
			return tcObject.GetComponent<TrackCommand>();
		}

		#endregion


		#region Private Methods



		#endregion
	}
}