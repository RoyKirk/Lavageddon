﻿using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {



    public float HitPoints = 1;
    public bool keystone = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Damage(float damage)
    {
        HitPoints -= damage;
        if(HitPoints<=0)
        {
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        //destruction
        if (GameObject.Find("Main Camera"))
        {
            GameObject.Find("Main Camera").GetComponent<managerscript>().blockdestroyed = true;
        }
    }
}
