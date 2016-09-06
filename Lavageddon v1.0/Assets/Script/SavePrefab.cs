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

    public GameObject blockFloat;
    public GameObject blockArmor;
    public struct blockinfo
    {
        public Vector3 trans;
        public bool floating;
    }

    public List<blockinfo> Boat = new List<blockinfo>();
    public int playernumber = 0;

    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat.txt";// this is the write path to print the boat block details to read from later.

	// Use this for initialization
	void Start ()
    {
        //do some research to get a better path location, preferably this games location
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat" + playernumber + ".txt";
        ReadBoat(false);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void WriteBoat()
    {
        string createText = "";
        File.WriteAllText(path, createText);
        
        foreach (blockinfo block in Boat)
        {
            string blockN = block.trans.x + "<" + block.trans.y + "<" + block.trans.z + "< " + block.floating + Environment.NewLine;
            File.AppendAllText(path, blockN);
        }
    }

    public void ReadBoat(bool create)
    {
        string test = "True";
        bool testoutcome;

        //int start = 0;
        //int at = 0;
        char splitChar = '<';

        foreach (string blockLine in File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat" + playernumber + ".txt"))
        {
            String[] values = blockLine.Split(splitChar);
            Vector3 pos = new Vector3((float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)));

            if (testoutcome = blockLine.Contains(test))//float block
            {
                if(create)
                {
                    Instantiate(blockFloat, pos, Quaternion.identity);//loads the boat
                }
                else
                {
                    AddtoList(pos, true);//adds to the list (this needs to be done in this script
                }
            }
            else//armor block
            {
                if (create)
                {
                    Instantiate(blockFloat, pos, Quaternion.identity);
                }
                else
                {
                    AddtoList(pos, false);
                }
            }
        }
    }
    

    public void AddtoList(Vector3 pos, bool floating)
    {
        blockinfo block = new blockinfo();
        block.trans = pos;
        block.floating = floating;
        Boat.Add(block);
    }

    public void RemovefromList(Vector3 pos)
    {
        foreach (blockinfo block in Boat)
        {
            if(block.trans == pos)
            {
                Boat.Remove(block);
                break;
            }
        }
    }

    public void ResetBoat()
    {
        //Destroy()
    }
    
}
