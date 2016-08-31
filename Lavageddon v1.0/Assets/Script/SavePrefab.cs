using UnityEngine;
using System.Collections;
//using UnityEditor;s
using System.Collections.Generic;

public class SavePrefab : MonoBehaviour
{
    /*CANT USE UNITY EDITOR TO SAVE BOAT PREFAB
     * 
     * will have to create own method that saves the transforms of placed blocks and type of block into a list.
     * then instantiate those blocks from the list, taking away the players resources as well.
     * 
     * create a button that will reset the boat to just one block
     * 
     * side note: creating joints will need to happen at the right time, and the reset will need to have none
     */
    public struct blockinfo
    {
        Vector3 trans;
        bool floating;
    }

    public List<blockinfo> Boat = new List<blockinfo>();


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
        //GameObject prefab = PrefabUtility.CreatePrefab("Assets/Resources/player" + playerINT + "parent" + ".prefab", parent);
        //prefab.transform.parent = playerBoats.transform;
    }
}
