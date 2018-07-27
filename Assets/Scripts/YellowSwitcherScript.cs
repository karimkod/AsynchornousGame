using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSwitcherScript : MonoBehaviour
{
 
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            other.GetComponent<CubeMechanism>().Switch();

        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
