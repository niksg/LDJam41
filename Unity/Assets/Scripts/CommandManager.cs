namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class CommandManager : MonoBehaviour {

		#region Serialized Fields

		public List<ITrackCommand> ITrackCommands {
	       get {
		      m_ITrackCommands = m_ITrackCommands ?? new List<ITrackCommand>(GetComponentsInChildren<ITrackCommand>(true));
		      return m_ITrackCommands;
	       }
        }

		#endregion


		#region Public Properties



		#endregion


		#region Private Properties



		#endregion


		#region Fields

		private List<ITrackCommand> m_ITrackCommands;

        #endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public ITrackCommand GetRandomCommand() {

			GameObject tcObject = Instantiate(this.ITrackCommands[Random.Range(0, this.ITrackCommands.Count)].GameObject) as GameObject;
			tcObject.SetActive(true);
			
			return tcObject.GetComponent<ITrackCommand>();
		}

		#endregion


		#region Private Methods



		#endregion
	}
}