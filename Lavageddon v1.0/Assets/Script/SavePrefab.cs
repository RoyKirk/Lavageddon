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

    public GameObject BoatExample;

    public struct blockinfo
    {
        public Vector3 trans;
        public char Btype;
    }

    public List<blockinfo> Boat = new List<blockinfo>();
    public int playernumber = 0;

    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat.txt";// this is the write path to print the boat block details to read from later.

    Vector3[] spawnorigins = new Vector3[4];

    //Vector3[] randRot = new Vector3[6];
    Vector3[] randRot = { new Vector3(0, 0, 0), new Vector3(90, 0, 0), new Vector3(-90, 0, 0), new Vector3(180, 0, 0), new Vector3(90, 0, 0), new Vector3(0, 0, 180) };
                 
    
    void Awake()
    {
         path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat" + playernumber + ".txt";

        spawnorigins[0] = new Vector3(2.02f, 5.05f, -24.24001f);
        spawnorigins[1] = new Vector3(-21.05f, 5.05f, 4.59f);
        spawnorigins[2] = new Vector3(2.02f, 5.05f, 25.78f);
        spawnorigins[3] = new Vector3(20.51f, 5.05f, 2.13f);

        if (!File.Exists(path))
         {
            //string createText = (spawnorigins[playernumber].x.ToString() + "<"+ spawnorigins[playernumber].y.ToString() + "<" + spawnorigins[playernumber].z.ToString() + "<S");
            //File.WriteAllText(path, createText);
            //
            //AddtoList(spawnorigins[playernumber], 'S');
            //
            ////delete the boat example after a time or certain events.
            //Instantiate(BoatExample, spawnorigins[playernumber], Quaternion.identity);
            ////instantiate boat example at ^ pos.
             
         }
    }         
    // Use this for initialization
    void Start ()
    {
        //
        //string createText = "";
        //
        //
        ////for (int i = 0; i < DefaultBoat0.Length; i++)
        ////{
        ////    createText.Insert(LoadedBoat.Length, DefaultBoat0[i]);
        ////}
        ////Debug.Log(createText);
        //File.WriteAllText(path, createText);
        //ReadBoat(false);

        //do some research to get a better path location, preferably this games location

        spawnorigins[0] = new Vector3(2.02f, 5.05f, -24.24001f);
        spawnorigins[1] = new Vector3(-21.05f, 5.05f, 4.59f);
        spawnorigins[2] = new Vector3(2.02f, 5.05f, 25.78f);
        spawnorigins[3] = new Vector3(20.51f, 5.05f, 2.13f);


        //do some research to get a better path location, preferably this games location
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Boat" + playernumber + ".txt";
        ReadBoat(false);
        if (!File.Exists(path))
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

    //string defaultBoat = "2.02<5.05<-20.20001< F" + Environment.NewLine + "2.02<5.05<-19.19001< F" + Environment.NewLine + "2.02<5.05<-18.18001< F" + Environment.NewLine + "2.02<5.05<-17.17001< F" + Environment.NewLine + "2.02<5.05<-16.16001< F" + Environment.NewLine + "3.03<5.05<-20.20001< F" + Environment.NewLine + "3.03<5.05<-19.19001< F" + Environment.NewLine + "4.04<5.05<-19.19001< F" + Environment.NewLine + "4.04<5.05<-18.18001< F" + Environment.NewLine + "4.04<5.05<-17.17001< F" + Environment.NewLine + "4.04<5.05<-16.16001< F" + Environment.NewLine + "3.03<5.05<-15.15001< F" + Environment.NewLine + "4.04<5.05<-20.20001< F" + Environment.NewLine + "4.04<5.05<-15.15001< F" + Environment.NewLine + "2.02<5.05<-15.15001< F" + Environment.NewLine + "5.05<5.05<-20.20001< F" + Environment.NewLine + "5.05<5.05<-17.17001< F" + Environment.NewLine + "5.05<5.05<-16.16001< F" + Environment.NewLine + "5.05<5.05<-19.19001< F" + Environment.NewLine + "5.05<5.05<-18.18001< F" + Environment.NewLine + "5.05<5.05<-15.15001< F" + Environment.NewLine + "1.01<5.05<-20.20001< F" + Environment.NewLine + "1.01<5.05<-19.19001< F" + Environment.NewLine + "1.01<5.05<-18.18001< F" + Environment.NewLine + "1.01<5.05<-17.17001< F" + Environment.NewLine + "1.01<5.05<-16.16001< F" + Environment.NewLine + "1.01<5.05<-15.15001< F" + Environment.NewLine + "6.06<5.05<-20.20001< F" + Environment.NewLine + "6.06<5.05<-19.19001< F" + Environment.NewLine + "6.06<5.05<-18.18001< F" + Environment.NewLine + "6.06<5.05<-17.17001< F" + Environment.NewLine + "6.06<5.05<-16.16001< F" + Environment.NewLine + "6.06<5.05<-15.15001< F" + Environment.NewLine + "0<5.05<-20.20001< F" + Environment.NewLine + "0<5.05<-19.19001< F" + Environment.NewLine + "0<5.05<-18.18001< F" + Environment.NewLine + "0<5.05<-17.17001< F" + Environment.NewLine + "0<5.05<-16.16001< F" + Environment.NewLine + "0<5.05<-15.15001< F" + Environment.NewLine + "0<5.05<-14.14001< F" + Environment.NewLine + "1.01<5.05<-14.14001< F" + Environment.NewLine + "2.02<5.05<-14.14001< F" + Environment.NewLine + "4.04<5.05<-14.14001< F" + Environment.NewLine + "5.05<5.05<-14.14001< F" + Environment.NewLine + "6.06<5.05<-14.14001< F" + Environment.NewLine + "3.03<5.05<-18.18001< F" + Environment.NewLine + "5.05<5.05<-13.13001< F" + Environment.NewLine + "4.04<5.05<-13.13001< F" + Environment.NewLine + "3.03<5.05<-14.14001< F" + Environment.NewLine + "3.03<5.05<-13.13001< F" + Environment.NewLine + "2.02<5.05<-13.13001< F" + Environment.NewLine + "1.01<5.05<-13.13001< F" + Environment.NewLine + "6.06<5.05<-13.13001< F" + Environment.NewLine + "0<5.05<-13.13001< F" + Environment.NewLine + "3.03<5.05<-12.12001< F" + Environment.NewLine + "1.01<5.05<-12.12001< F" + Environment.NewLine + "1.01<5.05<-11.11001< F" + Environment.NewLine + "3.03<5.05<-11.11001< F" + Environment.NewLine + "2.02<5.05<-11.11001< F" + Environment.NewLine + "4.04<5.05<-11.11001< F" + Environment.NewLine + "5.05<5.05<-12.12001< F" + Environment.NewLine + "5.05<5.05<-11.11001< F" + Environment.NewLine + "3.03<5.05<-16.16001< S";
    //
    //string LoadedBoat = "";
    //
    //string[] DefaultBoat0 = { "3.03<5.05<-24.24001< F" + Environment.NewLine, "4.04<5.05<-24.24001< F"+ Environment.NewLine,"6.06<5.05<-24.24001< F"+ Environment.NewLine,
    //    "6.06<5.05<-23.23001< F" + Environment.NewLine,"6.06<5.05<-22.22001< F"+ Environment.NewLine,"6.06<5.05<-21.21001< F"+ Environment.NewLine, "6.06<5.05<-20.20001< F"+ Environment.NewLine,
    //    "5.05<5.05<-23.23001< F" + Environment.NewLine,"5.05<5.05<-22.22001< F"+ Environment.NewLine, "5.05<5.05<-21.21001< F"+ Environment.NewLine, "5.05<5.05<-20.20001< F"+ Environment.NewLine,
    //    "4.04<5.05<-23.23001< F" + Environment.NewLine,"4.04<5.05<-22.22001< F"+ Environment.NewLine, "4.04<5.05<-21.21001< F"+ Environment.NewLine,"4.04<5.05<-20.20001< F"+ Environment.NewLine,
    //    "3.03<5.05<-23.23001< F" + Environment.NewLine,"3.03<5.05<-22.22001< F" + Environment.NewLine, "3.03<5.05<-21.21001< F"+ Environment.NewLine,"3.03<5.05<-20.20001< F"+ Environment.NewLine,
    //    "2.02<5.05<-23.23001< F" + Environment.NewLine, "2.02<5.05<-22.22001< F"+ Environment.NewLine,"2.02<5.05<-21.21001< F"+ Environment.NewLine, "2.02<5.05<-20.20001< F"+ Environment.NewLine,
    //    "5.05<6.06<-21.21001< F" + Environment.NewLine, "5.05<6.06<-23.23001< F"+ Environment.NewLine,
    //    "5.05<6.06<-22.22001< F"+ Environment.NewLine, "4.04<6.06<-21.21001< S"+ Environment.NewLine,"2.02<5.05<-24.24001< A"+ Environment.NewLine,
    //    "2.02<6.06<-24.24001< A"+ Environment.NewLine,"2.02<6.06<-23.23001< A"+ Environment.NewLine,"2.02<6.06<-22.22001< A"+ Environment.NewLine,"2.02<6.06<-21.21001< A"+ Environment.NewLine,
    //    "2.02<6.06<-20.20001< A"+ Environment.NewLine,"3.03<6.06<-20.20001< A"+ Environment.NewLine,"4.04<6.06<-20.20001< A"+ Environment.NewLine,
    //    "5.05<6.06<-20.20001< A"+ Environment.NewLine,"6.06<6.06<-20.20001< A"+ Environment.NewLine,"6.06<6.06<-21.21001< A"+ Environment.NewLine,"6.06<6.06<-22.22001< A"+ Environment.NewLine,
    //    "6.06<6.06<-23.23001< A"+ Environment.NewLine,"6.06<6.06<-24.24001< A"+ Environment.NewLine,"5.05<6.06<-24.24001< A"+ Environment.NewLine,
    //    "4.04<6.06<-24.24001< A"+ Environment.NewLine,"3.03<6.06<-24.24001< A"+ Environment.NewLine,"4.04<6.06<-22.22001< F"+ Environment.NewLine,"3.03<6.06<-22.22001< F"+ Environment.NewLine,
    //    "3.03<6.06<-21.21001< F"+ Environment.NewLine,"4.04<6.06<-23.23001< F"+ Environment.NewLine,"3.03<6.06<-23.23001< F"+ Environment.NewLine };



    public void ReadBoat(bool create)
    {
        if(!File.Exists(path))
        {
            //for (int i = 0; i < DefaultBoat0.Length; i++)
            //{
            //    LoadedBoat.Insert(LoadedBoat.Length, DefaultBoat0[i]);
            //}
            //string empty = "";
            //File.WriteAllText(path, empty);
            //createStartBlock();
            //ReadBoat(true);
            //create default boat

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
            int rand = UnityEngine.Random.Range(0, 5);
            //Debug.Log(rand);
            String[] values = blockLine.Split(splitChar);
            Vector3 pos = new Vector3((float.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)),(float.Parse(values[2], System.Globalization.CultureInfo.InvariantCulture.NumberFormat)));

            if (testoutcome = blockLine.Contains(Flo))//float block
            {
                if (create)
                {
                    //Debug.Log(randRot[rand]);
                    GameObject block = Instantiate(blockFloat, pos, Quaternion.Euler(randRot[rand])) as GameObject;//loads the boat
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
                    GameObject block = Instantiate(blockArmor, pos, Quaternion.Euler(randRot[rand])) as GameObject;//loads the boat
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

    

    public void createStartBlock()
    {
        DeleteAll();
        if(Boat.Count == 0)
        {
            GameObject block = (GameObject)Instantiate(blockSpawn, spawnorigins[playernumber], Quaternion.identity);
            block.GetComponent<BuildingBlock>().playerOwner = playernumber;
            AddtoList(spawnorigins[playernumber], 'S');
            GetComponent<managerscript>().spawnPos = spawnorigins[playernumber];
            GetComponent<managerscript>().spawnblock = true;
            //GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().numberOfBlocks += GameObject.Find("Player" + playernumber + "(Clone)").GetComponentInChildren<managerscript>().FloatBlockCost;

        }
    }

    public void DeleteAll()
    {
        GameObject[] AllBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject go in AllBlocks)
        {
            if(go.GetComponent<BuildingBlock>().playerOwner == playernumber)
            {
                RemovefromList(go.transform.position);
                Destroy(go);
            }
        }
        GetComponent<managerscript>().numberOfBlocks = 0;
    }
}
