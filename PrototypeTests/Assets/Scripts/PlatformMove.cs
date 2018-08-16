using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformMove : MonoBehaviour {
    public Transform waypointParent;
    public Transform[] waypoints;
    int currentIndex = 1;
    public float platSpeed = 3f;
    public Vector3 pointA, pointB;
    bool platAtA = true;

    
    

	// Use this for initialization
	void Start () {
        GenWaypoints();

        #region pingPonginit
        pointA = waypoints[1].position;
        pointB = waypoints[2].position;
        #endregion

    }



    // Update is called once per frame
    void Update()
    {
        #region WaypointMethod

        Transform point = waypoints[currentIndex];
        float distance = Vector3.Distance(transform.position, point.position);

        if(distance < 0.05f)
        {
            currentIndex++;
            if(currentIndex >= waypoints.Length)
            {
                currentIndex = 1;
            }
        }
        this.transform.position = Vector3.MoveTowards(transform.position, point.position, 3f * Time.deltaTime);
        
        #endregion

        #region mathPingPongMethod
        /*
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * 1f, 1f)); //interpoling between A & B with fraction given by pingpong
        */
        #endregion

        #region SinMethod
        /*
        transform.position = Vector3.Lerp(pointA, pointB, (Mathf.Sin(platSpeed*Time.time) + 1f) / 2); //using Sin function with current speed and time will return a value that is within interpoling range [0-1]
        //No adjustment on final statement would result in the platform stopping when sin(x) enters negative range. so adding 1f gets range [0-2] then /2 gives range [0-1]
        */
        #endregion

        #region BoolMethod
        /*
        float distanceCurrent;
        if (platAtA)
        {
            float step = platSpeed * Time.deltaTime; //MoveTowards requires a float MaxDistanceDelta which is how much closer the object will get to its destination, which is controlled by speed and relative frames
            transform.position = Vector3.MoveTowards(transform.position, pointB, step);
            distanceCurrent = Vector3.Distance(transform.position, pointB);
        } else
        {
            float step = platSpeed * Time.deltaTime; //MoveTowards requires a float MaxDistanceDelta which is how much closer the object will get to its destination, which is controlled by speed and relative frames
            transform.position = Vector3.MoveTowards(transform.position, pointA, step);
            distanceCurrent = Vector3.Distance(transform.position, pointA);
        }

        if(distanceCurrent < 0.5f)
        {
            if (platAtA)
            {
                platAtA = false;
            }
            else
            {
                platAtA = true;
            }
        }
        */

        #endregion


    }


    void GenWaypoints()
    {
        

        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }
}
