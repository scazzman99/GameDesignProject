using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigid : MonoBehaviour {

    public Rigidbody rigid;
    public float speed = 5f;
    public float jumpSpeed = 10f;
    public float rayDist = 1f;
	// Use this for initialization
	void Start () {
        rigid = GameObject.Find("PlayerRigid").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float moveH = Input.GetAxis("Horizontal") * speed;
        float moveV = Input.GetAxis("Vertical") * speed;
        Vector3 moveDir = new Vector3(-1f * moveV, 0f, moveH); //Adding minus 1 cause it is backwards
        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z); //force can add in jumping if it happens, move x and z already altered by speed.

        if(Input.GetButton("Jump") && isGrounded())
        {
            force.y = jumpSpeed;
        }

        rigid.velocity = force; //this actually gives the player their speed

        if(moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDir); //rotate the player to look in the direction they are travelling always
        }
	}

    bool isGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayDist))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, ray.origin + ray.direction * rayDist);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            transform.parent = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            transform.parent = collision.transform;
        }
    }


}
