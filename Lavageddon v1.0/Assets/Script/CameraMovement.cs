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

    public int player = 0;

    public float lavaHeight = 1.0f;
    public GameObject redScreen;

    public GameObject body;

    public float thirdPersonoffset = 2.0f;

    Vector3 thirdPersonPivot;

    void Update()
    {
        //controller look
        //float rotationX = transform.localEulerAngles.y + Controller.state[player].ThumbSticks.Right.X * (sensitivityX/10);

        //rotationY += Controller.state[player].ThumbSticks.Right.Y * (sensitivityY/10);
        //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


        //thirdPersonPivot = transform.position + transform.forward.normalized * thirdPersonoffset;

        float rotationX = Controller.state[player].ThumbSticks.Right.X * (sensitivityX / 10);
        rotationY = Controller.state[player].ThumbSticks.Right.Y * (sensitivityY / 10);

        transform.RotateAround(body.transform.position, body.transform.up, rotationX);
        transform.RotateAround(body.transform.position, body.transform.right, -rotationY);

        //transform.RotateAround(thirdPersonPivot, transform.up, rotationX);
        //transform.RotateAround(thirdPersonPivot, transform.right, -rotationY);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        //transform.RotateAround(body.transform.position, transform.up, rotationX);
        //transform.RotateAround(body.transform.position, transform.right, -rotationY);

        transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward.normalized * movementSpeed;

        transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

        transform.position += Controller.state[player].Triggers.Right * transform.up.normalized * movementSpeed;

        transform.position -= Controller.state[player].Triggers.Left * transform.up.normalized * movementSpeed;

        if (Controller.state[player].DPad.Up == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, 1, 0) * movementSpeed;
        }

        if (Controller.state[player].DPad.Down == ButtonState.Pressed)
        {
            transform.position += new Vector3(0, -1, 0) * movementSpeed;
        }



        if (Controller.prevState[player].Buttons.Back == ButtonState.Released && Controller.state[player].Buttons.Back == ButtonState.Pressed)
        {
            GetComponent<PlayerMovement>().enabled = true;
            GameObject.Find("Controller").GetComponent<ModeSwitch>().construction = false;
            GetComponent<managerscript>().constructionMode = false;
        }
        if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)
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
        }
        if (transform.position.y > lavaHeight)
        {
            redScreen.SetActive(false);
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
        movementSpeed = DV.PlayerRelated[6] / 100;

    }

}
