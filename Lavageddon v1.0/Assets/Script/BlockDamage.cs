using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {



    public float HitPoints = 1;
    float hp;
    public bool keystone = false;

    public int cost = 1;

    GameObject playerManager;

    // Use this for initialization
    void Start () {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();
        //Rigidbody rb = GetComponent<Rigidbody>()

        Debug.Log(gameObject.name);
        if (gameObject.name == "BlockFloat(Clone)")
        {
            HitPoints = (int)DV.BlockRelated[1];
            cost = (int)DV.BlockRelated[2];
        }
        if (gameObject.name == "BlockArmour(Clone)")
        {
            HitPoints = (int)DV.BlockRelated[5];
            cost = (int)DV.BlockRelated[6];
        }
        hp = HitPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Damage(float damage)
    {
        hp -= damage;
        if(hp<=0)
        {
            transform.DetachChildren();
            Destroy(gameObject);
        }
        if(hp>HitPoints)
        {
            hp = HitPoints;
        }
    }

    void OnDestroy()
    {
        //destruction
        if (GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<managerscript>().blockdestroyed = true;
        }
    }
}
