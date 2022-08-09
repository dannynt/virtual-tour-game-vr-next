using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pools for different types of gameobjects.
/// </summary>
public class Pool
{
	// Gameobjects in pool.
	private List<GameObject> objects;
	// Current gameobject "pointer".
	private int currentObjecIndexInPool;

	/// <summary>
	/// Sets the assets and initializes objects.
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	/// <param name="objectCount">Object count.</param>
	public void SetAssets(GameObject prefab, int objectCount)
	{
		objects = new List<GameObject> ();
		currentObjecIndexInPool = 0;

		for (int i = 0; i < objectCount; i++) 
		{
			GameObject go = Object.Instantiate (prefab, new Vector3 (-10, -10, 0), Quaternion.identity);
			objects.Add(go);
		}
	}


	/// <summary>
	/// Gets the next object from pool.
	/// </summary>
	/// <returns>The Next object.</returns>
	public GameObject GetNext()
	{
		GameObject currentObject = objects [currentObjecIndexInPool];

		// Start from beginning.
		if (currentObjecIndexInPool + 1 == objects.Count) 
		{
			currentObjecIndexInPool = 0;
		} 
		// Move to next object with currentObjectIndexInPool.
		else 
		{
			currentObjecIndexInPool++;
		}

		return currentObject;
	}
}
