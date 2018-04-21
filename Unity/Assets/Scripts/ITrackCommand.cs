namespace LDJam41 {

	using UnityEngine;
	using NikInput;

	public interface ITrackCommand {

		Vector2 Position {
			get;
			set;
		}

		float Progress {
			get;
		}

		bool IsHold {
			get;
		}

		float Duration {
			get;
		}

		void UpdateTrackCommand(float dt);
		bool Check(params ControlButton[] heldButtons);

		void Miss();
		void Hit();
	}
}