using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public void LoadScene(int i)
    {
        GameManagerScript.Instance.LoadLevel(i); 
    }


}
