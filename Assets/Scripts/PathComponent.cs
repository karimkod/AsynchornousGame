using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathComponent : MonoBehaviour
{
    public float offsetToWaitBeforeRecover;
    public float offsetToWaitBeforeRestartRegeneration;
    private Transform currentTransform;
    private Coroutine recoverCoroutine;

    

 	// Use this for initialization
	void OnEnable ()
    {
        if (currentTransform == null)
        {
            currentTransform = GetComponent<Transform>();
           
        }
        recoverCoroutine = StartCoroutine(Recover());
	}
	
    private IEnumerator Recover()
    {
        
        yield return new WaitUntil(() => (currentTransform.position.y - 
        LevelManagerScript.Instance.leadPlayer.position.y) < offsetToWaitBeforeRecover);
        gameObject.SetActive(false); 
    }

    private void Update()
    {

    }


    public IEnumerator DestroyAfter()
    {
        if (recoverCoroutine != null)
            StopCoroutine(recoverCoroutine);

        yield return new WaitUntil(() => (currentTransform.position.y -
        LevelManagerScript.Instance.leadPlayer.position.y) < offsetToWaitBeforeRecover);
        Destroy(gameObject);
    }

    public IEnumerator RestartPathGeneration()
    {
        yield return new WaitUntil(() => currentTransform.position.y -
        LevelManagerScript.Instance.leadPlayer.position.y < offsetToWaitBeforeRestartRegeneration);
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
    }

    private void StopCoroutines()
    {

    }


}
