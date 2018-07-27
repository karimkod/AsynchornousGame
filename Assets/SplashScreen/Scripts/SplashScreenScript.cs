using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField]
    private Animation splashScreenAnimation; 

	// Use this for initialization
	IEnumerator Start()
    {
        yield return new WaitUntil(() => !splashScreenAnimation.isPlaying);
        GameManagerScript.Instance.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
