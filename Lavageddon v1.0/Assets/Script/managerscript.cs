using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;


public class managerscript : MonoBehaviour {

    public enum BlockType
    {
        FLOAT,
        ARMOUR,
        SPAWN,
        FLOAT3X3X3,
        ARMOUR3X3X3,

    };
    int numberOfBlockTypes = 3;
    public bool blockdestroyed = false;
    public GameObject blockPlacePrefabFloat;
    public GameObject blockPlacePrefabArmour;
    public GameObject blockPlacePrefabFloat3X3X3;
    public GameObject blockPlacePrefabArmour3X3X3;
    public GameObject blockPlacePrefabFloatInFront;
    public GameObject blockPlacePrefabArmourInFront;
    public GameObject blockPlacePrefabFloat3X3X3InFront;
    public GameObject blockPlacePrefabArmour3X3X3InFront;
    public GameObject blockPlacePrefabSpawn;
    public GameObject blockPrefabFloat;
    public GameObject blockPrefabArmour;
    public GameObject blockPrefabFloat3X3X3;
    public GameObject blockPrefabArmour3X3X3;
    public GameObject blockPrefabSpawn;
    GameObject block;
    GameObject blockInFront;
    public float blockDistance = 5f;
    public float placementReach = 1000f;
    public float placementOffset = 1.2f;
    public bool constructionMode = true;
    public BlockType blockType = BlockType.FLOAT;
    bool startConstruction = true;
    public float startDistance = 10;
    public int maxNumberOfBlocks = 100;
    public int numberOfBlocks = 0;
    public Text numberText;
    public int player = 0;

    public int FloatBlockCost;
    public int ArmourBlockCost;

    float blockTimer = 0;
    float blockTime = 0.1f;

    //reference to option variables
    GameObject playerManager;

    //GameObject boatParent;

    public bool testingboat = false;
    public bool rejoin = true;
    public bool saved;
    public SavePrefab save;

    public bool spawnblock;

    public GameObject BlockTurner;

    public Animator anim;

    public GameObject ConstructionCrosshair;

    public GameObject FloatBlockPlacementAudio;
    public GameObject ArmourBlockPlacementAudio;

    public GameObject itemSwitchAudio;

    //List<GameObject> boat = new List<GameObject>();
    // Use this for initialization

    Vector3[] randRot = new Vector3[27];

    //these are for reseting the spawn block if placed a second
    GameObject findSpawnBlock;
    public bool testNewSpawnBlock = false;
    bool confirm = false;
    const float newSpawnCounterMax = 1;
    float newSpawnCounter;

    void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        //variables are set to options variables
        maxNumberOfBlocks = (int)DV.BlockRelated[0];
        //Debug.Log("getting var " + (int)DV.BlockRelated[0]);
        FloatBlockCost = (int)DV.BlockRelated[2];
        ArmourBlockCost = (int)DV.BlockRelated[6];
    }
    void Start ()
    {
        randRot[0] = new Vector3(0, 0, 0);

        randRot[1] = new Vector3(0, 90, 0);
        randRot[2] = new Vector3(0, 180, 0);
        randRot[3] = new Vector3(0, 0, 90);
        randRot[4] = new Vector3(0, 0, 180);
        randRot[5] = new Vector3(0, 90, 90);
        randRot[6] = new Vector3(0, 180, 180);
        randRot[7] = new Vector3(0, 90, 180);
        randRot[8] = new Vector3(0, 180, 90);

        randRot[9] = new Vector3(90, 0, 0);
                                 
        randRot[10] = new Vector3(90, 90, 0);
        randRot[11] = new Vector3(90, 180, 0);
        randRot[12] = new Vector3(90, 0, 90);
        randRot[13] = new Vector3(90, 0, 180);
        randRot[14] = new Vector3(90, 90, 90);
        randRot[15] = new Vector3(90, 180, 180);
        randRot[16] = new Vector3(90, 90, 180);
        randRot[17] = new Vector3(90, 180, 90);

        randRot[18] = new Vector3(180, 0, 0);
                                 
        randRot[19] = new Vector3(180, 90, 0);
        randRot[20] = new Vector3(180, 180, 0);
        randRot[21] = new Vector3(180, 0, 90);
        randRot[22] = new Vector3(180, 0, 180);
        randRot[23] = new Vector3(180, 90, 90);
        randRot[24] = new Vector3(180, 180, 180);
        randRot[25] = new Vector3(180, 90, 180);
        randRot[26] = new Vector3(180, 180, 90);

        newSpawnCounter = newSpawnCounterMax;

        if (blockType == BlockType.FLOAT)
        {
            //GameObject temp = (GameObject)Instantiate(blockPrefabFloat, transform.position + transform.forward * startDistance - transform.up * startDistance / 4, new Quaternion(0, 0, 0, 0));
            //temp.GetComponent<BlockDamage>().keystone = true;
        }
        if (blockType == BlockType.ARMOUR)
        {
            //GameObject temp = (GameObject)Instantiate(blockPrefabArmour, transform.position + transform.forward * startDistance - transform.up * startDistance / 4, new Quaternion(0, 0, 0, 0));
            //temp.GetComponent<BlockDamage>().keystone = true;
        }

        //boatParent = GameObject.FindGameObjectWithTag("boatPrefab" + player);
        //save.parent = boatParent;
    }

    public bool resetboatcheck = false;
    const float resetboatcounterMAX = 2.0f;
    float resetboutcounter;

    // Update is called once per frame
    void Update ()
    {
        if(!constructionMode && !saved)
        {
            //testingboat = false;
            save.WriteBoat();
            saved = true;
            //get the offset of the camera to move the body pos, to above block and adjust the camera to the correct pos.
            //Vector3 temp = transform.position - GetComponent<PlayerMovement>().body.transform.position;
            spawnPos.y += 1;
            Vector3 temp1 = transform.localPosition;
            Vector3 temp2 = GetComponent<PlayerMovement>().body.transform.localPosition;
            GetComponent<PlayerMovement>().body.transform.parent.transform.position = spawnPos;// + temp;
            transform.localPosition = temp1;
            GetComponent<PlayerMovement>().body.transform.localPosition = temp2;
            BlockTurner.SetActive(false);
            ConstructionCrosshair.SetActive(false);

            //GetComponent<PlayerMovement>().body.transform.position = spawnPos;
            //transform.position = GetComponent<PlayerMovement>().body.transform.position + temp;
            //transform.position = spawnPos;
            //transform.position -= GetComponent<PlayerMovement>().body.transform.localPosition;
            //GetComponent<PlayerMovement>().body.transform.position = spawnPos;
            numberText.text = "";
        }
        else
        {
            numberText.text = "No. of Blocks = " + (maxNumberOfBlocks - numberOfBlocks);
        }

	    if(blockdestroyed)
        {
            FixedJoint[] joints = FindObjectsOfType(typeof(FixedJoint)) as FixedJoint[];
            foreach (FixedJoint joint in joints)
            {
                if (!joint.connectedBody)
                {
                    Destroy(joint);
                }
            }
            blockdestroyed = false;
        }

        if (Controller.prevState[player].Buttons.Guide == ButtonState.Released && Controller.state[player].Buttons.Guide == ButtonState.Pressed)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
                     Application.OpenURL(webplayerQuitURL);
            #else
                     Application.Quit();
            #endif
        }

        if (constructionMode && !testingboat)
        {
            saved = false;
            if (resetboutcounter < 0)
            {
                resetboatcheck = false;
            }
            else
            {
                resetboutcounter -= Time.deltaTime;
            }
            if (numberOfBlocks == 0 && spawnblock == false)
            {
                resetboatcheck = true;
            }

            if (Controller.prevState[player].Buttons.LeftStick == ButtonState.Released && Controller.state[player].Buttons.LeftStick == ButtonState.Pressed && resetboatcheck)
            {
                save.createStartBlock();
                resetboatcheck = false;
            }

            //if the player presses the left stick in, reset the boat // set up a check to confirm they want to reset the boat, unless they have no blocks.
            if (Controller.prevState[player].Buttons.LeftStick == ButtonState.Released && Controller.state[player].Buttons.LeftStick == ButtonState.Pressed && !resetboatcheck)
            {
                //they have pressed it once, que a confirm sentence with a timer. they have to press button again to confirm or not to cancel
                resetboatcheck = true;//set this to true auto if they have no blocks, so you dont need to confirm.
                resetboutcounter = resetboatcounterMAX;
            }
            //they have confirmed that they want to reset their boat.
            

            //timer so they have X seconds to press the reset button again before canceling
            

            if (Controller.prevState[player].Triggers.Right < 0.2 && Controller.state[player].Triggers.Right > 0.2 && Controller.state[player].Triggers.Right < 0.9 && block && block.GetComponent<PlacementBlockScript>().placeable && numberOfBlocks < maxNumberOfBlocks)
            {
                PlaceBlock();
                //numberOfBlocks += FloatBlockCost;
            }

            if (Controller.state[player].Triggers.Right > 0.9 && block && block.GetComponent<PlacementBlockScript>().placeable)// && numberOfBlocks < maxNumberOfBlocks)
            {
                blockTimer += Time.deltaTime;

                if (blockTimer >= blockTime)
                {
                    PlaceBlock();
                    blockTimer = 0;
                }
            }

            if (Controller.state[player].Triggers.Right < 0.2 && testNewSpawnBlock)
            {
                confirm = true;
            }
            else
            {
                confirm = false;
            }

            if (testNewSpawnBlock)
            {
                newSpawnCounter -= Time.deltaTime;
            }
            if (newSpawnCounter <= 0)
            {
                testNewSpawnBlock = false;
                newSpawnCounter = newSpawnCounterMax;
            }

            if (Controller.prevState[player].Triggers.Left < 0.2 && Controller.state[player].Triggers.Left > 0.2)
            {
                RemoveBlock();
            }
            if (Controller.state[player].Triggers.Left > 0.9)
            {
                blockTimer += Time.deltaTime;

                if (blockTimer >= blockTime)
                {
                    RemoveBlock();
                    blockTimer = 0;
                }
            }
            if (Controller.prevState[player].Triggers.Left > 0.9 && Controller.state[player].Triggers.Left < 0.9)
            {
                blockTimer = 0;
            }


            if (Controller.prevState[player].Buttons.RightShoulder == ButtonState.Released && Controller.state[player].Buttons.RightShoulder == ButtonState.Pressed)
            {
                blockType++;
                ResetBlock();
                BlockTurner.transform.Rotate(0, 0, 120);
                Instantiate(itemSwitchAudio, transform.position, Quaternion.identity);
            }
            if (Controller.prevState[player].Buttons.LeftShoulder == ButtonState.Released && Controller.state[player].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                blockType--;
                ResetBlock();
                BlockTurner.transform.Rotate(0, 0, -120);
                Instantiate(itemSwitchAudio, transform.position, Quaternion.identity);
            }

            if((int)blockType == numberOfBlockTypes)
            {
                blockType = (BlockType)0;
                ResetBlock();
            }
            else if((int)blockType < 0)
            {
                blockType = (BlockType)(numberOfBlockTypes -1);
                ResetBlock();
            }


            
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            ////if (Physics.Raycast(ray, out hit, placementReach))
            //if (Physics.Raycast(transform.position, transform.forward, out hit, placementReach))
            //{

            //    //Debug.DrawLine(ray.origin, hit.point);
            //    Debug.DrawLine(transform.position, hit.point);

            //    if (hit.collider.tag == "Block")
            //    {
            //        if (startConstruction)
            //        {
            //            if (blockType == BlockType.FLOAT)
            //            {
            //                block = (GameObject)Instantiate(blockPlacePrefabFloat, hit.collider.transform.position, hit.collider.transform.rotation);
            //            }
            //            if (blockType == BlockType.ARMOUR)
            //            {
            //                block = (GameObject)Instantiate(blockPlacePrefabArmour, hit.collider.transform.position, hit.collider.transform.rotation);
            //            }
            //            startConstruction = false;
            //        }

            //        block.transform.rotation = hit.collider.transform.rotation;
            //        block.transform.position = hit.collider.transform.position + hit.normal.normalized * placementOffset;

            //    }
            //    else if (hit.collider.tag != "Block" && hit.collider.tag != "PlaceBlock")
            //    {
            //        Destroy(block);
            //        startConstruction = true;
            //    }



            //}
            //else
            //{
            //    Destroy(block);
            //    startConstruction = true;
            //}
        }
        else
        {
            Destroy(block);
            Destroy(blockInFront);
            startConstruction = true;
        }
        //if (!block.GetComponent<buildingblock>().placed)
        //{
        //    block.transform.position = transform.position + transform.forward.normalized * blockOffset;
        //    block.transform.rotation = transform.rotation;
        //}
        //if (block.GetComponent<buildingblock>().placed)
        //{
        //    block.GetComponent<BoxCollider>().enabled = true;
        //    block = (GameObject)Instantiate(blockPrefab, transform.position + transform.forward.normalized * blockOffset, transform.rotation);
        //    block.GetComponent<BoxCollider>().enabled = false;
        //}
        if (constructionMode)
        {
            if (Controller.prevState[player].Buttons.Back == ButtonState.Released && Controller.state[player].Buttons.Back == ButtonState.Pressed)
            {
                GameObject[] blocks = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject resetBlock in blocks)
                {
                    if (resetBlock.tag == "Block")
                    {
                        resetBlock.GetComponent<BuildingBlock>().ResetBoat();
                    }
                }
                foreach (GameObject resetBlock in blocks)
                {
                    if (resetBlock.tag == "Block")
                    {
                        resetBlock.GetComponent<BuildingBlock>().MakeJoints();
                    }
                }

            }
            if (Controller.prevState[player].Buttons.RightStick == ButtonState.Released && Controller.state[player].Buttons.RightStick == ButtonState.Pressed)
            {//the right stick is being pressed in, atm we want this to "test the boat"
                theyHaveTestedBoat = true;
                testingboat = !testingboat;
                rejoin = !rejoin;
            }

        }
        else
        {
            numberText.text = "";
        }


    }

    public bool theyHaveTestedBoat = false;

    void LateUpdate()
    {

        if (constructionMode && !testingboat)
        {
            //if (Controller.prevState[player].Buttons.A == ButtonState.Released && Controller.state[player].Buttons.A == ButtonState.Pressed && block && block.GetComponent<PlacementBlockScript>().placeable && numberOfBlocks < maxNumberOfBlocks)
            //{
            //    if (blockType == BlockType.FLOAT)
            //    {
            //        //change these to a var to set its parent to a refernce
            //        GameObject blok = Instantiate(blockPrefabFloat, block.transform.position, block.transform.rotation) as GameObject;
            //        blok.transform.parent = boatParent.transform;
            //        numberOfBlocks += FloatBlockCost;
            //    }
            //    if (blockType == BlockType.ARMOUR)
            //    {
            //        GameObject blok = Instantiate(blockPrefabArmour, block.transform.position, block.transform.rotation) as GameObject;
            //        blok.transform.parent = boatParent.transform;
            //        numberOfBlocks += ArmourBlockCost;
            //    }
            //    //numberOfBlocks += FloatBlockCost;
            //}



            RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, placementReach))
            if (Physics.Raycast(transform.position, transform.forward, out hit, placementReach))
            {
                Debug.DrawLine(transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    if (hit.collider.GetComponent<BuildingBlock>().playerOwner == player)
                    {
                        if (startConstruction)
                        {
                            Destroy(blockInFront);
                            if (blockType == BlockType.FLOAT)
                            {
                                block = (GameObject)Instantiate(blockPlacePrefabFloat, hit.collider.transform.position, hit.collider.transform.rotation);
                                //block.GetComponent<BuildingBlock>().playerOwner = player;
                            }
                            if (blockType == BlockType.ARMOUR)
                            {
                                block = (GameObject)Instantiate(blockPlacePrefabArmour, hit.collider.transform.position, hit.collider.transform.rotation);
                                //block.GetComponent<BuildingBlock>().playerOwner = player;
                            }
                            if (blockType == BlockType.FLOAT3X3X3)
                            {
                                block = (GameObject)Instantiate(blockPlacePrefabFloat3X3X3, hit.collider.transform.position, hit.collider.transform.rotation);
                                //block.GetComponent<BuildingBlock>().playerOwner = player;
                            }
                            if (blockType == BlockType.ARMOUR3X3X3)
                            {
                                block = (GameObject)Instantiate(blockPlacePrefabArmour3X3X3, hit.collider.transform.position, hit.collider.transform.rotation);
                                //block.GetComponent<BuildingBlock>().playerOwner = player;
                            }
                            if (blockType == BlockType.SPAWN)
                            {
                                block = (GameObject)Instantiate(blockPlacePrefabSpawn, hit.collider.transform.position, hit.collider.transform.rotation);
                            }
                            startConstruction = false;
                        }
                        if (blockType == BlockType.FLOAT3X3X3 || blockType == BlockType.ARMOUR3X3X3)
                        {
                            block.transform.rotation = hit.collider.transform.rotation;
                            block.transform.position = hit.collider.transform.position + hit.normal.normalized * placementOffset * 2;
                        }
                        else
                        {
                            block.transform.rotation = hit.collider.transform.rotation;
                            block.transform.position = hit.collider.transform.position + hit.normal.normalized * placementOffset;
                        }
                    }
                    else
                    {
                        BlockInFront();
                    }
                }
                else if (hit.collider.tag != "Block" && hit.collider.tag != "PlaceBlock")
                {
                    BlockInFront();
                }
                else
                {
                    BlockInFront();
                }
            }
            else
            {
                BlockInFront();
            }
        }
    }

    void BlockInFront()
    {
        Destroy(blockInFront);
        if (blockType == BlockType.FLOAT)
        {
            blockInFront = (GameObject)Instantiate(blockPlacePrefabFloatInFront, transform.position + transform.forward.normalized*blockDistance, Quaternion.identity);
            //block.GetComponent<BuildingBlock>().playerOwner = player;
        }
        if (blockType == BlockType.ARMOUR)
        {
            blockInFront = (GameObject)Instantiate(blockPlacePrefabArmourInFront, transform.position + transform.forward.normalized * blockDistance, Quaternion.identity);
            //block.GetComponent<BuildingBlock>().playerOwner = player;
        }
        if (blockType == BlockType.FLOAT3X3X3)
        {
            blockInFront = (GameObject)Instantiate(blockPlacePrefabFloat3X3X3InFront, transform.position + transform.forward.normalized * blockDistance, Quaternion.identity);
            //block.GetComponent<BuildingBlock>().playerOwner = player;
        }
        if (blockType == BlockType.ARMOUR3X3X3)
        {
            blockInFront = (GameObject)Instantiate(blockPlacePrefabArmour3X3X3InFront, transform.position + transform.forward.normalized * blockDistance, Quaternion.identity);
            //block.GetComponent<BuildingBlock>().playerOwner = player;
        }
        if (blockType == BlockType.SPAWN)
        {
            blockInFront = (GameObject)Instantiate(blockPlacePrefabSpawn, transform.position + transform.forward.normalized * blockDistance, Quaternion.identity);
        }
        Destroy(block);
        startConstruction = true;
    }

    void ResetBlock()
    {
        if (block)
        {
            Destroy(block);


            if (blockType == BlockType.FLOAT)
            {
                block = (GameObject)Instantiate(blockPlacePrefabFloat, block.transform.position, block.transform.rotation);
            }
            if (blockType == BlockType.ARMOUR)
            {
                block = (GameObject)Instantiate(blockPlacePrefabArmour, block.transform.position, block.transform.rotation);
            }
            if (blockType == BlockType.FLOAT3X3X3)
            {
                block = (GameObject)Instantiate(blockPlacePrefabFloat3X3X3, block.transform.position, block.transform.rotation);
            }
            if (blockType == BlockType.ARMOUR3X3X3)
            {
                block = (GameObject)Instantiate(blockPlacePrefabArmour3X3X3, block.transform.position, block.transform.rotation);
            }
            if (blockType == BlockType.SPAWN)
            {
                block = (GameObject)Instantiate(blockPlacePrefabSpawn, block.transform.position, block.transform.rotation);
            }
        }
    }

    void PlaceBlock()
    {
        if (blockType == BlockType.FLOAT)
        {
            if (numberOfBlocks + FloatBlockCost <= maxNumberOfBlocks)
            {
                Instantiate(FloatBlockPlacementAudio,transform.position,Quaternion.identity);
                BlockPlaceAndCost(blockPrefabFloat, FloatBlockCost);
                //save.AddtoList(block.transform.position, true);//these true or false need to be in relation to float or armour
                //numberOfBlocks += FloatBlockCost;
            }
        }
        if (blockType == BlockType.ARMOUR)
        {
            if (numberOfBlocks + ArmourBlockCost <= maxNumberOfBlocks)
            {
                Instantiate(ArmourBlockPlacementAudio, transform.position, Quaternion.identity);
                BlockPlaceAndCost(blockPrefabArmour, ArmourBlockCost);
                //save.AddtoList(block.transform.position, false);//these true or false need to be in relation to float or armour
                //numberOfBlocks += ArmourBlockCost;
            }
        }
        if (blockType == BlockType.FLOAT3X3X3)
        {
            if (numberOfBlocks + FloatBlockCost * 27 <= maxNumberOfBlocks)
            {
                GameObject blok = Instantiate(blockPrefabFloat3X3X3, block.transform.position, block.transform.rotation) as GameObject;
                foreach (Transform child in blok.transform)
                {
                    child.GetComponent<BuildingBlock>().playerOwner = player;
                    save.AddtoList(child.transform.position, 'F');
                    numberOfBlocks += FloatBlockCost;
                }
            }
        }
        if (blockType == BlockType.ARMOUR3X3X3)
        {
            if (numberOfBlocks + ArmourBlockCost * 27 <= maxNumberOfBlocks)
            {
                GameObject blok = Instantiate(blockPrefabArmour3X3X3, block.transform.position, block.transform.rotation) as GameObject;
                foreach (Transform child in blok.transform)
                {
                    child.GetComponent<BuildingBlock>().playerOwner = player;
                    save.AddtoList(child.transform.position, 'A');
                    numberOfBlocks += ArmourBlockCost;
                }
            }
        }
        //set up a bool that checks if they want to place a spawn block anyway, delete last spawn block and create a new one
        if(blockType == BlockType.SPAWN && spawnblock == false)
        {
            BlockPlaceAndCost(blockPrefabSpawn, 0);
            spawnPos = block.transform.position;
            //save.AddtoList(transform.position, 'S');
            spawnblock = true;
        }
        else if (blockType == BlockType.SPAWN && spawnblock == true)
        {
            //they are trying to place another spawn block
            if (confirm)
            {
                findSpawnBlock = GameObject.Find("BlockSpawn(Clone)");
                if (findSpawnBlock.GetComponent<BuildingBlock>().playerOwner == player)
                {
                    save.RemovefromList(findSpawnBlock.transform.position);
                    Destroy(findSpawnBlock);
                }
                spawnPos = block.transform.position;
                BlockPlaceAndCost(blockPrefabSpawn, 0);
                spawnblock = true;
            }
            testNewSpawnBlock = true;

        }
    }

    public Vector3 spawnPos;

    int prevRand = 0;

    void BlockPlaceAndCost(GameObject blockPrefab, int blockCost)
    {
        anim.SetTrigger("green");
        //anim.SetBool("GREEN", true);
        GameObject blok;
        int rand = Random.Range(0, 26);
        if (rand == prevRand)
        {
            rand = Random.Range(0, 26);
        }
        prevRand = rand;
        if (blockCost == 0)
        {
            blok = Instantiate(blockPrefab, block.transform.position, Quaternion.Euler(randRot[0])) as GameObject;
            blok.GetComponent<BuildingBlock>().playerOwner = player;
        }
        else
        {
            blok = Instantiate(blockPrefab, block.transform.position, Quaternion.Euler(randRot[rand])) as GameObject;
            blok.GetComponent<BuildingBlock>().playerOwner = player;
        }
        
        //Debug.Log(block.transform.position);
        if (blockCost == FloatBlockCost)
        {
            save.AddtoList(blok.transform.position, 'F');
        }
        else if(blockCost == ArmourBlockCost)
        {
            save.AddtoList(blok.transform.position, 'A');
        }
        else
        {
            blok.transform.rotation = Quaternion.identity;
            save.AddtoList(blok.transform.position, 'S');
        }
        numberOfBlocks += blockCost;
        //anim.SetBool("GREEN", false);
    }

    //void BlockPlaceAndCost3X3(GameObject blockPrefab, int blockCost)
    //{
    //    if (numberOfBlocks + blockCost * 27 <= maxNumberOfBlocks)
    //    {
    //        GameObject blok = Instantiate(blockPrefab, block.transform.position, block.transform.rotation) as GameObject;
    //        foreach (Transform child in blok.transform)
    //        {
    //            child.GetComponent<BuildingBlock>().playerOwner = player;
    //            if(blockCost == ArmourBlockCost)
    //            {
    //                save.AddtoList(child.transform.position, false);
    //            }
    //            else
    //            {
    //                save.AddtoList(child.transform.position, true);
    //            }
    //            
    //            numberOfBlocks += blockCost;
    //        }
    //    }
    //}

    public void LoadBoatPlacement(int blockID, Vector3 pos)
    {
        Debug.Log(maxNumberOfBlocks);
        int rand = Random.Range(0, 5);
        switch (blockID)
        {
            case 0:
                if (numberOfBlocks + FloatBlockCost <= maxNumberOfBlocks)
                {
                    GameObject blok = Instantiate(blockPrefabFloat, pos, Quaternion.Euler(randRot[rand])) as GameObject;
                    blok.GetComponent<BuildingBlock>().playerOwner = player;
                    save.AddtoList(blok.transform.position, 'F');
                    numberOfBlocks += FloatBlockCost;
                    Debug.Log("asd");
                }
                break;
            case 1:
                if (numberOfBlocks + ArmourBlockCost <= maxNumberOfBlocks)
                {
                    GameObject blok = Instantiate(blockPrefabArmour, pos, Quaternion.Euler(randRot[rand])) as GameObject;
                    blok.GetComponent<BuildingBlock>().playerOwner = player;
                    save.AddtoList(blok.transform.position, 'A');
                    numberOfBlocks += ArmourBlockCost;
                    Debug.Log("aasdasd");
                }
                break;
        }
    }

    void RemoveBlock()
    {
        anim.SetTrigger("red");
        RaycastHit shot;
        if (Physics.Raycast(transform.position, transform.forward, out shot))
        {
            Debug.DrawLine(transform.position, shot.point);
            if (shot.collider.tag == "Block")
            {
                if (!shot.collider.GetComponent<BlockDamage>().keystone && shot.collider.GetComponent<BuildingBlock>().playerOwner == player)
                {
                    shot.collider.GetComponent<BlockDamage>().Damage(shot.collider.GetComponent<BlockDamage>().HitPoints);
                    save.RemovefromList(shot.collider.transform.position);
                    numberOfBlocks -= shot.collider.GetComponent<BlockDamage>().cost;
                    if(shot.collider.name == "BlockSpawn(Clone)")
                    {
                        spawnblock = false;
                    }
                    //Debug.Log(shot.collider.transform.position);
                }
            }
        }
        //anim.SetBool("RED", false);
    }
}
