using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnScript : MonoBehaviour
{
    public float timeToWait; 
	// Use this for initialization
	IEnumerator Start ()
    {
        yield return new WaitForSeconds(timeToWait);
        GameManagerScript.Instance.LoadLevel(4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
