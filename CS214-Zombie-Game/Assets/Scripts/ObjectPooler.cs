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
    private GameObject obj;

    public bool isMultipleMode;

	void Awake() {
		SharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool) {
			for (int i = 0; i < item.amountToPool; i++) {
                AddObject (item);
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
                    return AddObject (item);
                }
            }
        }
        return null;
    }

    private GameObject AddObject(ObjectPoolItem item)
    {
        if(isMultipleMode)
        {
            obj = PhotonNetwork.Instantiate(item.objectToPool.tag,Vector3.zero,Quaternion.identity,0);
        }else{
            obj = (GameObject)Instantiate(item.objectToPool);
        }
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
