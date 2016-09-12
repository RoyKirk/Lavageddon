using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingBlock : MonoBehaviour {


    public float blockCast;

    public int playerOwner;//this needs to be set when created to determine which player controls this block. this will also be used to determine if the player can destroy it or not

    public Vector3 startPos;
    public Quaternion startRotation;

    bool changeState;

    // Use this for initialization
    void Start ()
    {
        MakeJoints();        
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<WhirlpoolCurrent>().enabled = false;
        GetComponent<FloatFixed>().enabled = false;
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<WhirlpoolCurrent>().enabled = true;
            GetComponent<FloatFixed>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BlockDamage>().keystone = false;
            Debug.Log("not in construction mode");
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<WhirlpoolCurrent>().enabled = false;
            GetComponent<FloatFixed>().enabled = false;
        }
        //this can be optimsed later to only be called once every change, atm its being called every frame which is not needed.
        if (GameObject.Find("Player" + playerOwner + "(Clone)").GetComponent<managerscript>().testingboat == true)
        {
            TestBoat(playerOwner);
            changeState = true;
        }
        else if (changeState == true)
        {
            //reset boat
            
            transform.position = startPos;
            transform.rotation = startRotation;
            //GetComponent<Rigidbody>().MovePosition(startPos);
            //GetComponent<Rigidbody>().MoveRotation(startRotation);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            changeState = false;
            MakeJoints();
        }
    }
    // Update is called once per frame

    //set the block to change state (this is required to be called for the boat testing funtion)
    public void TestBoat(int owner)
    {
        if (playerOwner == owner)
        {
            GetComponent<BuildingBlock>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<WhirlpoolCurrent>().enabled = true;
            GetComponent<FloatFixed>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BlockDamage>().keystone = false;
        }
    }

    //this needs to be set when created to determine which player controls this block. this will also be used to determine if the player can destroy it or not

    public void MakeJoints()
    {
        FixedJoint[] joints = GetComponents<FixedJoint>();
        foreach (FixedJoint joint in joints)
        {
             Destroy(joint);
        }


        GetComponent<BoxCollider>().enabled = false;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        if (Physics.Raycast(transform.position, transform.up * -1.0f, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        if (Physics.Raycast(transform.position, transform.forward * -1.0f, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        if (Physics.Raycast(transform.position, transform.right * -1.0f, out hit, blockCast))
        {
            //Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.tag == "Block")
            {
                FixedJoint temp = gameObject.AddComponent<FixedJoint>();
                temp.connectedBody = hit.collider.GetComponent<Rigidbody>();
                temp.enableCollision = false;
                temp.enablePreprocessing = false;
                temp.GetComponent<Collider>().enabled = false;
                temp.breakForce = 1000.0f;
                temp.breakTorque = 1000.0f;
            }
        }

        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<WhirlpoolCurrent>().enabled = false;
        GetComponent<FloatFixed>().enabled = false;
    }
}
