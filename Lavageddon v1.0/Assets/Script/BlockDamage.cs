using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {



    public float HitPoints = 1;
    float hp;
    public bool keystone = false;


	// Use this for initialization
	void Start () {
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
