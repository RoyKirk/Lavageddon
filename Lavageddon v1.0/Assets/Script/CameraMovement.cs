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

    public Vector3 thirdPersonoffset;
    

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

        //rotationX = Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
        //rotationY = Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;
        //transform.RotateAround(body.transform.position, body.transform.up, rotationX);
        ////transform.RotateAround(body.transform.position, body.transform.right, -rotationY);

        ////transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, initialXRotate + maximumY, initialXRotate + 360 + minimumY), transform.localEulerAngles.y, transform.localEulerAngles.z);

        //if (transform.localEulerAngles.x >= maximumY && transform.localEulerAngles.x <= 360 + minimumY)
        //{
        //    //transform.localEulerAngles -= new Vector3(previousRotation - transform.localEulerAngles.x, 0, 0);
        //    transform.RotateAround(body.transform.position, body.transform.right, (previousRotation - transform.localEulerAngles.x));
        //    //transform.RotateAround(body.transform.position, body.transform.right, -rotationY);
        //    //transform.RotateAround(body.transform.position, new Vector3(1, 0, 0), 100 * (previousRotate - transform.localEulerAngles.x));
        //}
        //else
        //{
        //    transform.RotateAround(body.transform.position, body.transform.right, rotationY);
        //    previousRotation = body.transform.localEulerAngles.x;
        //}

        //transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward.normalized * movementSpeed;

        //transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

        //transform.position += Controller.state[player].Triggers.Right * transform.up.normalized * movementSpeed;

        //transform.position -= Controller.state[player].Triggers.Left * transform.up.normalized * movementSpeed;


        //if backbutton is pressed (they are ready) and they have a spawn block placed.
        if (Controller.prevState[player].Buttons.Back == ButtonState.Released && Controller.state[player].Buttons.Back == ButtonState.Pressed)
        {
            if(GetComponent<managerscript>().spawnblock)
            {
                Debug.Log("trigger battle phase");
                GameObject.Find("Controller").GetComponent<ModeSwitch>().setBool(player);
            }
        }
        if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)//if the mode has changed to battle
        {
            GetComponent<PlayerMovement>().enabled = true;
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

        if (body)
        {
            rotationX += Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
            rotationY -= Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;

            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            Vector3 position = rotation * thirdPersonoffset + body.transform.position;

            body.transform.localEulerAngles = new Vector3(body.transform.localEulerAngles.x, rotation.eulerAngles.y, body.transform.localEulerAngles.z);

            transform.rotation = rotation;
            transform.position = position;
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
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        
        rotationY = 0;
        rotationX = 0;


        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        movementSpeed = DV.PlayerRelated[6];

        initialXRotate = transform.localEulerAngles.x;
        previousRotation = transform.localEulerAngles.x;
    }
}
