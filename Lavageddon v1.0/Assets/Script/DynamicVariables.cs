using UnityEngine;
using System.Collections;

public class DynamicVariables : MonoBehaviour {

    Object[] duplicates = new Object[2];
    static DynamicVariables Original;

    public float[] BlockRelated;
    public float[] PlayerRelated;
    public float[] WeaponRelated;

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

    public void Increment(int relation, int variable, float increase)
    {
        if(delay <= 0)
        {
            switch (relation)
            {
                case 0:
                    BlockRelated[variable] += increase;
                    break;
                case 1:
                    PlayerRelated[variable] += increase;
                    if (variable == 5)
                    {
                        if(PlayerRelated[variable] > 1)
                        {
                            PlayerRelated[variable] = 0;
                        }
                        else if(PlayerRelated[variable] < 0)
                        {
                            PlayerRelated[variable] = 1;
                        }
                    }
                    break;
                case 2:
                    WeaponRelated[variable] += increase;
                    break;
            }
            //BlockRelated[variable] += increase;
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
            Destroy(duplicates[1]);
        }
        if (level > 0)
        {
           //get all the scripts or make all the scripts access this script when they get instantiated

            
        }
    }
}
