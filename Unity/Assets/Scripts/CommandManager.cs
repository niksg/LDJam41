namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;
	using NikInput;

	public class CommandManager : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public ComboManager ComboManager;

		#endregion


		#region Public Properties

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


		#region Private Properties

		private int Level {
			get;
			set;
		}

		private int Index {
			get;
			set;
		}

		private string CurrentCombo {
			get;
			set;
		}

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
		}

		public TrackCommand GetNext() {

			if (this.CurrentCombo == null) {
				this.CurrentCombo = this.ComboManager.GetCombo(this.Level);
			}
			if (this.Index >= this.CurrentCombo.Length) {
				this.CurrentCombo = this.ComboManager.GetCombo(this.Level);
			}
			
			string nextLetter = this.CurrentCombo[this.Index].ToString();
			this.Index++;

			Poolable poolable = this.Pool.Get();
			TrackCommand tc = poolable.GetComponent<TrackCommand>();
			tc.Reset();
			tc.Init(TypeForLetter(nextLetter));

			if (tc.Type == ControlButtonType.None) {
				poolable.Repool();
				return null;
			}
			return tc;
		}

		#endregion


		#region Private Methods

		private ControlButtonType TypeForLetter(string letter) {

			letter = letter.ToUpper();

			switch(letter) {
				case "A":
					return ControlButtonType.Key_A;
				case "B":
					return ControlButtonType.Key_B;
				case "C":
					return ControlButtonType.Key_A;
				case "D":
					return ControlButtonType.Key_D;
				case "E":
					return ControlButtonType.Key_E;
				case "F":
					return ControlButtonType.Key_F;
				case "G":
					return ControlButtonType.Key_G;
				case "H":
					return ControlButtonType.Key_H;
				case "I":
					return ControlButtonType.Key_I;
				case "J":
					return ControlButtonType.Key_J;
				case "K":
					return ControlButtonType.Key_K;
				case "L":
					return ControlButtonType.Key_L;
				case "M":
					return ControlButtonType.Key_M;
				case "N":
					return ControlButtonType.Key_N;
				case "O":
					return ControlButtonType.Key_O;
				case "P":
					return ControlButtonType.Key_P;
				case "Q":
					return ControlButtonType.Key_Q;
				case "R":
					return ControlButtonType.Key_R;
				case "S":
					return ControlButtonType.Key_S;
				case "T":
					return ControlButtonType.Key_T;
				case "U":
					return ControlButtonType.Key_U;
				case "V":
					return ControlButtonType.Key_V;
				case "W":
					return ControlButtonType.Key_W;
				case "X":
					return ControlButtonType.Key_X;
				case "Y":
					return ControlButtonType.Key_Y;
				case "Z":
					return ControlButtonType.Key_Z;
				default:
					return ControlButtonType.None;
			}
		}

		#endregion
	}
}