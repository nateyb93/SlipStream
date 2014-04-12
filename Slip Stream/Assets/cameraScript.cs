using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

    //decides whether or not the camera will try to revert to default
    bool buttonHeld=false;

    //relative angle of the camera in relation to the ship
    int angle = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        //gradually rotate the camera each cycle
        if (Input.GetKey(KeyCode.Q))
        {
            buttonHeld = true;
            if (angle > -90)
            {
                transform.Rotate(0, -5, 0);
                //update the relative angle
                angle -= 5;
                
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            buttonHeld = true;
            if (angle < 90)
            {
                transform.Rotate(0, 5, 0);
                //update the relative angle
                angle += 5;
            }
        }
        //revert the camera if no button is being held
        if (!buttonHeld)
        {
            if (angle < 0)
            {
                transform.Rotate(0, 5, 0);
                //update the relative angle
                angle += 5;
            }
            if (angle > 0)
            {
                transform.Rotate(0, -5, 0);
                //update the relative angle
                angle -= 5;
            }
        }


        buttonHeld = false;
	}
}
