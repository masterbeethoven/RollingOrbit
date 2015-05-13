using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	
	public static bool prepopulate = false;
	public static GameObject objectPool;

	
	#region BulletPool
	const string PREFAB_PICKUP = "PickUp";
	public static int maxPickups = 12;
	public static Queue<GameObject> pickups;
	#endregion
	
	
	void Start ()
	{	
		objectPool = this.gameObject;
		//enemyPool = this.gameObject;
		//pickupPool = this.gameObject;

		
		pickups = new Queue<GameObject>();

		
		
		for (int i = 1; i <= 12; i++)
		{
			ObjectPool.GetPickups();
		}
		
	}
	
	public static GameObject GetPickups(){
		return GetFromPool(pickups, PREFAB_PICKUP);
	}

	
	public static void RemovePickups(GameObject line){
		AddToPool(line, pickups, maxPickups);
	}
	
	private static GameObject GetFromPool(Queue<GameObject> pool, string prefab){
		if(pool.Count == 0){
			return (GameObject)Instantiate(Resources.Load(prefab, typeof(GameObject)), new Vector3(Random.Range(0, 5),Random.Range(0,5), Random.Range(0,5)), Quaternion.identity);
			
			
		}
		
		GameObject result = pool.Dequeue();
		result.transform.parent = null;
		
		result.SetActive(true);
		result.GetComponent<Poolable>().Reset();

		//from bulletscript
		
		return result;	
	}
	
	private static void AddToPool(GameObject obj, Queue<GameObject> pool, int size){
		if(pool.Count >= size){
			Destroy(obj);
		} else {
			obj.transform.parent = objectPool.transform;
			obj.SetActive(false);
			
			pool.Enqueue(obj);
		}
	}
	
}
