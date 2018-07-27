using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public float xPositionToCover; 
	// Use this for initialization
	void Start ()
    {
        
        float cameraRatio = (float)Camera.main.pixelWidth / (float)Camera.main.pixelHeight;
       
        Camera.main.orthographicSize = xPositionToCover / cameraRatio;

    }

}
