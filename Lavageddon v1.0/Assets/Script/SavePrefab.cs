using UnityEngine;
using System.Collections;
using UnityEditor;

public class SavePrefab : MonoBehaviour {

    public GameObject parent;

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
        GameObject prefab = PrefabUtility.CreatePrefab("Assets/Temp/" + parent.name + ".prefab", parent);
    }
}
