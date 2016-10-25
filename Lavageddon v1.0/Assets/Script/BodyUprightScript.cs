using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class BodyUprightScript : MonoBehaviour {

    public float movementSpeed = 10.0f;

    DynamicVariables DV;
    GameObject playerManager;
    CameraMovement cameraScript;
    PlayerMovement playerScript;


    public int player = 0;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DV = playerManager.GetComponent<DynamicVariables>();
        cameraScript = transform.parent.GetComponentInChildren<CameraMovement>();
        playerScript = transform.parent.GetComponentInChildren<PlayerMovement>();
    }

    void Update()
    {

        if (cameraScript.enabled)
        {
            movementSpeed = DV.PlayerRelated[6];
        }
        else if (playerScript.enabled)
        {
            movementSpeed = DV.PlayerRelated[7];
        }
        //transform.position += Controller.state[player].ThumbSticks.Left.Y * new Vector3(transform.forward.normalized.x + transform.up.normalized.x, 0, transform.forward.normalized.z + transform.up.normalized.z) * movementSpeed * Time.deltaTime;
        transform.parent.transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward * movementSpeed * Time.deltaTime;
        transform.parent.transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed * Time.deltaTime;

        if (cameraScript.enabled || !playerScript.alive)
        {
            if (Controller.state[player].DPad.Up == ButtonState.Pressed)
            {
                transform.parent.transform.position += new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
            }

            if (Controller.state[player].DPad.Down == ButtonState.Pressed)
            {
                transform.parent.transform.position += new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
            }

            if (Controller.state[player].Buttons.A == ButtonState.Pressed)
            {
                transform.parent.transform.position += new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
            }

            if (Controller.state[player].Buttons.X == ButtonState.Pressed)
            {
                transform.parent.transform.position += new Vector3(0, -1, 0) * movementSpeed * Time.deltaTime;
            }
        }

    }
    // Update is called once per frame
    void LateUpdate () {
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
