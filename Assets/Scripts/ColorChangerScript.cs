using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerScript : MonoBehaviour
{
    [HideInInspector]
    public Color levelColor;
    private float[] colorRGBValues = { 0, 1, 0 };

    public float step = 0.01f;
    
    private int currentIndex;


    // Use this for initialization
    void Awake ()
    {

        levelColor = new Color(colorRGBValues[1],
            colorRGBValues[2], colorRGBValues[0], 1);

        currentIndex = 0;
        StartCoroutine(ChangeColor());

    }

    private IEnumerator ChangeColor()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            GetNextColor();
            levelColor.r = colorRGBValues[1];
            levelColor.g = colorRGBValues[2];
            levelColor.b = colorRGBValues[0];
            
        }



    }

    private void GetNextColor()
    {
        colorRGBValues[currentIndex] += step;
        if ((colorRGBValues[currentIndex] >= 1f && step > 0) || (colorRGBValues[currentIndex] <= 0f && step < 0))
        {
            currentIndex = (currentIndex + 1) % colorRGBValues.Length;
            step = step * -1;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
