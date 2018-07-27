using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pathComponents; // Containing Prefabs of differents type of obstacles 
    public int poolInstancesOfComponents; // number of initial components on the pool


    public static Dictionary<string, List<GameObject>> componentsInstances; //Dictionnary of Pools of obsacles

    public int minIndex = 1; // the minimum index to start generating randomly obstacles from
    public int numberOfComponentsPerCycle; // Number of obstacles to set valid from the pool every cycles.
    public float offsetBetweenComponents;  // distance to ofsset between  two successive backgrounds and obstacles. 
    public float startYPosition; // distance to from where to start instanciation of obstacles.

    public int numberOfSimpleRoad = 3; 

    private Coroutine pathCoroutine; // PathCreator coroutine -used to stop it later-
	// Use this for initialization
	void Start ()
    {
        
      
        componentsInstances = null;

        
        LevelManagerScript.Instance.ChangeContextEvent += SwitchContest;
        SwitchContest();

       
    }
    

    private void SwitchContest()
    {
        if (pathCoroutine != null)
        {
            StopCoroutine(pathCoroutine);
            pathCoroutine = null;
        }


        if (componentsInstances != null)
        {
            foreach (KeyValuePair<string, List<GameObject>> pair in componentsInstances)
            {
                foreach (GameObject go in pair.Value)
                {
                    if (go.activeSelf)
                        StartCoroutine(go.GetComponent<PathComponent>().DestroyAfter());
                    else
                        Destroy(go);
                }
            }
        }

        pathComponents = LevelManagerScript.Instance.GetPathComponents();
        componentsInstances = new Dictionary<string, List<GameObject>>();
        foreach (GameObject go in pathComponents)
        {
            componentsInstances.Add(go.tag, new List<GameObject>());
        }

        InitComponents();

    }


    private void InitComponents()
    {
        InitObsatclesPool();
        pathCoroutine = StartCoroutine(PathCreator());
    }

    private void InitObsatclesPool()
    {
        GameObject go;
        List<GameObject> outList;
        int numberOfInstances = numberOfSimpleRoad; 
        for (int i = 0; i < pathComponents.Length; i++)
        {
            
            if (i != 0)
            {
                

                numberOfInstances = poolInstancesOfComponents;
            }else
                if (LevelManagerScript.Instance.Difficulty != 0)
                    continue;
            


            for (int j = 0; j < numberOfInstances; j++)
            {
                go = Instantiate(pathComponents[i]);
                go.SetActive(false);
                componentsInstances.TryGetValue(go.tag, out outList);
                outList.Add(go);

            }
        }


    }

    private IEnumerator PathCreator()
    {
        GameObject go = null;
        GameObject lastGO = null; 
        System.Random randomer = new System.Random();
        int randomIndex = 0;
        List<GameObject> outList; 
        if (LevelManagerScript.Instance.Difficulty == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                componentsInstances.TryGetValue(pathComponents[0].tag, out outList);
                go = outList.Find((x) => !x.activeSelf);
                go.transform.position = new Vector3(0, startYPosition, 0);
                go.SetActive(true);
                startYPosition += offsetBetweenComponents;
            }
            yield return StartCoroutine(go.GetComponent<PathComponent>().RestartPathGeneration());
        }
        
        while (true)
        {
           for (int i = 0; i < numberOfComponentsPerCycle; i++)
            {
                randomIndex = randomer.Next(minIndex,pathComponents.Length);
                componentsInstances.TryGetValue(pathComponents[randomIndex].tag, out outList);
                go = outList.Find((x) => !x.activeSelf);

                if (go == null)
                {
                    //Debug.LogError("Instanciation !");
                    go = lastGO; 
                   
                }
                else
                {
                    go.transform.position = new Vector3(0, startYPosition, 0);
                    go.SetActive(true);
                    lastGO = go; 
                    startYPosition += offsetBetweenComponents;


                }
               
                
            }
            yield return StartCoroutine(go.GetComponent<PathComponent>().RestartPathGeneration());
        }
    }


    public void ReplaceWithEmpty(GameObject goToRemplace)
    {

        GameObject go = null;
        List<GameObject> outList;
        goToRemplace.SetActive(false);
        componentsInstances.TryGetValue(pathComponents[0].tag, out outList);
        go = outList.Find((x) => !x.activeSelf);
        if (go == null)
        {
            go = Instantiate(pathComponents[0]) as GameObject;
            go.transform.position = goToRemplace.transform.position;
            go.SetActive(true);
            StartCoroutine(go.GetComponent<PathComponent>().DestroyAfter());

        }
        else
        {
            go.transform.position = goToRemplace.transform.position;
            go.SetActive(true);

        }

       

        return;

    }

    
}
