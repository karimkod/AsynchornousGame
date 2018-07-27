using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuScript: MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
  
    public void LaodLevel(int i)
    {
        GameManagerScript.Instance.LoadLevel(i);
    }

    public void MuteRestore()
    {
        BackGroundMusicManager.Instance.MuteRestore();
    }

    public void ToggleGameObject(GameObject goToToggle)
    {
        if (goToToggle == null)
            return;

        goToToggle.SetActive(!goToToggle.activeSelf);
    }

    public void FacebookPage()
    {
        Application.OpenURL("https://www.facebook.com/PptStd/");
    }


    public void ShowLeaderBoard()
    {
        GooglePlayServices.Instance.ShowLeaderBoard();
    }


}
