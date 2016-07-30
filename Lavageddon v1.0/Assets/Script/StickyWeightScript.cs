using UnityEngine;
using System.Collections;

public class StickyWeightScript : MonoBehaviour {
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Block")
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            FixedJoint temp = gameObject.AddComponent<FixedJoint>();
            temp.connectedBody = other.collider.GetComponent<Rigidbody>();
            temp.enableCollision = false;
            temp.GetComponent<Collider>().enabled = false;
            temp.breakForce = 5000.0f;
            temp.breakTorque = 5000.0f;
        }
    }
}
