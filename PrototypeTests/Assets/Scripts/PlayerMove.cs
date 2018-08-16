using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour {

    [Header("Move Vars")]
    [Space(10)]
    [Range(0f, 10f)]
    public float speed = 5f;
    public float jumpSpeed = 8f;
    public float grav = 20f;
    public CharacterController player;
    private Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            moveDirection = this.transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= grav * Time.deltaTime; //delat time to keep movement in check with framerates
        player.Move(moveDirection * Time.deltaTime);
	}

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider myCollider)
    {
        if (myCollider.gameObject.CompareTag("Plat"))
        {
            this.transform.parent = null; //removes player from the platform parent, thus letting them move without influence
        }
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay(Collider myCollider)
    {
        if (myCollider.gameObject.CompareTag("Plat"))
        {
            this.transform.parent = myCollider.transform; //Makes the player a child of the platform, meaning they will move together with player control independent 
        }
    }
     //For this platform collision script to wrok, the platform needed 2 box colliders (CharcterController), one for the platform itself so I dont fall through and the other as a trigger to see if I was touching platform.
     
}
