using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CameraMovement : MonoBehaviour {

    public float movementSpeed;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    float rotationX = 0F;
    public int player = 0;

    public float lavaHeight = 2.0f;
    public GameObject redScreen;

    public GameObject body;

    public float thirdPersonoffset = 2.0f;

    Vector3 thirdPersonPivot;

    //public SavePrefab save;

    private Vector3 initialVector = Vector3.forward;

    float initialXRotate;
    float previousRotation;

    void Update()
    {
        //controller look
        //float rotationX = transform.locallocalEulerAngles.y + Controller.state[player].ThumbSticks.Right.X * (sensitivityX/10);

        //rotationY += Controller.state[player].ThumbSticks.Right.Y * (sensitivityY/10);
        //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //transform.locallocalEulerAngles = new Vector3(-rotationY, rotationX, 0);


        //thirdPersonPivot = transform.position + transform.forward.normalized * thirdPersonoffset;

        rotationX = Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
        rotationY = Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;
        transform.RotateAround(body.transform.position, body.transform.up, rotationX);
        //transform.RotateAround(body.transform.position, body.transform.right, -rotationY);

        //transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, initialXRotate + maximumY, initialXRotate + 360 + minimumY), transform.localEulerAngles.y, transform.localEulerAngles.z);

        if (transform.localEulerAngles.x >= maximumY && transform.localEulerAngles.x <= 360 + minimumY)
        {
            //transform.localEulerAngles -= new Vector3(previousRotation - transform.localEulerAngles.x, 0, 0);
            //transform.RotateAround(body.transform.position, body.transform.right, (previousRotation - transform.localEulerAngles.x));
            transform.RotateAround(body.transform.position, body.transform.right, -rotationY);
            //transform.RotateAround(body.transform.position, new Vector3(1, 0, 0), 100 * (previousRotate - transform.localEulerAngles.x));
        }
        else
        {
            transform.RotateAround(body.transform.position, body.transform.right, rotationY);
            previousRotation = body.transform.localEulerAngles.x;
        }
        
        //transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward.normalized * movementSpeed;

        //transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

        //transform.position += Controller.state[player].Triggers.Right * transform.up.normalized * movementSpeed;

        //transform.position -= Controller.state[player].Triggers.Left * transform.up.normalized * movementSpeed;

        if (Controller.state[player].DPad.Up == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
        }

        if (Controller.state[player].DPad.Down == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
        }


        //transform.position += Controller.state[player].ThumbSticks.Left.Y * new Vector3(transform.forward.normalized.x + transform.up.normalized.x, 0, transform.forward.normalized.z + transform.up.normalized.z) * movementSpeed * Time.deltaTime;
        transform.position += Controller.state[player].ThumbSticks.Left.Y * body.transform.forward * movementSpeed * Time.deltaTime;
        transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed * Time.deltaTime;

        if (Controller.state[player].Buttons.A == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
        }

        if (Controller.state[player].Buttons.X == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
        }


        //this needs to change to check if all players are ready. so this will just change a bool and when all players bools are true it will change state.
        //this will also check to see if all players have placed their spawn blocks.
        if (Controller.prevState[player].Buttons.Back == ButtonState.Released && Controller.state[player].Buttons.Back == ButtonState.Pressed)
        {
            GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Controller").GetComponent<ModeSwitch>().construction = false;
            //save.CreatePrefab();
            //save.WriteBoat();
            GetComponent<managerscript>().constructionMode = false;

            //if(GetComponent<managerscript>().spawnblock)
            //{
            //    GameObject.Find("Controller").GetComponent<ModeSwitch>().setBool(player, true);
            //}
        }
        if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)
        {
            GetComponent<PlayerMovement>().enabled = true;
            //save.CreatePrefab();
            //save.createblocks();
            GetComponent<managerscript>().constructionMode = false;
        }
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
    }

    GameObject playerManager;

    void Start()
    {
        Cursor.visible = false;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }


        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        movementSpeed = DV.PlayerRelated[6];

        initialXRotate = transform.localEulerAngles.x;
        previousRotation = transform.localEulerAngles.x;
    }

}
