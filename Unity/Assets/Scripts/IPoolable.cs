namespace LDJam41 {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using NikCore;

	public interface IPoolable {

		Poolable Poolable {
			get;
		}
	}
}