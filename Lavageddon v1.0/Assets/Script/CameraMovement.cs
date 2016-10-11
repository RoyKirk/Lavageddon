using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

    public float movementSpeed;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY;
    float rotationX;
    public int player = 0;

    public float lavaHeight = 2.0f;
    public GameObject redScreen;

    public GameObject body;

    public Vector3 thirdPersonoffset;

    Rigidbody bodyRB;

    //public SavePrefab save;

    private Vector3 initialVector = Vector3.forward;

    float initialXRotate;
    float previousRotation;

    //Construction UI elements
    public Text spawnblockWarning;
    public Text readyText;
    public Text pressToReady;
    public Text testBoat;

    public managerscript MS;
    public bool spawnPosGood = true;

    void Awake()
    {
        MS = GetComponent<managerscript>();
    }

    void Update()
    {

        bodyRB.velocity = new Vector3(0,0,0);
        bodyRB.angularVelocity = new Vector3(0,0,0);

        //PLAYER HAS PLACED SPAWN BLOCK
        if (MS.spawnblock && spawnPosGood)
        {
            spawnblockWarning.text = "";
        }
        else
        {
            GameObject.Find("Controller").GetComponent<ModeSwitch>().setBool(player, false);
            readystate = false;
        }

        //PLAYER IS NOT READY
        if(readystate == false)
        {
            readyText.text = "";
        }
        else
        {
            readyText.text = "Ready!";//need to turn this off once round starts. maybe have a function that turns all construction UI off?
        }
        
        //PLAYER HAS USED ALL BLOCKS
        if (MS.numberOfBlocks == MS.maxNumberOfBlocks && readystate == false)
        {
            pressToReady.text = "Press Back to Ready";
        }
        else
        {
            pressToReady.text = "";
        }

        //player has used 50% of blocks
        if(MS.numberOfBlocks >= MS.maxNumberOfBlocks/2)
        {
            //want a timer for this to turn off after X
            testBoat.text = "Right Thumbstick to test boat!";
        }
        else if(MS.numberOfBlocks == 0)
        {
            testBoat.text = "Left Thumbstick to spawn a block";
        }
        else
        {
            testBoat.text = "";
        }
        if(spawnPosGood == false)
        {
            spawnblockWarning.text = "something is obstructing the spawn block!";
        }

        //if backbutton is pressed (they are ready) and they have a spawn block placed.
        if (Controller.prevState[player].Buttons.Back == ButtonState.Released && Controller.state[player].Buttons.Back == ButtonState.Pressed)
        {
            if(spawnPosGood == true)
            {
                if (MS.spawnblock)
                {
                    //Debug.Log("trigger battle phase");
                    readystate = !readystate;
                    GameObject.Find("Controller").GetComponent<ModeSwitch>().setBool(player, readystate);
                    //readyText.text = "Ready!";
                }
                else
                {
                    GameObject.Find("Controller").GetComponent<ModeSwitch>().setBool(player, false);
                    readystate = false;
                    //turn on UI telling player to place spawn block
                    spawnblockWarning.text = "You need to place a spawn block before you can ready!";
                }
            }
            
        }

        if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)//if the mode has changed to battle
        {
            GetComponent<PlayerMovement>().enabled = true;
            MS.constructionMode = false;
            TurnOffConstructionUI();
        }
    }

    bool readystate = false;

    void TurnOffConstructionUI()
    {
        spawnblockWarning.text = "";
        readyText.text = "";
        pressToReady.text = "";
        testBoat.text = "";
    }

    void LateUpdate()
    {
        if (transform.position.y < lavaHeight)
        {
            redScreen.SetActive(true);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = true;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = true;
        }
        if (transform.position.y > lavaHeight)
        {
            redScreen.SetActive(false);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = false;
        }

        if (body)
        {
            rotationX += Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
            rotationY -= Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;

            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion rotation = Quaternion.Euler(0, rotationX, 0);
            Quaternion rotationCam = Quaternion.Euler(rotationY, 0, 0);
            Quaternion rotationSep = Quaternion.Euler(rotationY, rotationX, 0);
            Vector3 position = rotationCam * thirdPersonoffset + body.transform.localPosition;

            //body.transform.localEulerAngles = new Vector3(body.transform.localEulerAngles.x, rotation.eulerAngles.y, body.transform.localEulerAngles.z);

            transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
            transform.localPosition = position;
            transform.parent.transform.rotation = rotation;
            //transform.position = position;
        }


    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    GameObject playerManager;

    void Start()
    {
        Cursor.visible = false;
        // Make the rigid body not change rotation

        bodyRB = body.transform.parent.GetComponent<Rigidbody>();

        if (bodyRB)
        {
            bodyRB.freezeRotation = true;
            bodyRB.isKinematic = true;
        }

        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;
        thirdPersonoffset = transform.localPosition - body.transform.localPosition;

        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        movementSpeed = DV.PlayerRelated[6];

        initialXRotate = transform.localEulerAngles.x;
        previousRotation = transform.localEulerAngles.x;
    }
}
