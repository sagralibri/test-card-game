using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TreasureHandler : MonoBehaviour
{
    private manager manager;
    public Treasure testTreasure;
    int partitionLinesTreasure = manager.treasureMax;
    List<GameObject> removing = new List<GameObject>();
    public static List<GameObject> partitionsTreasure = new List<GameObject>();
    public GameObject empty;
    public GameObject treasurePrefab;
    int normalizex = 1280;
    int normalizey = 720;

// ---------------------------------------------------------
    void Start()
    {
        Debug.Log("treasure added");
        if (manager.NewTreasure == null)
            manager.NewTreasure = new UnityEvent();
        manager.NewTreasure.AddListener(RefreshCards);

        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.NewTreasure.Invoke();
    }


    void Update()
    {
        
    }


    public void RefreshCards()
    {
        CreateParitionObjectsTreasures();
        SetToIndexTreasure();
        Debug.Log("Treasure scripts ran");
    }


    public void SetToIndexTreasure()
    {
        int i = 0;
        foreach (GameObject objeect in manager.drawnTreasureObjects)
        {
            removing.Add(objeect);
        }
            Debug.Log("foreach 1 successful");
        for (int index = 0; index < removing.Count; index++)
        {
            manager.drawnTreasureObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
            Debug.Log("for 1 successful");
        foreach (Treasure treasure in manager.treasures)
        {
            GameObject currentpartition = partitionsTreasure[i];
            Vector2 pos = new Vector3(-normalizex, -normalizey, 0) + currentpartition.transform.position;
            GameObject newTreasure = Instantiate(treasurePrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            newTreasure.GetComponent<TreasureScript>().usedTreasure = treasure;
            manager.drawnTreasureObjects.Add(newTreasure);
            i++;
        }
        Debug.Log("SetToIndexTreasure functional");
    }



        public void CreateParitionObjectsTreasures()
    {
        int modBoundRight = 1156; // change
        int modBoundLeft = 431; // change
        foreach (GameObject partition in partitionsTreasure)
        {
            removing.Add(partition);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            partitionsTreasure.Remove(removing[index]);
            Destroy(removing[index]);
        }
        for (int i = 1; i <= partitionLinesTreasure; i++)
        {
            Debug.Log(((float)i-(float)1) / ((float)partitionLinesTreasure-(float)1));
            Debug.Log(partitionLinesTreasure);

            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesTreasure-(float)1)));
            GameObject clone = Instantiate(empty, new Vector2(posx+normalizex, -580+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsTreasure.Add(clone);
        }
        Debug.Log("CreateParitionObjectsTreasure functional");
    }


}
