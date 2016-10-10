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
    public GameObject blockSpawn;
    public struct blockinfo
    {
        public Vector3 trans;
        public char Btype;
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
        if(!File.Exists(path))
        {
            string createText = "";
            File.WriteAllText(path, createText);
        }
	}
	
    public void WriteBoat()
    {
        string createText = "";
        File.WriteAllText(path, createText);
        
        foreach (blockinfo block in Boat)
        {
            string blockN = block.trans.x + "<" + block.trans.y + "<" + block.trans.z + "< " + block.Btype + Environment.NewLine;
            File.AppendAllText(path, blockN);
        }
    }

    public void ReadBoat(bool create)
    {
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "");
            createStartBlock();
        }
        string Flo = "F";
        string Arm = "A";
        bool testoutcome;
        
        char splitChar = '<';

        managerscript ms = GetComponent<managerscript>();

        foreach (string blockLine in File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat" + playernumber + ".txt"))
        {
            String[] values = blockLine.Split(splitChar);
            Vector3 pos = new Vector3((float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)));

            if (testoutcome = blockLine.Contains(Flo))//float block
            {
                if (create)
                {
                    GameObject block = Instantiate(blockFloat, pos, Quaternion.identity) as GameObject;//loads the boat
                    block.GetComponent<BuildingBlock>().playerOwner = playernumber;
                    //ms.LoadBoatPlacement(0, pos);
                    //GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().numberOfBlocks += GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().FloatBlockCost;
                }
                else
                {
                    AddtoList(pos, 'F');//adds to the list (this needs to be done in this script
                    GetComponent<managerscript>().numberOfBlocks += GetComponent<managerscript>().FloatBlockCost;
                }
            }
            else if(testoutcome = blockLine.Contains(Arm))//armor block
            {
                if (create)
                {
                    GameObject block = Instantiate(blockArmor, pos, Quaternion.identity) as GameObject;//loads the boat
                    block.GetComponent<BuildingBlock>().playerOwner = playernumber;
                    //ms.LoadBoatPlacement(1, pos);
                    //GameObject.Find("Player" + playernumber + "(Clone)").GetComponent<managerscript>().numberOfBlocks += GameObject.Find("Player" + playernumber + "(Clone)").GetComponent<managerscript>().ArmourBlockCost;
                    
                }
                else
                {
                    AddtoList(pos, 'A');
                    GetComponent<managerscript>().numberOfBlocks += GetComponent<managerscript>().ArmourBlockCost;
                }
            }
            else//spawn block
            {
                if (create)
                {
                    GameObject block = Instantiate(blockSpawn, pos, Quaternion.identity) as GameObject;//loads the boat
                    block.GetComponent<BuildingBlock>().playerOwner = playernumber;
                    //ms.LoadBoatPlacement(1, pos);
                    //GameObject.Find("Player" + playernumber + "(Clone)").GetComponent<managerscript>().numberOfBlocks += GameObject.Find("Player" + playernumber + "(Clone)").GetComponent<managerscript>().ArmourBlockCost;
                    //GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().spawnblock = true;
                    //GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().spawnPos = pos;

                    GetComponent<managerscript>().spawnblock = true;
                    GetComponent<managerscript>().spawnPos = pos;
                    //Debug.Log(GetComponent<managerscript>().spawnblock + GetComponent<managerscript>().spawnPos.y.ToString());
                }
                else
                {
                    AddtoList(pos, 'S');
                }
            }
        }
    }
    

    public void AddtoList(Vector3 pos, char Btype)
    {
        blockinfo block = new blockinfo();
        block.trans = pos;
        block.Btype = Btype;
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

    Vector3 spawnorigin = new Vector3(0, 4.04f, 16.16f);

    public void createStartBlock()
    {
        if(Boat.Count == 0)
        {
            AddtoList(spawnorigin, 'S');
            GameObject block = (GameObject)Instantiate(blockSpawn, spawnorigin, Quaternion.identity);
            block.GetComponent<BuildingBlock>().playerOwner = playernumber;
            GetComponent<managerscript>().spawnPos = spawnorigin;
            GetComponent<managerscript>().spawnblock = true;
            //GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().numberOfBlocks += GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().FloatBlockCost;

        }
    }

    public void DeleteAll(int playerOwner)
    {
        GameObject[] AllBlocks = GameObject.FindGameObjectsWithTag("Block");
        //foreach ( in AllBlocks)
        //{
        //
        //}
    }
}
