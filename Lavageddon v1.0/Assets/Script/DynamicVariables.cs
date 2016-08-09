using UnityEngine;
using System.Collections;

public class DynamicVariables : MonoBehaviour {

    Object[] duplicates = new Object[2];
    static DynamicVariables Original;

    public int MAXRESOURCES = 100;
    public int FLOATBLOCKHP = 3;

    [System.Serializable]
    public struct BlockRelated
    {
        int MaxResources;

        int FloatBlockHP;
        int ArmourBlockHP;

        float FloatBloatFLOATATION;
        float ArmourBlockFLOATATION;

        float FloatBlockMASS;
        float ArmourBlockMASS;
    }

    [System.Serializable]
    public struct PlayerRelated
    {
        float JumpForce;
        float Gravity;
        float Sensitivity;
        float DeathTimer;
    }

    public struct WeaponRelated
    {
        float CannonBulletSpeed;
        float CannonDmg;
        float CannonCD;

        float BeamDmg;
        float BeamCD;
        float BeamForce;

        float StickyBulletSpeed;
        float StickyCD;
        float StickyWeight;
    }

   

    public void Increment(int area, float increase)
    {
        if(delay <= 0)
        {
            switch (area)
            {
                case 0:
                    MAXRESOURCES += (int)increase;
                    break;
                case 1:
                    FLOATBLOCKHP += (int)increase;
                    break;
            }
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
