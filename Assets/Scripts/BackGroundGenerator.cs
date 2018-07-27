using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGenerator : MonoBehaviour
{
    public GameObject backgroundComponent; // Containing Prefab of the background
    public int poolInstancesOfBackgrounds; // number of initial backgrounds on the pool

    public static List<GameObject> backgroundInstances; // Pool of backgrounds


    public int numberOfBackgroundPerCycle; // Number of backgrounds to set valid form the pool every cycles.

    public float offsetBetweenComponents;  // distance to ofsset between  two successive backgrounds and obstacles. 



    private Transform lastBackgroundComponent; // last background transform to know where to instanciat the next background



    void Start ()
    {

        backgroundInstances = new List<GameObject>();

        lastBackgroundComponent = backgroundComponent.transform;

        InitBackgroundsPool();
        StartCoroutine(BackgroundCreator());


    }

    // Update is called once per frame
    void Update () {
		
	}

    private void InitBackgroundsPool()
    {
        GameObject go = null;
        for (int i = 0; i < poolInstancesOfBackgrounds; i++)
        {
            go = Instantiate(backgroundComponent);
            backgroundInstances.Add(go);
            go.SetActive(false);

        }
        return;
    }


    private IEnumerator BackgroundCreator()
    {
        GameObject go = null;
        GameObject lastGo = null;
        while (true)
        {
            for (int i = 0; i < numberOfBackgroundPerCycle; i++)
            {
                go = backgroundInstances.Find(x => !x.activeSelf);
                if (go == null)
                {
                    //Debug.LogError("Instanciation !");
                    go = lastGo;

                }
                else
                {
                    go.transform.position = lastBackgroundComponent.position +
                                 new Vector3(0, offsetBetweenComponents, 0);
                    go.SetActive(true);

                    lastGo = go;
                    lastBackgroundComponent = go.transform;
                }

            }
            yield return StartCoroutine(go.GetComponent<PathComponent>().RestartPathGeneration());


        }
    }
}
