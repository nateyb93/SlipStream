using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

    public float currentSpeed;
    
    //number of missiles and the recharge counter
    int missileCharger = 0;
    public int curMissileCount = 7;
    //time required to recharge 1 missile
    int missileChargeTime = 40;
    //missiles cannot fire unless curMissileCooldown = 0
    int curMissileCooldown = 0;
    public int missileCooldownTime = 10;
    public int missileCountMax = 7;

    public int health;
    public GUIText healthText;

    //speed constants
    //the default move speed for the ship
    public float MOVESPEED = 5f;
    public float MOVINGTURN = .5f;
    public float STATIONARYTURN = 3f;
    public int MAXHEALTH = 10;

    //our missiles
    public GameObject missilePrefab;

    //detects the first wall collision to ensure that next launch is not towards a wall
    private bool firstWallCollision;

    static GameObject missile;
    
	// Use this for initialization
	void Start () 
    {
        currentSpeed = 0;
        health = MAXHEALTH;
        healthText.text = "Health: " + health;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        //applying forward movement
        rigidbody.AddRelativeForce(0f, 0f, currentSpeed);

        //health management *************TODO*************This is the gameover condition and will need extra functionality added to it
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        #region handling missiles and ammunition
        //add a missile every few cycles, max out at ten missiles
        if (curMissileCount<missileCountMax)
        {
            if(missileCharger<missileChargeTime)
            {
                missileCharger++;
            }
            else //once the missile charger reaches 10 we add a missile and reset the counter
            {
                missileCharger=0;
                curMissileCount++;
            }
        }


        #endregion

        #region +++++++++++++++++++++++++++++++++++++ controls +++++++++++++++++++++++++++++++++++++
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (currentSpeed != 0)
            {
                transform.Rotate(0.0f, MOVINGTURN, 0.0f);
            }
            else
            {
                transform.Rotate(0.0f, 3f, 0.0f);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentSpeed != 0)
            {
                transform.Rotate(0.0f, -1 * MOVINGTURN, 0.0f);
            }
            else
            {
                transform.Rotate(0.0f, -1 * STATIONARYTURN, 0.0f);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed = MOVESPEED;
        }

        //shooting
        if (curMissileCooldown == 0 && curMissileCount > 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                missile = Instantiate(missilePrefab, transform.position, transform.rotation) as GameObject;
                missile.transform.Rotate(0, 180, 0);
                curMissileCooldown = missileCooldownTime;
                curMissileCount--;
            }
            if (Input.GetKey(KeyCode.D))
            {
                missile = Instantiate(missilePrefab, transform.position, transform.rotation) as GameObject;
                missile.transform.Rotate(0, 0, 0);
                curMissileCooldown = missileCooldownTime;
                curMissileCount--;
            }
        }
        if (curMissileCooldown > 0 )
        {
            curMissileCooldown--;
        }
        //+++++++++++++NOTE ABOUT CAMERA CONTROLS: Camera controls are located in the camera script 
        #endregion
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Walls")
        {
             currentSpeed = 0;
        }
        if (other.gameObject.tag == "Missile")
        {
            health--;
            healthText.text = "Health: " + health;
        }
    }

    //sometimes the trigger fails to trigger if the ship is already against the wall
    //and it moves towards the wall, causing it to lose control. This is to fix that issue
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Walls")
        {
            currentSpeed = 0;
        }
    }
}
