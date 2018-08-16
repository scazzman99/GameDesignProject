using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrappleRay : MonoBehaviour {

    [Header("Reference")]
    public GameObject player;
    public GameObject mainCam;
    public Camera mainC;
    public Material white;
    private bool isGrappling = false;
    private bool isTrigger = false;
    private GameObject pointBuffer;
    
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        mainCam = GameObject.FindGameObjectWithTag("Mcam");
        mainC = GameObject.FindGameObjectWithTag("Mcam").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isGrappling && pointBuffer != null) //check if pointBuffer is null just in case
        {
            GrapplePlayer(pointBuffer, player);
            Debug.Log("isGrapple is true");
            return;
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {
            Ray grappleLine;
            grappleLine = mainC.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hitInfo;

            if (Physics.Raycast(grappleLine, out hitInfo, 20f))
            {
                if (hitInfo.collider.CompareTag("Button"))
                {
                    Debug.Log("You hit a button");
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Debug.Log("You hit button");
                        hitInfo.collider.gameObject.GetComponent<Renderer>().material = white; //this gets the render component of the object ray collides with and changes its material to one imported from inspector
                        isGrappling = true;
                        pointBuffer = hitInfo.collider.gameObject;
                        GrapplePlayer(hitInfo.collider.gameObject, player); //calls function GrapplePlayer with gameObject of the button and player
                
                    }
                }
            }
        }
	}

    void GrapplePlayer(GameObject button, GameObject player)
    {
        Transform buttonPosition = button.transform; //get position of button our mouse is hitting
        Transform playerPosition = player.transform; //get position of player right now
        
        

        playerPosition.position = Vector3.MoveTowards(playerPosition.position, buttonPosition.position, 30f * Time.deltaTime); //move the player toward the button by deltaTime
        
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            isGrappling = false;
            pointBuffer = null; //wipes the pointBuffer varaible for the next grapple
            Debug.Log("isGrapple is false");
            return;
        }

        if (isTrigger)
        {
            isGrappling = false;
            pointBuffer = null; //wipes the pointBuffer varaible for the next grapple
            Debug.Log("isGrapple is false");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Button"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Button"))
        {
            isTrigger = false;
        }
    }




}
