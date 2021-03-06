﻿using UnityEngine;
using System.Collections;

public class FloatFixed : MonoBehaviour
{
    public bool FLOAT;

    public float waterLevel = 4;
    public float floatHeight = 2;
    public float bounceDamp = .05f;
    public float FloatScale = 1;

    Vector3 localScale;

    const int offsetsize = 8;
    Vector3[] buoyancyOffset = new Vector3[offsetsize];

    float[] forceFactor = new float[offsetsize];
    Vector3[] actionPoint = new Vector3[offsetsize];
    Vector3[] upLift = new Vector3[offsetsize];

    Rigidbody rb;

    void Start()
    {
        GetComponent<BuildingBlock>().enabled = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        localScale = this.transform.localScale / 2;
        //TOP FLOAT POINTS
        buoyancyOffset[0] = new Vector3(-localScale.x, localScale.y, -localScale.z);//back left
        buoyancyOffset[1] = new Vector3(localScale.x, localScale.y, -localScale.z);//back right
        buoyancyOffset[2] = new Vector3(-localScale.x, localScale.y, localScale.z);//front left
        buoyancyOffset[3] = new Vector3(localScale.x, localScale.y, localScale.z);//front right
        //BOTTOM FLOAT POINTS
        buoyancyOffset[4] = new Vector3(-localScale.x, -localScale.y, -localScale.z);//back left;
        buoyancyOffset[5] = new Vector3(localScale.x, -localScale.y, -localScale.z);//back right;
        buoyancyOffset[6] = new Vector3(-localScale.x, -localScale.y, localScale.z);//front left;
        buoyancyOffset[7] = new Vector3(localScale.x, -localScale.y, localScale.z);//front right;
        
        //components
    }
    
    void Update()
    {
        if (GameObject.Find("Controller") && GetComponent<BuildingBlock>().enabled == false)
        {
            if (GameObject.Find("Controller").GetComponent<ModeSwitch>().construction && GameObject.Find("Player" + GetComponent<BuildingBlock>().playerOwner + "(Clone)").GetComponentInChildren<managerscript>().testingboat == false || !GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)
            {
                GetComponent<BuildingBlock>().enabled = true;
            }
        }

        for (int i = 0; i < offsetsize; i++)
        {
            actionPoint[i] = transform.position + transform.TransformDirection(buoyancyOffset[i]);
            forceFactor[i] = 1f - ((actionPoint[i].y - waterLevel) / floatHeight);

            if(forceFactor[i] > 0f)
            {
                
                
                   upLift[i] = -Physics.gravity * (forceFactor[i] - rb.velocity.y * bounceDamp);
                   rb.AddForceAtPosition(upLift[i] * FloatScale, actionPoint[i]);
                
            }
        }
    }
}
