using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {

    public GameObject BlockDestructionSound;
    public GameObject BlockDeleteSound;

    public float HitPoints = 1;
    float hp;
    public bool keystone = false;

    public int cost = 1;

    GameObject playerManager;

    // Use this for initialization
    void Start () {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();
        Rigidbody rb = GetComponent<Rigidbody>();
        FloatFixed ff = GetComponent<FloatFixed>();

        //Debug.Log(gameObject.name);
        if (gameObject.name == "BlockFloat(Clone)")
        {
            //Debug.Log("float block var set");
            HitPoints = (int)DV.BlockRelated[1];
            cost = (int)DV.BlockRelated[2];
            ff.FloatScale = (DV.BlockRelated[3] / 200);
            rb.mass = (DV.BlockRelated[4] / 100);
        }
        if (gameObject.name == "BlockArmour(Clone)")
        {
            HitPoints = (int)DV.BlockRelated[5];
            cost = (int)DV.BlockRelated[6];
            ff.FloatScale = (DV.BlockRelated[7] / 200);
            rb.mass = (DV.BlockRelated[8] / 100);
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
            if (!GameObject.Find("Controller").GetComponent<ModeSwitch>().construction)
            {
                Instantiate(BlockDestructionSound, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(BlockDeleteSound, transform.position, Quaternion.identity);
            }
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
