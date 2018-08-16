using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterControl : MonoBehaviour {

    [Header("Variables")]
    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public CharacterController player;
    private Vector3 moveDir = Vector3.zero;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); //make the moveDir vector the inputAxis
            moveDir = transform.TransformDirection(moveDir); //makes the movement direction in the world for the attached object
            moveDir *= speed; //adjust the move direction value to match the speed

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y -= gravity * Time.deltaTime; //adds gravity regardless
        player.Move(moveDir * Time.deltaTime); //actually moves the character controller. Needs timeDeltatime to move in terms of frames (gets really fast if not)
	}

   /*private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            this.transform.parent = null; //removes player from the platform parent, thus letting them move without influence
        }
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            this.transform.parent = collision.transform; //Makes the player a child of the platform, meaning they will move together with player control independent 
        }
    }
    */

}
