﻿using UnityEngine;
using System.Collections;

public class DynamicVariables : MonoBehaviour {

    Object[] duplicates = new Object[2];
    static DynamicVariables Original;

    public float[] BlockRelated;

    // BLOCK RELATED
    int MaxResources;

    int FloatBlockHP;
    int ArmourBlockHP;

    float FloatBloatFLOATATION;
    float ArmourBlockFLOATATION;

    float FloatBlockMASS;
    float ArmourBlockMASS;

    //PLAYER RELATED
    float JumpForce;
    float Gravity;
    float Sensitivity;
    float DeathTimer;

    // WEAPON RELATED
    float CannonBulletSpeed;
    float CannonDmg;
    float CannonCD;

    float BeamDmg;
    float BeamCD;
    float BeamForce;

    float StickyBulletSpeed;
    float StickyCD;
    float StickyWeight;

    void Update()
    {
        //for(int i = 0; i < 20; i++)
        //{
        //    ALLVALUES[i] = ;
        //}
    }

    public void Increment(int area, float increase)
    {
        if(delay <= 0)
        {
            //switch (area)
            //{
            //    case 0:
            //        MaxResources += (int)increase;
            //        break;
            //    case 1:
            //        FloatBlockHP += (int)increase;
            //        break;
            //}
            BlockRelated[area] += increase;
            if(increase == 1 || increase == -1)
            {
                delay = 0.2f;
            }
            
        }
        if(delay > -.1f)
        {
            delay -= Time.deltaTime;
        }
    }

    public float delay = 0f;
    void Awake()
    {
        if (Original)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Original = this;
        }
    }


    void OnLevelWasLoaded(int level)
    {
        duplicates = FindObjectsOfType(GetType());
        if (duplicates.Length > 1)
        {
            Destroy(duplicates[0]);
        }
        if (level > 0)
        {
           //get all the scripts or make all the scripts access this script when they get instantiated

            
        }
    }
}
