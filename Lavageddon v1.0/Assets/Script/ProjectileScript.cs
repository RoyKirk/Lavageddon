using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{
    public float dmg = 1;
    public GameObject explosion;
    public GameObject bombEffect;

    GameObject playerManager;

    public int playerOwner;

    GameObject PLAYER;

	// Use this for initialization
	void Start ()
    {
        PLAYER = GameObject.Find("Player" + playerOwner + "(Clone)");
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();

        dmg = DV.WeaponRelated[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(PLAYER == null)
        {
            PLAYER = GameObject.Find("Player" + playerOwner + "(Clone)");
        }
	    if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Block")
        {
            PLAYER.GetComponentInChildren<PlayerMovement>().CannonAnim.SetTrigger("Orange");
            //get the owner of this projectile and tell them they hit something.
            c.GetComponent<BlockDamage>().Damage(dmg);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(c.tag == "Wall")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
