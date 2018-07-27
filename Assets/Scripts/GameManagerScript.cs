using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    private static GameManagerScript instance; 
    public static GameManagerScript Instance { get { return instance; } }


    [SerializeField]
    private Animation transitionAnimation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this; 
        else
        {
            if (instance != this)
                Destroy(gameObject);
                
        }
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
    private IEnumerator LoadLevelCoroutine(int i)
    {
        transitionAnimation.clip = transitionAnimation.GetClip("FadeOutAnimation");
        transitionAnimation.Play();
        yield return new WaitUntil(()=>!transitionAnimation.isPlaying)  ;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(i);
        asyncOp.allowSceneActivation = true;
        yield return new WaitUntil(() => asyncOp.isDone);
        //asyncOp.allowSceneActivation = true;
        //yield return new WaitUntil(() => asyncOp.isDone); 
        transitionAnimation.clip = transitionAnimation.GetClip("FadeOnAnimation");
        transitionAnimation.Play();
        yield return null; 
    }

    public void LoadLevel(int i)
    {
        StartCoroutine(LoadLevelCoroutine(i)); 
    }
}
