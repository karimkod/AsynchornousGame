using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private ColorChangerScript colorChanger;
	// Use this for initialization
	void Start ()
    {
        if (colorChanger == null)
            colorChanger = GameObject.FindObjectOfType<ColorChangerScript>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = colorChanger.levelColor;

    }

    // Update is called once per frame
    void Update ()
    {
        _spriteRenderer.color = colorChanger.levelColor;
	}
}
