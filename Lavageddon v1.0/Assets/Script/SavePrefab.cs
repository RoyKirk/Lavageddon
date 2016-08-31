using UnityEngine;
using System.Collections;
//using UnityEditor;s
using System.Collections.Generic;
using System;
using System.IO;

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
        public Vector3 trans;
        public bool floating;
    }

    public List<blockinfo> Boat = new List<blockinfo>();

    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat.txt";// this is the write path to print the boat block details to read from later.

	// Use this for initialization
	void Start ()
    {
        //do some research to get a better path location, preferably this games location
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat.txt";
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void WriteBoat()
    {
        if(!File.Exists(path))//if the file doesnt already exist (not sure if we need this as the file should always be overridden at this stage)
        {
            
        }

        string createText = "BOATS!";
        File.WriteAllText(path, createText);

        int blockCounter = 0;

        foreach (blockinfo block in Boat)
        {
            string blockN = Environment.NewLine + "Block" + blockCounter + " = <" + block.trans.x + "> <" + block.trans.y + "> <" + block.trans.z + "> <" + block.floating + ">";
            File.AppendAllText(path, blockN);
            blockCounter++;
        }
    }

    public void ReadBoat()
    {
        //foreach (string line in File.ReadLines(@"d:\data\episodes.txt"))
        //{
        //    if (line.Contains("episode") & line.Contains("2006"))
        //    {
        //        Console.WriteLine(line);
        //    }
        //}

        foreach (string blockLine in File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat.txt"))
        {
            Debug.Log(blockLine);
        }
    }

    public void AddtoList(Vector3 pos, bool floating)
    {
        blockinfo block = new blockinfo();
        block.trans = pos;
        block.floating = floating;
        Boat.Add(block);
        //Debug.Log("Adding: " + block.trans.x + " " + block.trans.y + " " + block.trans.z);
    }

    public void RemovefromList(Vector3 pos)
    {
        foreach (blockinfo block in Boat)
        {
            if(block.trans == pos)
            {
                Boat.Remove(block);
                //Debug.Log("Removed: "+block.trans.x + " " + block.trans.y + " " + block.trans.z);
                break;
            }
        }
    }

    public void createblocks()
    {
        //for(int i = 0; i < Boat.Count; i++)
        //{
        //    Debug.Log(Boat.);
        //}
        Debug.Log("creating boats for players");

        foreach (blockinfo block in Boat)
        {
            Debug.Log("created: "+block.trans.x + " " + block.trans.y + " " + block.trans.z);
        }
    }
}
