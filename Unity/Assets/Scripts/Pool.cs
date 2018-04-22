namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public class Pool : MonoBehaviour {

		#region Serialized Fields

		[SerializeField]
		public int Count;

		[SerializeField]
		public int PoolPadding;

		#endregion


		#region Public Properties

		public Poolable Poolable {
	       get {
		      m_Poolable = m_Poolable ?? GetComponentInChildren<Poolable>();
		      return m_Poolable;
	       }
        }

		#endregion

		#region Private Properties

        private List<Poolable> Poolables {
	       get {
		      m_Poolables = m_Poolables ?? new List<Poolable>();
		      return m_Poolables;
	       }
        }

        #endregion


		#region Fields

        private Poolable m_Poolable;
		private List<Poolable> m_Poolables;

		#endregion


		#region Monobehaviour

		private void Awake() {

			FillPool(this.Count);
		}

		#endregion


		#region Public Methods

		public Poolable Get() {

			if (this.Poolables.Count == 0) {
				FillPool(this.PoolPadding);
			}

			Poolable poolable = this.Poolables[this.Poolables.Count-1];
			this.Poolables.Remove(poolable);
			poolable.ShouldRepool += Repool;
			poolable.gameObject.SetActive(true);

			return poolable;
		}

		#endregion


		#region Private Methods

		private void FillPool(int count) {

			for (int i = 0; i < count; i++) {
				GameObject newPoolableObject = Instantiate(this.Poolable.gameObject) as GameObject;
				AddToPool(newPoolableObject.GetComponent<Poolable>());
			}

			this.Poolable.gameObject.SetActive(false);
		}

		private void AddToPool(Poolable poolable) {

			poolable.transform.parent = transform;
			poolable.gameObject.SetActive(false);
			this.Poolables.Add(poolable);
		}

		private void Repool(Poolable poolable) {

			poolable.ShouldRepool -= Repool;
			AddToPool(poolable);
		}

		#endregion
	}
}