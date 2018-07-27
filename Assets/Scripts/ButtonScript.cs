using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour,IPointerDownHandler
{
    public GameObject ClickEffect;
    private Animation buttonAnimation;
    private Camera mainCamera;
    private AudioSource clickSoundEffect; 
	// Use this for initialization
	void Start ()
    {
       // ClickEffect.transform.SetParent(Camera.main.transform);
        buttonAnimation = ClickEffect.GetComponent<Animation>();
        mainCamera = Camera.main;
        clickSoundEffect = GetComponent<AudioSource>(); 
	}
	

    public void OnPointerDown(PointerEventData eventData)
    {

        ClickEffect.transform.position = mainCamera.ScreenToWorldPoint(eventData.position) + new Vector3(0, 0, 10);
        buttonAnimation.Stop();
        buttonAnimation.Play();
        clickSoundEffect.Play();
       /* Instantiate(ClickEffectPrefabs, Camera.main.ScreenToWorldPoint(eventData.position) +
        new Vector3(0, 0, 10), Quaternion.identity);*/
    }
}
