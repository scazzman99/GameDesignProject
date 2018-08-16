using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REMEMBER: CTRL + K + D WILL CLEAN UP THE CODE
public class Player : MonoBehaviour
{
    // dont forget the f at end of nums for float e.g. 10f
    public float speed;
    public Rigidbody rb;
    public float jumpHeight = 10f;
    //private bool isGroundedB = true;
    public float rayDistance = 1f; //how long in units the ray will be below the player

    
    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down); //Ray is a special data type from unity API. Takes attached objects position and direction of the ray. down here to hit ground
        Gizmos.color = Color.blue; //change the colour of the groundRay
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance); //draw the line from origin of groundRay to point from origin to distance in a direction

    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    bool isGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(groundRay, out hit, rayDistance)) //out modifier fills the variable with data from outside the function. checks for any raycast hit
        {
            return true;
        }
        return false;
    }
    void Update()
    {
        #region OLD_MOVECODE
        /*
        //check for W key press
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 forward;
            forward.x = 0;
            forward.y = 0;
            forward.z = 1;
            this whole thing can be in one line

           // Vector3 forward = new Vector3(0, 0, 1);
           //Unity already defined a forward direction for vector 3 so we can do this in one line.
            Move Forward
            rb.AddForce(Vector3.forward*speed);
        }

       if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * speed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed);
        }

        // If space bar is pressed
            // JUMP UP!
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isGrounded = false;
        }
    
        //If i used an else-if condition, composite vectors would not be viable as combine vectors would not be possible in the code logic
        //switch statements are also not viable here, as we are not making a choice, we are just looking for several conditions
        //There IS a way to condense this entire thing into roughly 3 lines
        */
        #endregion

        float inputH = Input.GetAxis("Horizontal") * speed; //this number will represent if we moved left or right (between -1 and 1). Multiplied by speed to not alter y velocity later
        float inputV = Input.GetAxis("Vertical") * speed; //this number will represent if we moved forward or back (between -1 and 1). Multiplied by speed to not alter y velocity later
        Vector3 moveDir = new Vector3(inputV, 0f, inputH); //get move direction from H & V getAxis, with no y movement.

        Vector3 force = new Vector3(moveDir.x, rb.velocity.y, moveDir.z); //using this with speed multiply ends up accelerating you at a massive speed if you walk off the edge so dont do this (updates everytime)
       

        if (Input.GetButton("Jump") && isGrounded()) //using get button for universal use.
        {
            force.y = jumpHeight;
        }

        rb.velocity = force; //give rigid body a velocity in given direction and multiply velocity vector by speed (moveDir * speed) or by just using 'force' which is updating.

        if (moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDir); //only look at quiternions in unity context. If moving in direction, rotate to that direction.
        }

    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Ground")
        {
            isGroundedB = true;
        }
    }*/
}
