using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( CharacterController ) ) ]
[ RequireComponent( typeof( SphereCollider ) ) ]
public class driver : MonoBehaviour {

	Animator anim;
	private CharacterController controller;

	public float speed = 30.0f;   // movement speed
	public float rotationSpeed = 2.0f;  //turning speed

    private float yaw = 0;
    private float pitch = 0;

    public float yawMin = -20f;
    public float yawMax = 45f;

	public float maxHealth = 250.0f;

    public int damage = 1;
	private static float currHealth;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		currHealth = maxHealth;
        GameObject managerObj = GameObject.FindWithTag( "GameManager" );
		if( managerObj != null )
		{
			GameManager manager = managerObj.GetComponent< GameManager >();
	        minX = manager.LevelMinX;
	        maxX = manager.LevelMaxX;
	        minZ = manager.LevelMinZ;
	        maxZ = manager.LevelMaxZ;
		}
    }

    void Update()
	{
		float dt = Time.deltaTime;
		float dx =  0;
		float dy =  0;
		float dz =  0;

		////////////////////// MOVEMENT /////////////////////

		dx = Input.GetAxis( "Horizontal" ) * dt * speed;
		dz = Input.GetAxis( "Vertical" ) * dt * speed;

		////////////////////////////////////////////////////


		//////////////////// MOUSE ORBIT ///////////////////

		pitch += Input.GetAxis( "Mouse X" ) * rotationSpeed;
        yaw += Input.GetAxis( "Mouse Y" ) * rotationSpeed * -1;
        yaw = Mathf.Clamp( yaw, yawMin, yawMax );
        transform.localRotation = Quaternion.Euler( yaw, pitch, 0 );

		////////////////////////////////////////////////////


		////////////////////// FLYING //////////////////////

		bool isGrounded = controller.isGrounded;
		anim.SetBool( "isGrounded", isGrounded );

		// ascend
        if( Input.GetKey( KeyCode.Space ) )
        {
            dy = speed * dt;
        }
        // decend
        if( Input.GetKey( KeyCode.LeftShift ) )
        {
            dy = -speed * dt;
        }

		////////////////////////////////////////////////////


		////////////////////// ATTACK //////////////////////

		if( Input.GetMouseButton(0) )
		{
			anim.SetBool ("FlameThrower", true);
		}
		else
		{
			anim.SetBool ("FlameThrower", false);
		}

		////////////////////////////////////////////////////


		Vector3 moveDir = transform.TransformDirection( new Vector3( dx, 0, dz ) );
		moveDir.y = dy;
		controller.Move( moveDir );
        if (transform.position.x > maxX)
        {
            transform.SetPositionAndRotation(new Vector3(maxX, transform.position.y, transform.position.z), transform.rotation);
            //Debug.Log("Max X Reached");
        }
        else if (transform.position.x < minX)
        {
            transform.SetPositionAndRotation(new Vector3(minX, transform.position.y, transform.position.z), transform.rotation);
            //Debug.Log("Min X Reached");
        }
        if (transform.position.z > maxZ)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, maxZ), transform.rotation);
            //Debug.Log("Max Z Reached");
        }
        else if (transform.position.z < minZ)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, minZ), transform.rotation);
            //Debug.Log("Min Z Reached");
        }
    }

	// damage system
	void OnCollisionEnter( Collision collision )
    {
    	if( collision.gameObject.tag == "Projectile" )
		{
			currHealth = currHealth - 20 > 0 ? currHealth - 20 : 0;

			if ( currHealth <= 0 )
			{
				FindObjectOfType< GameManager >().EndGame();
			}
		}
    }

    public float getMaxHealth()
	{
		return maxHealth;
	}

	public float getCurrHealth()
	{
		return currHealth;
    }

    public float getHealthPercentage()
    {
        return (float)currHealth/(float)maxHealth;
    }

    public void setHealthPercentage(float percentage)
    {
        currHealth = Mathf.Clamp((percentage * maxHealth), 1, maxHealth);
    }

    public void heal(float healamount, bool allowPastMax = false)
    {
        currHealth += healamount;
        if(!allowPastMax && currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }
}
