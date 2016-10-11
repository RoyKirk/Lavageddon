using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class BodyUprightScript : MonoBehaviour {

    public float movementSpeed = 10.0f;

    public int player = 0;

    void Update()
    {

        //transform.position += Controller.state[player].ThumbSticks.Left.Y * new Vector3(transform.forward.normalized.x + transform.up.normalized.x, 0, transform.forward.normalized.z + transform.up.normalized.z) * movementSpeed * Time.deltaTime;
        transform.parent.transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward * movementSpeed * Time.deltaTime;
        transform.parent.transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed * Time.deltaTime;

        if (transform.parent.GetComponentInChildren<CameraMovement>().enabled || !transform.parent.GetComponentInChildren<PlayerMovement>().alive)
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
