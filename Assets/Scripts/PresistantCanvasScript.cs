using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PresistantCanvasScript : MonoBehaviour
{
    Canvas presistantCanvas; 

	// Use this for initialization
	void Start ()
    {
        if (presistantCanvas == null)
            presistantCanvas = GetComponent<Canvas>();

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        AttachCamera();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AttachCamera()
    {
        presistantCanvas.worldCamera = Camera.main;
    }
}
