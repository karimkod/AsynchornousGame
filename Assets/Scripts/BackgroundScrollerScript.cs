using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollerScript : MonoBehaviour
{
    public float scrollingSpeed;
    public float Ydirection;
    private Transform currentTransform;
    private Vector3 offsetVector; 


	// Use this for initialization
	void Start ()
    {
        currentTransform = GetComponent<Transform>();
        offsetVector = new Vector3(0, Ydirection, 0);

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = Vector3.MoveTowards(currentTransform.position, 
            currentTransform.position + offsetVector , Time.deltaTime * scrollingSpeed);
	}
}
