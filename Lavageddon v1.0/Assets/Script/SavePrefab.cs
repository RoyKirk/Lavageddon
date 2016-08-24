using UnityEngine;
using System.Collections;
using UnityEditor;

public class SavePrefab : MonoBehaviour {

    public GameObject parent;
    //public GameObject playerBoats;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void CreatePrefab()
    {
        GameObject prefab = PrefabUtility.CreatePrefab("Assets/Resources/player1parent" + ".prefab", parent);
        //prefab.transform.parent = playerBoats.transform;
    }
}
