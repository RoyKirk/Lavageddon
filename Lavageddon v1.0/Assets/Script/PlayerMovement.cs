using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour {

    public enum Weapon
    {
        BOMB,
        LASER,
        STICKY,
    };
    int numberOfWeapons = 3;
    public Weapon weapon = Weapon.BOMB;
    public int laserDamage = 1;
    public float movementSpeed;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float frictionCast = 1.0f;
    public float jumpForce = 1000.0f;
    public float laserForce = 1000.0f;
    float rotationY = 0F;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    int lengthOfLineRenderer = 10;

    public float laserMinTime = 0.5f;
    public float laserResidual = 0.02f;
    float laserTimer = 0.0f;
    public float bombMinTime = 0.5f;
    float bombTimer = 0.0f;
    public float stickyMinTime = 0.5f;
    float stickyTimer = 0.0f;

    public int player = 0;

    public bool alive = true;

    public float lavaHeight = 2.0f;

    //public float shootDistance = 1000.0f;
    void Update()
    {
        if (alive)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, frictionCast))
            {

                Debug.DrawLine(transform.position, hit.point);
                if (Controller.prevState[player].Buttons.A == ButtonState.Released && Controller.state[player].Buttons.A == ButtonState.Pressed)
                {
                    GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                }
                //if (hit.collider.tag == "Block")
                //{

                //}
            }

            //controller look
            float rotationX = transform.localEulerAngles.y + Controller.state[player].ThumbSticks.Right.X;

            rotationY += Controller.state[player].ThumbSticks.Right.Y;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

            transform.position += Controller.state[player].ThumbSticks.Left.Y * new Vector3(transform.forward.normalized.x + transform.up.normalized.x, 0, transform.forward.normalized.z + transform.up.normalized.z) * movementSpeed;

            transform.position += Controller.state[player].ThumbSticks.Left.X * transform.right.normalized * movementSpeed;

            laserTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;
            stickyTimer += Time.deltaTime;

            if (laserTimer >= laserResidual)
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.enabled = false;
            }

            if (laserTimer < laserMinTime)
            {
                LineRenderer lineRenderer = GetComponent<LineRenderer>();
                int i = 0;
                while (i < lengthOfLineRenderer)
                {
                    //Vector3 pos = (transform.position + transform.right.normalized*(lengthOfLineRenderer-i+1)/20) + (transform.forward.normalized * i - transform.right.normalized / (lengthOfLineRenderer - i + 1) / 20);
                    //Vector3 pos = transform.position + transform.forward.normalized * i;
                    Vector3 pos = (transform.position - transform.up.normalized * (lengthOfLineRenderer - i + 1) / 20) + (transform.forward.normalized * i + transform.up.normalized / (lengthOfLineRenderer - i + 1) / 20);
                    lineRenderer.SetPosition(i, pos);
                    i++;
                }
            }

            //if (Controller.prevState[player].Triggers.Right < 0.1 && Controller.state[player].Triggers.Right > 0.1)
            if (Controller.state[player].Triggers.Right > 0.1)
            {

                if (weapon == Weapon.BOMB && bombTimer >= bombMinTime)
                {
                    bombTimer = 0.0f;
                    GetComponent<FiringScript>().Fire();
                }
                if (weapon == Weapon.STICKY && stickyTimer >= stickyMinTime)
                {
                    stickyTimer = 0.0f;
                    GetComponent<FireStickyWeight>().Fire();
                }
                if (weapon == Weapon.LASER && laserTimer >= laserMinTime)
                {
                    laserTimer = 0.0f;
                    LineRenderer lineRenderer = GetComponent<LineRenderer>();
                    lineRenderer.enabled = true;
                    int i = 0;
                    while (i < lengthOfLineRenderer)
                    {
                        //Vector3 pos = (transform.position + transform.right.normalized*(lengthOfLineRenderer-i+1)/20) + (transform.forward.normalized * i - transform.right.normalized / (lengthOfLineRenderer - i + 1) / 20);
                        //Vector3 pos = transform.position + transform.forward.normalized * i;
                        Vector3 pos = (transform.position - transform.up.normalized * (lengthOfLineRenderer - i + 1) / 20) + (transform.forward.normalized * i + transform.up.normalized / (lengthOfLineRenderer - i + 1) / 20);
                        lineRenderer.SetPosition(i, pos);
                        i++;
                    }
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
                        Debug.DrawLine(transform.position, shot.point, Color.red);
                        if (shot.collider.tag == "Block")
                        {

                            shot.collider.GetComponent<BlockDamage>().Damage(laserDamage);
                            shot.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward.normalized * laserForce, shot.point);

                        }
                    }
                }
            }


            if (Controller.prevState[player].Buttons.RightShoulder == ButtonState.Released && Controller.state[player].Buttons.RightShoulder == ButtonState.Pressed)
            {
                weapon++;
            }
            if (Controller.prevState[player].Buttons.LeftShoulder == ButtonState.Released && Controller.state[player].Buttons.LeftShoulder == ButtonState.Pressed)
            {
                weapon--;
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

    void LateUpdate()
    {
        if (alive)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, frictionCast))
            {
                if (hit.collider.tag == "Block")
                {
                    Vector3 vel = hit.collider.GetComponent<Rigidbody>().velocity;
                    GetComponent<Rigidbody>().velocity = new Vector3(vel.x, GetComponent<Rigidbody>().velocity.y, vel.z);
                    //GetComponent<Rigidbody>().angularVelocity = hit.collider.GetComponent<Rigidbody>().angularVelocity;
                }
                else
                {
                    //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                    //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                }
            }
            else
            {
                //GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            }

            if (transform.position.y < lavaHeight)
            {
                alive = false;
            }
        }

    }

    void Start()
    {
        //Cursor.visible = false;
        GetComponent<CameraMovement>().enabled = false;
        //GetComponent<WhirlpoolCurrent>().enabled = true;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        lineRenderer.enabled = false;
    }
}
