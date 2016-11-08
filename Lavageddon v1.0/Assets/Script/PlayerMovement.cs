using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public enum Weapon
    {
        BOMB,
        LASER,
        STICKY,
    };

    int numberOfWeapons = 3;
    public Weapon weapon = Weapon.BOMB;
    [System.NonSerialized]
    private int laserDamage = 1;
    [System.NonSerialized]
    private float movementSpeed;
    [System.NonSerialized]
    private float sensitivityX;
    [System.NonSerialized]
    private float sensitivityY;
    public float minimumX;
    public float maximumX;
    public float minimumY;
    public float maximumY;
    public float frictionCast;
    [System.NonSerialized]
    private float jumpForce;
    [System.NonSerialized]
    private float laserForce;
    float rotationY;
    float rotationX;
    public GameObject overheatBar;

    public GameObject jumpParticle;
    public GameObject laserParticle;

    public GameObject laserFireSound;
    GameObject laserFiring;
    public GameObject laserHitSound;
    GameObject laserHitting;
    public GameObject cannonFireSound;
    public GameObject weightGunFireSound;
    public GameObject jumpSound;
    public GameObject jumpLandingSound;
    public GameObject playerDeathAudio;
    public GameObject itemSwitchAudio;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    int lengthOfLineRenderer = 10;

    public float laserMinTime = 0.5f;
    public float laserResidual = 0.3f;
    float laserTimer = 0.0f;//laser cooldown if overheated
    float laserOverheat = 0.0f;
    bool laserOverheated = false;
    public float laserOverheatTime = 1.0f;
    public float coolDownFactor = 1.2f;
    public float overheatedCoolDownFactor = 0.5f;
    public float bombMinTime = 0.5f;
    public int bombsLeft;//number of bombs left
    public int bombClipSize = 6;
    float bombTimer = 0.0f;//time between bombs
    public float stickyMinTime = 0.5f;
    float stickyTimer = 0.0f;//time between stickies
    public int stickiesLeft;//number of stickies left
    public int stickyClipSize = 4;
    public float projectileResidual = 0.2f;
    public int player = 0;

    public float reloadTime = 1.0f;
    float reloadTimer = 0.0f;
    bool reloading = false;

    public bool alive = true;

    public float lavaHeight = 1.5f;

    public float angularFriction = 0.8f;

    public float submergedMinTime = 1.0f;
    float submergedTimer = 0.0f;
    public bool submergeAccumulate = true;

    public float initialFOV = 80.0f;
    public float zoomFOV = 30.0f;

    Vector3 thirdPersonoffset;

    public GameObject redScreen;

    public GameObject body;

    Rigidbody bodyRB;

    //these are the crosshairs
    public GameObject explosionCrosshair;
    public GameObject LaserCrosshair;
    public GameObject WeightCrosshair;

    public GameObject weaponTurner;

    //crosshair animators
    public Animator CannonAnim;
    public Animator LaserAnim;
    public Animator WeightAnim;

    public Slider laserOverheatSlider;
    public GameObject laserSlider;



    bool jumped = false;
    bool inAir = false;

    public GameObject gameOver;

    ////these are the weapon images
    //public GameObject explosion;
    //public GameObject Laser;
    //public GameObject Weight;

    //public float shootDistance = 1000.0f;

    
   //void Start()
   //{
   //    weaponTurner.SetActive(true);
   //    Debug.Log("weapon turner has been set to active");
   //}
    
    void Update()
    {
        if (alive)
        {




            if (Controller.prevState[player].Buttons.A == ButtonState.Released && Controller.state[player].Buttons.A == ButtonState.Pressed)
            {


                RaycastHit hit;

                if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, frictionCast))
                {
                    if (hit.collider.tag == "Block")
                    {
                        Debug.DrawLine(body.transform.position, hit.point);
                        bodyRB.AddForce(0, jumpForce, 0);
                        jumpParticle.GetComponent<ParticleSystem>().Play();
                        Instantiate(jumpSound, transform.position, Quaternion.identity);
                        jumped = true;
                    }
                }
                //else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(1, -1, 0), out hit, frictionCast))
                //{
                //    if (hit.collider.tag == "Block")
                //    {
                //        Debug.DrawLine(body.transform.position, hit.point);
                //        bodyRB.AddForce(0, jumpForce, 0);
                //    }
                //}
                //else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(-1, -1, 0), out hit, frictionCast))
                //{
                //    if (hit.collider.tag == "Block")
                //    {
                //        Debug.DrawLine(body.transform.position, hit.point);
                //        bodyRB.AddForce(0, jumpForce, 0);
                //    }
                //}
                //else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 1), out hit, frictionCast))
                //{
                //    if (hit.collider.tag == "Block")
                //    {
                //        Debug.DrawLine(body.transform.position, hit.point);
                //        bodyRB.AddForce(0, jumpForce, 0);
                //    }
                //}
                //else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, -1), out hit, frictionCast))
                //{
                //    if (hit.collider.tag == "Block")
                //    {
                //        Debug.DrawLine(body.transform.position, hit.point);
                //        bodyRB.AddForce(0, jumpForce, 0);
                //    }
                //}
            }

            if (jumped)
            {
                RaycastHit hit;

                if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, frictionCast))
                {
                }
                else
                {
                    inAir = true;
                    jumped = false;
                }
            }
            if (inAir)
            {
                RaycastHit hit;

                if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, frictionCast))
                {
                    if (hit.collider.tag == "Block")
                    {
                        Debug.DrawLine(body.transform.position, hit.point);
                        Instantiate(jumpLandingSound, transform.position, Quaternion.identity);
                        inAir = false;
                    }
                }
            }


            // CROSSHAIR NOTE use this to reference which weapon is being used and which cross hair should be active!
            if (weapon == Weapon.BOMB)
            {
                laserSlider.SetActive(false);
                GetComponent<GUImanager>().ChangeWeaponUI(0);
                bombTimer += Time.deltaTime;
                //stickyTimer = 0.0f;
                //laserTimer = 0.0f;
                if (bombsLeft <= 0)
                {
                    reloading = true;
                }
                //set the crosshairs
                explosionCrosshair.SetActive(true);
                LaserCrosshair.SetActive(false);
                WeightCrosshair.SetActive(false);

                //change the images
                //explosion.transform.position = new Vector3(0, 120, 0);
            }
            if (weapon == Weapon.STICKY)
            {
                laserSlider.SetActive(false);
                GetComponent<GUImanager>().ChangeWeaponUI(1);
                stickyTimer += Time.deltaTime;
                //laserTimer = 0.0f;
                //bombTimer = 0.0f;
                if (stickiesLeft <= 0)
                {
                    reloading = true;
                }
                explosionCrosshair.SetActive(false);
                LaserCrosshair.SetActive(false);
                WeightCrosshair.SetActive(true);

               // Weight.transform.position = new Vector3(0, 120, 0);
            }
            if (weapon == Weapon.LASER)
            {
                laserSlider.SetActive(true);
                laserOverheatSlider.value = laserTimer;
                GetComponent<GUImanager>().ChangeWeaponUI(2);
                //stickyTimer = 0.0f;
                //bombTimer = 0.0f;
                explosionCrosshair.SetActive(false);
                LaserCrosshair.SetActive(true);
                WeightCrosshair.SetActive(false);
                //overheatBar.GetComponent<Image>().GetComponent<Material>().SetFloat("node_7559", 100 * (laserTimer / laserOverheatTime));
                //Laser.transform.position = new Vector3(0, 120, 0);
            }

            if (stickyTimer >= projectileResidual)
            {
                GamePad.SetVibration((PlayerIndex)player, 0f, 0f);
            }

            if (bombTimer >= projectileResidual)
            {
                GamePad.SetVibration((PlayerIndex)player, 0f, 0f);
            }

            if(laserTimer >= laserOverheatTime)
            {
                laserOverheated = true;
                laserTimer = laserOverheatTime;
            }

            if(laserTimer < 0)
            {
                laserTimer = 0;
            }

            if(laserOverheated)
            {
                laserTimer -= Time.deltaTime*overheatedCoolDownFactor;
                if(laserTimer <= 0)
                {
                    laserOverheated = false;
                }
                GamePad.SetVibration((PlayerIndex)player, 0, 0);
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }

            if(reloading)
            {
                GetComponent<GUImanager>().playerinvincible.text = "Reloading";
                reloadTimer += Time.deltaTime;
                if(reloadTimer >= reloadTime)
                {
                    reloadTimer = 0;
                    if (weapon == Weapon.BOMB)
                    {
                        BombReload();
                    }
                    if (weapon == Weapon.STICKY)
                    {
                        StickyReload();
                    }
                    reloading = false;
                }
            }
            else
            {
                GetComponent<GUImanager>().playerinvincible.text = "";
            }

            if (Controller.state[player].Buttons.X == ButtonState.Pressed)
            {
                reloading = true;
            }

            //if (Controller.prevState[player].Triggers.Right < 0.1 && Controller.state[player].Triggers.Right > 0.1)
            if (Controller.state[player].Triggers.Right > 0.1)
            {

                if (weapon == Weapon.BOMB && bombTimer >= bombMinTime && bombsLeft > 0 && !reloading)
                {
                    bombsLeft--;
                    GamePad.SetVibration((PlayerIndex)player, 0.6f, 0.3f);
                    bombTimer = 0.0f;
                    GetComponent<FiringScript>().Fire(player);
                    Instantiate(cannonFireSound, transform.position, new Quaternion(0, 0, 0, 0));
                }
                if (weapon == Weapon.STICKY && stickyTimer >= stickyMinTime && stickiesLeft > 0 && !reloading)
                {
                    stickiesLeft--;
                    GamePad.SetVibration((PlayerIndex)player, 0.6f, 0.3f);
                    stickyTimer = 0.0f;
                    GetComponent<FireStickyWeight>().Fire(player);
                    Instantiate(weightGunFireSound, transform.position, new Quaternion(0, 0, 0, 0));
                }
                if (weapon == Weapon.LASER && !laserOverheated)
                {
                    if (!laserFiring.activeSelf)
                    {
                        laserFiring.SetActive(true);
                    }
                    laserTimer += Time.deltaTime;
                    GamePad.SetVibration((PlayerIndex)player, 0.0f, 0.3f);
                    //laser.enabled = true;
                    //int i = 0;
                    //while (i < lengthOfLineRenderer)
                    //{
                    //    //Vector3 pos = new Vector3(i * 0.5F, Mathf.Sin(i + t), 0);
                    //    Vector3 pos = transform.position + transform.forward.normalized * i;
                    //    i++;
                    //}
                    //laser.SetPosition(0, transform.position);
                    //laser.SetPosition(1, transform.position + transform.forward.normalized * laserLength);
                    RaycastHit shot;
                    if (Physics.Raycast(transform.position, transform.forward, out shot))
                    {



                        LineRenderer lineRenderer = GetComponent<LineRenderer>();
                        lineRenderer.enabled = true;
                        int i = 0;
                        while (i < lengthOfLineRenderer)
                        {
                            //Vector3 pos = (transform.position + transform.right.normalized*(lengthOfLineRenderer-i+1)/20) + (transform.forward.normalized * i - transform.right.normalized / (lengthOfLineRenderer - i + 1) / 20);
                            //Vector3 pos = transform.position + transform.forward.normalized * i;
                            //Vector3 pos = (transform.position - transform.up.normalized * (lengthOfLineRenderer - i + 1) / 20) + (transform.forward.normalized * i + transform.up.normalized / (lengthOfLineRenderer - i + 1) / 20);
                            //lineRenderer.SetPosition(i, pos);
                            Vector3 pos = (body.transform.position + new Vector3(0, 1, 0)) + i*((shot.point - body.transform.position)/lengthOfLineRenderer);
                            lineRenderer.SetPosition(i, pos);
                            i++;
                        }

                        Debug.DrawLine(transform.position, shot.point, Color.green);
                        if (shot.collider.tag == "Block")
                        {
                            if (!laserHitting.activeSelf)
                            {
                                laserHitting.SetActive(true);
                            }
                            GameObject laserHit = (GameObject)Instantiate(laserParticle, shot.transform.position, new Quaternion(0, 0, 0, 0));
                            laserHit.transform.up = shot.normal;
                            shot.collider.GetComponent<BlockDamage>().Damage(laserDamage);
                            shot.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward.normalized * laserForce, shot.point);
                            LaserAnim.SetTrigger("Orange");
                        }
                        else if(shot.collider.tag == "Body")//shot another player with the laser
                        {
                            //set a bool here to reference from GUI code and write "player is invincible"
                            //this needs to be turned off after timer, maybe trigger it in guimanager
                            GetComponent<GUImanager>().Hitplayer = true;
                        }
                    }
                    else
                    {
                        laserHitting.SetActive(false);
                    }
                }
            }
            else
            {
                laserFiring.SetActive(false);
                laserHitting.SetActive(false);
            }

            if (Controller.state[player].Triggers.Right < 0.1)
            {
                if (!laserOverheated)
                {
                    laserTimer -= Time.deltaTime * coolDownFactor;
                }
                GamePad.SetVibration((PlayerIndex)player, 0, 0);
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }

            if (Controller.state[player].Triggers.Left > 0.1)
            {
                GetComponent<Camera>().fieldOfView = zoomFOV;
            }
            if (Controller.state[player].Triggers.Left < 0.1)
            {
                GetComponent<Camera>().fieldOfView = initialFOV;
            }

            if (Controller.prevState[player].Buttons.RightShoulder == ButtonState.Released && Controller.state[player].Buttons.RightShoulder == ButtonState.Pressed)
            {
                weapon++;
                //rotate clockwise
                weaponTurner.transform.Rotate(0, 0, 120);
                Instantiate(itemSwitchAudio, transform.position, Quaternion.identity);
            }
            if (Controller.prevState[player].Buttons.LeftShoulder == ButtonState.Released && Controller.state[player].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                weapon--;
                //rotate counter clockwise
                weaponTurner.transform.Rotate(0, 0, -120);
                Instantiate(itemSwitchAudio, transform.position, Quaternion.identity);
            }

            if ((int)weapon == numberOfWeapons)
            {
                weapon = (Weapon)0;
            }
            else if ((int)weapon < 0)
            {
                weapon = (Weapon)(numberOfWeapons - 1);
            }


            //if (Controller.prevState[player].Buttons.Start == ButtonState.Released && Controller.state[player].Buttons.Start == ButtonState.Pressed)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
        }

    }

    void BombReload()
    {
        bombsLeft = bombClipSize;
    }
    void StickyReload()
    {
        stickiesLeft = stickyClipSize;
    }

    float x;

    void LateUpdate()
    {
        if (alive)
        {

            if (body.transform.position.y < lavaHeight)
            {
                PlayerDead();

            }
            else if (body.transform.position.y > lavaHeight && submergeAccumulate)
            {
                submergedTimer = 0;
            }


            if (body)
            {
                rotationX += Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
                rotationY -= Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;

                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion rotation = Quaternion.Euler(0, rotationX, 0);
                Quaternion rotationCam = Quaternion.Euler(rotationY, 0, 0);
                Quaternion rotationSep = Quaternion.Euler(rotationY, rotationX, 0);
                Vector3 position = rotationCam * thirdPersonoffset + body.transform.localPosition;

                //body.transform.localEulerAngles = new Vector3(body.transform.localEulerAngles.x, rotation.eulerAngles.y, body.transform.localEulerAngles.z);

                transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.localPosition = position;
                transform.parent.transform.rotation = rotation;
                //transform.position = position;
            }

            RaycastHit hit;

            if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, frictionCast))
            {
                Debug.DrawLine(body.transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    bodyRB.velocity = new Vector3(vel.x, bodyRB.velocity.y, vel.z);
                    //bodyRB.angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity * angularFriction;
                }
                else
                {
                    bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(1, -1, 0), out hit, frictionCast))
            {
                Debug.DrawLine(body.transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    bodyRB.velocity = new Vector3(vel.x, bodyRB.velocity.y, vel.z);
                    //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
                }
                else
                {
                    bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(-1, -1, 0), out hit, frictionCast))
            {
                Debug.DrawLine(body.transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    bodyRB.velocity = new Vector3(vel.x, bodyRB.velocity.y, vel.z);
                    //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
                }
                else
                {
                    bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 1), out hit, frictionCast))
            {
                Debug.DrawLine(body.transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    bodyRB.velocity = new Vector3(vel.x, bodyRB.velocity.y, vel.z);
                    //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
                }
                else
                {
                    bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else if (Physics.Raycast(body.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, -1), out hit, frictionCast))
            {
                Debug.DrawLine(body.transform.position, hit.point);

                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    bodyRB.velocity = new Vector3(vel.x, bodyRB.velocity.y, vel.z);
                    //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
                }
                else
                {
                    bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }

            //if (Physics.Raycast(body.transform.position, new Vector3(0, -1, 0), out hit, frictionCast))
            //{
            //    Debug.DrawLine(body.transform.position, hit.point);

            //    if (hit.collider.tag == "Block")
            //    {
            //        Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
            //        GetComponent<Rigidbody>().velocity = new Vector3(vel.x, GetComponent<Rigidbody>().velocity.y, vel.z);
            //        //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
            //    }
            //    else
            //    {
            //        GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
            //        //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //    }
            //}
            else
            {
                bodyRB.velocity = new Vector3(0, bodyRB.velocity.y, 0);
                bodyRB.angularVelocity = new Vector3(0, 0, 0);
            }


        }
        else
        {
            GamePad.SetVibration((PlayerIndex)player, 0.0f, 0.0f);
            //controller look
            //float rotationX = transform.localEulerAngles.y + Controller.state[player].ThumbSticks.Right.X;

            //rotationY += Controller.state[player].ThumbSticks.Right.Y;
            //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            //float rotationX = Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
            //rotationY = Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;
            //transform.RotateAround(body.transform.position, body.transform.up, rotationX);
            //transform.RotateAround(body.transform.position, body.transform.right, -rotationY);


            if (body)
            {
                rotationX += Controller.state[player].ThumbSticks.Right.X * sensitivityX * Time.deltaTime;
                rotationY -= Controller.state[player].ThumbSticks.Right.Y * sensitivityY * Time.deltaTime;

                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion rotation = Quaternion.Euler(0, rotationX, 0);
                Quaternion rotationCam = Quaternion.Euler(rotationY, 0, 0);
                Vector3 position = rotationCam * thirdPersonoffset + body.transform.localPosition;

                transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
                transform.localPosition = position;
                transform.parent.transform.rotation = rotation;
            }

            //transform.position += Controller.state[player].ThumbSticks.Left.Y * transform.forward.normalized * movementSpeed * Time.deltaTime;

            //transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed * Time.deltaTime;

            //transform.position += Controller.state[player].Triggers.Right * transform.up.normalized * movementSpeed * Time.deltaTime;

            //transform.position -= Controller.state[player].Triggers.Left * transform.up.normalized * movementSpeed * Time.deltaTime;

        }


        if (body.transform.position.y < lavaHeight)
        {
            GamePad.SetVibration((PlayerIndex)player, 0.08f, 0.15f);
        }


        if (transform.position.y < lavaHeight)
        {
            redScreen.SetActive(true);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = true;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = true;
        }
        if (transform.position.y > lavaHeight)
        {
            redScreen.SetActive(false);
            GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>().enabled = false;
            GetComponent<UnityStandardAssets.ImageEffects.GlobalFog>().enabled = false;
        }

    }


    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    //used for setting variables set in menu
    GameObject playerManager;


    void OnEnable()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();
        Rigidbody rb = body.transform.parent.GetComponent<Rigidbody>();

        //PLAYER RELATED VALUES
        jumpForce = (DV.PlayerRelated[0] * 10);
        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        rb.mass = DV.PlayerRelated[3];
        submergedMinTime = DV.PlayerRelated[4];
        if (DV.PlayerRelated[5] == 1)
        {
            submergeAccumulate = true;
        }
        else
        {
            submergeAccumulate = false;
        }
        movementSpeed = DV.PlayerRelated[7];

        //WEAPON RELATED VALUES
        //cannon isnt referenced here
        bombMinTime = DV.WeaponRelated[2];

        laserDamage = (int)DV.WeaponRelated[3];
        laserMinTime = DV.WeaponRelated[4];
        laserForce = DV.WeaponRelated[5];

        stickyMinTime = DV.WeaponRelated[7];
        bombTimer = bombMinTime;
        stickyTimer = stickyMinTime;
        //gameOver.SetActive(false);
    }
    void Start()
    {
        laserOverheatSlider.maxValue = laserOverheatTime;

        laserFiring = (GameObject)Instantiate(laserFireSound, transform.position, new Quaternion(0, 0, 0, 0));
        laserFiring.SetActive(false);

        laserHitting = (GameObject)Instantiate(laserHitSound, transform.position, new Quaternion(0, 0, 0, 0));
        laserHitting.SetActive(false);

        weaponTurner.SetActive(true);

        thirdPersonoffset = GetComponent<CameraMovement>().thirdPersonoffset;
        rotationY = transform.eulerAngles.y;
        rotationX = transform.eulerAngles.x;

        //Cursor.visible = false;
        GetComponent<CameraMovement>().enabled = false;
        //GetComponent<WhirlpoolCurrent>().enabled = true;
        // Make the rigid body not change rotation
        bodyRB = body.transform.parent.GetComponent<Rigidbody>();

        if (bodyRB)
        {
            bodyRB.useGravity = true;
            bodyRB.freezeRotation = true;
            bodyRB.isKinematic = false;
        }

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.3F, 0.3F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        lineRenderer.enabled = false;
        alive = true;

                //setting the menu variables to the player variables
        playerManager = GameObject.FindGameObjectWithTag("Manager");
        DynamicVariables DV = playerManager.GetComponent<DynamicVariables>();
        Rigidbody rb = body.transform.parent.GetComponent<Rigidbody>();

        //PLAYER RELATED VALUES
        jumpForce = (DV.PlayerRelated[0] * 10);
        sensitivityX = DV.PlayerRelated[1];
        sensitivityY = DV.PlayerRelated[2];
        rb.mass = DV.PlayerRelated[3];
        submergedMinTime = DV.PlayerRelated[4];
        if(DV.PlayerRelated[5] == 1)
        {
            submergeAccumulate = true;
        }
        else
        {
            submergeAccumulate = false;
        }
        movementSpeed = DV.PlayerRelated[7];

        //WEAPON RELATED VALUES
        //cannon isnt referenced here
        bombMinTime = DV.WeaponRelated[2];

        laserDamage = (int)DV.WeaponRelated[3];
        laserMinTime = DV.WeaponRelated[4];
        laserForce = DV.WeaponRelated[5];

        stickyMinTime = DV.WeaponRelated[7];

        bombsLeft = bombClipSize;
        stickiesLeft = stickyClipSize;

    }



    void OnDestroy()
    {
        GamePad.SetVibration((PlayerIndex)player, 0f, 0f);
    }

    void PlayerDead()
    {

        submergedTimer += Time.deltaTime;
        if (submergedTimer >= submergedMinTime)
        {
            gameOver.SetActive(true);
            GamePad.SetVibration((PlayerIndex)player, 0f, 0f);
            alive = false;

            explosionCrosshair.SetActive(false);
            LaserCrosshair.SetActive(false);
            WeightCrosshair.SetActive(false);

            weaponTurner.SetActive(false);

            GameObject.Find("PlayerManager").GetComponent<DynamicPlayerCount>().playerDeath();
            Vector3 temp1 = transform.localPosition;
            Vector3 temp2 = GetComponent<PlayerMovement>().body.transform.localPosition;
            GetComponent<PlayerMovement>().body.transform.parent.transform.position = new Vector3(0f, 15f, -29f);// + temp;
            transform.localPosition = temp1;
            GetComponent<PlayerMovement>().body.transform.localPosition = temp2;
            //body.transform.position = new Vector3(0f, 15f, -29f);
            MeshRenderer[] meshes = body.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
            //transform.localEulerAngles = new Vector3(45, 0, 0);
            bodyRB.useGravity = false;
            bodyRB.velocity = new Vector3(0, 0, 0);
            submergedTimer = 0;

            body.GetComponent<CapsuleCollider>().enabled = false;
            Instantiate(playerDeathAudio, transform.position, Quaternion.identity);
        }
    }
}
