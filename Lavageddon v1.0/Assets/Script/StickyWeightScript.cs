﻿using UnityEngine;
using System.Collections;

public class StickyWeightScript : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            FixedJoint temp = gameObject.AddComponent<FixedJoint>();
            temp.connectedBody = other.GetComponent<Rigidbody>();
            temp.enableCollision = false;
            temp.GetComponent<Collider>().enabled = false;
            temp.breakForce = 5000.0f;
            temp.breakTorque = 5000.0f;
        }
    }
}