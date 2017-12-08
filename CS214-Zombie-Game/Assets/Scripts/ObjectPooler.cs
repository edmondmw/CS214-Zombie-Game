using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
	public int amountToPool;
	public GameObject objectToPool;
	public bool shouldExpand;
}



public class ObjectPooler : MonoBehaviour {

	public List<ObjectPoolItem> itemsToPool;
	public static ObjectPooler SharedInstance;
	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	void Awake() {
		SharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive(false);
				pooledObjects.Add(obj);
			}
		}
	}
	
	public GameObject GetPooledObject(string tag) {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
				return pooledObjects[i];
			}
		}
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					GameObject obj = (GameObject)Instantiate(item.objectToPool);
					obj.SetActive(false);
					pooledObjects.Add(obj);
					return obj;
				}
			}
		}
		return null;
	}



	/*Instantiate(playerBullet, turret.transform.position, turret.transform.rotation);
	 * 
	 * replace by 
	 * 
	 * GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(); 
  *if (bullet != null) {
   * bullet.transform.position = turret.transform.position;
    *bullet.transform.rotation = turret.transform.rotation;
   * bullet.SetActive(true);
  *}
	 * 
	 * 
	 * 
	 * 
	 * 
	 * */
}
