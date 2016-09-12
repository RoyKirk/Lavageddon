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
     * at the moment the left stick button will create a starting block for the player if there is none.
     * 
     * create a button that will reset the boat to just one block
     * the current function will need to be changed to delete all of your current blocks in the scene and re-instantiate the default.
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
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "");
            ResetBoat();
        }
        string test = "True";
        bool testoutcome;
        
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
        //0<4.04<16.16< True
        //foreach (blockinfo block in Boat)
        //{
        //    if (block.trans == pos)
        //    {
        //        Boat.Remove(block);
        //        break;
        //    }
        //}

        AddtoList(new Vector3(0, 4.04f, 16.16f), true);
        Instantiate(blockFloat, new Vector3(0, 4.04f, 16.16f), Quaternion.identity);
    }

}
