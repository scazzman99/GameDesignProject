using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollide : MonoBehaviour {

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay(Collider other)
    {
        other.transform.parent = this.gameObject.transform;
    }

    







}
