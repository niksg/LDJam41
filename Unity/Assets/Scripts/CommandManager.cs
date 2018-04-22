namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public class CommandManager : MonoBehaviour {

		#region Serialized Fields

		public List<TrackCommand> TrackCommands {
	       get {
		      m_TrackCommands = m_TrackCommands ?? new List<TrackCommand>(GetComponentsInChildren<TrackCommand>(true));
		      return m_TrackCommands;
	       }
        }

		public Pool Pool {
	       get {
		      m_Pool = m_Pool ?? GetComponent<Pool>();
		      return m_Pool;
	       }
        }

		#endregion


		#region Public Properties



		#endregion


		#region Private Properties



		#endregion


		#region Fields

		private List<TrackCommand> m_TrackCommands;
        private Pool m_Pool;

        #endregion


		#region Monobehaviour



		#endregion


		#region Public Methods

		public TrackCommand GetRandomCommand() {

			Poolable poolable = this.Pool.Get();
			TrackCommand tc = poolable.GetComponent<TrackCommand>();
			tc.Reset();
			tc.Init((ControlButtonType)Random.Range(0, 26));
			return tc;
			// GameObject tcObject = Instantiate(this.TrackCommands[Random.Range(0, this.TrackCommands.Count)].GameObject) as GameObject;
			// tcObject.SetActive(true);
			
			// return tcObject.GetComponent<TrackCommand>();
		}

		#endregion


		#region Private Methods



		#endregion
	}
}