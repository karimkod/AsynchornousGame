using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectTo : MonoBehaviour
{
    public Transform target;
    private Transform currentTransform;

    private void Start()
    {
        currentTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate ()
    {
		if (transform.position.y != target.position.y)
        {
            transform.position = new Vector2(currentTransform.position.x,target.position.y); 
        }
	}
}
