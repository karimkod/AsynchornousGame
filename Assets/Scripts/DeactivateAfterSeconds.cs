using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterSeconds : MonoBehaviour
{

	// Use this for initialization
	void OnEnable ()
    {
        Invoke("Deactivate", 1f);
	}


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
