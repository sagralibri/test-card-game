using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShopDeckHandler : MonoBehaviour
{
    private manager manager;
    //partition lines
    int partitionLinesTreasure = manager.treasureMax;
    int partitionLinesConsumable = manager.consumableMax;
    //test input
    public GameObject empty;
    public Treasure testTreasure;
    public Technique testConsumable;
    // prefabs
    public GameObject treasurePrefab;
    public GameObject consumablePrefab;

    List<GameObject> removing = new List<GameObject>();
    public static List<GameObject> partitionsDeck = new List<GameObject>();
    public static List<GameObject> partitionsTreasure = new List<GameObject>();
    public static List<GameObject> partitionsConsumable = new List<GameObject>();
    public Entity player;
    // oh boy
    public Technique knives, twinKnives, cutOff;
    public Technique punch, staggeringPunch, lightspeedFist, flurryOfBlows;
    public Technique quickSlash, flurrySlash;
    public Technique fireBolt;
    public Technique iceBeam;
    public Technique windShear;
    public Technique lightningBolt;
    public Technique cleansingLight;
    public Technique siphon, blackHole;
    public Technique trueStrike, megidola, megidolaon, decimate, worldEndingStrike, daqAttack;
    // oh boy pt2
    public Treasure machineKey;
    List<Technique> AllTechniques = new List<Technique>();
    List<Treasure> AllTreasures = new List<Treasure>();

    

    int normalizex = 1280;
    int normalizey = 720;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.treasures.Add(testTreasure);
        manager.consumables.Add(testConsumable);
        manager.consumables.Add(testConsumable);
        RefreshCards();
        if (manager.NewTreasure == null)
        {
            manager.NewTreasure = new UnityEvent();
        }
        if (manager.NewConsumable == null)
        {
            manager.NewConsumable = new UnityEvent();
        }
        manager.NewTreasure.AddListener(RefreshCards);

        Debug.Log(manager.treasures.Count);
        manager.NewTreasure.Invoke();
        manager.NewConsumable.Invoke(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RefreshCards()
    {
        CreatePartitionObjects();
        SetToIndexDeck();
    }


    public void SetToIndexDeck()
    {
        int i = 0;
        foreach (GameObject objeect in manager.drawnTreasureObjects)
        {
            removing.Add(objeect);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            manager.drawnTreasureObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
        foreach (GameObject objeect in manager.drawnConsumableObjects)
        {
            removing.Add(objeect);
        } 
        for (int index = 0; index < removing.Count; index++)
        {
            manager.drawnConsumableObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
        i = 0;
        foreach (Treasure treasure in manager.treasures)
        {
            GameObject currentpartition = partitionsTreasure[i];
            Vector2 pos = new Vector3(-normalizex, -normalizey, 0) + currentpartition.transform.position;
            GameObject newTreasure = Instantiate(treasurePrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            newTreasure.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            newTreasure.GetComponent<TreasureScript>().usedTreasure = treasure;
            manager.drawnTreasureObjects.Add(newTreasure);
            i++;
        }
        i = 0;
        foreach (Technique consumable in manager.consumables)
        {
            GameObject currentpartition = partitionsConsumable[i];
            Vector2 pos = new Vector3(-normalizex, -normalizey, 0) + currentpartition.transform.position;
            GameObject newConsumable = Instantiate(consumablePrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            newConsumable.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            newConsumable.GetComponent<ConsumableScript>().usedConsumable = consumable;
            manager.drawnConsumableObjects.Add(newConsumable);
            i++;
        }
    }



    public void CreatePartitionObjects()
    {
        int modBoundRight = -210;
        int modBoundLeft = -900;
        partitionLinesTreasure = manager.treasureMax;
        partitionLinesConsumable = manager.consumableMax;
        foreach (GameObject partition in partitionsTreasure)
        {
            removing.Add(partition);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            partitionsTreasure.Remove(removing[index]);
            Destroy(removing[index]);
        }
        foreach (GameObject partition in partitionsConsumable)
        {
            removing.Add(partition);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            partitionsConsumable.Remove(removing[index]);
            Destroy(removing[index]);
        }
        modBoundRight = 318;
        modBoundLeft = -720;
        for (int i = 1; i <= partitionLinesTreasure; i++)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesTreasure-(float)1)));
            Debug.Log(empty + " " + posx);
            GameObject clone = Instantiate(empty, new Vector2(posx+normalizex, -695+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsTreasure.Add(clone);
        }
        modBoundRight = -280;
        modBoundLeft = -5;
        for (int i = 1; i <= partitionLinesConsumable; i++)
        {
            float posy = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesConsumable-(float)1)));
            GameObject clone = Instantiate(empty, new Vector2(780+normalizex, posy+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsConsumable.Add(clone);
        }
    }

    public void GetLocalFinds()
    {
        int treasureFinds = UnityEngine.Random.Range(1, 4);
        int techniqueFinds = 3 - treasureFinds;
        List<Treasure> foundTreasures = new List<Treasure>();
        List<Technique> foundTechniques = new List<Technique>();

        for (int i = 0; i < techniqueFinds; i++)
        {
            foundTechniques.Add(GetTechniqueFinds());
        }
        for (int i = 0; i < treasureFinds; i++)
        {
            foundTreasures.Add(GetTreasureFinds());
        }
    }


    void AddAll()
    {
        // this is horrible
        AllTreasures.AddRange(new List<Treasure>() {machineKey});
        AllTechniques.AddRange(new List<Technique>() {knives, twinKnives, cutOff, punch, staggeringPunch, lightspeedFist, flurryOfBlows, quickSlash, flurrySlash, fireBolt, iceBeam, windShear, lightningBolt, cleansingLight, siphon, blackHole, trueStrike, megidola, megidolaon, decimate, worldEndingStrike, daqAttack});
    }


    public Technique GetTechniqueFinds()
    {
        int difficulty = (int)Mathf.Clamp((float)manager.difficultyValue / 5, 0 , 5);
        List<Technique> validTechniques = new List<Technique>();
        switch (difficulty)
        {
            case 0:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if (technique.rarity == Rarity.COMMON && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
            case 1:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if ((technique.rarity == Rarity.COMMON) || (technique.rarity == Rarity.RARE) && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
            case 2:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if (((technique.rarity == Rarity.COMMON || technique.rarity == Rarity.RARE) || technique.rarity == Rarity.EPIC) && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
            case 3:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if ((technique.rarity == Rarity.RARE || technique.rarity == Rarity.EPIC) && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
            case 4:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if ((technique.rarity == Rarity.RARE || technique.rarity == Rarity.EPIC || technique.rarity == Rarity.LEGENDARY) && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
            case 5:
            {
                foreach (Technique technique in AllTechniques)
                {
                    if ((technique.rarity == Rarity.EPIC || technique.rarity == Rarity.LEGENDARY || technique.rarity == Rarity.MYTHICAL) && technique.shoppable == true)
                    {
                        validTechniques.Add(technique);
                    }
                }
                break;
            }
        }
        int randomTechnique = UnityEngine.Random.Range(0, validTechniques.Count);
        return validTechniques[randomTechnique];
    }


    public Treasure GetTreasureFinds()
    {
        int difficulty = (int)Mathf.Clamp((float)manager.difficultyValue / 5, 0 , 5);
        List<Treasure> validTreasures = new List<Treasure>();
        switch (difficulty)
        {
            case 0:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if (Treasure.rarity == Rarity.COMMON && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
            case 1:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if ((Treasure.rarity == Rarity.COMMON) || (Treasure.rarity == Rarity.RARE) && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
            case 2:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if (((Treasure.rarity == Rarity.COMMON || Treasure.rarity == Rarity.RARE) || Treasure.rarity == Rarity.EPIC) && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
            case 3:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if ((Treasure.rarity == Rarity.RARE || Treasure.rarity == Rarity.EPIC) && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
            case 4:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if ((Treasure.rarity == Rarity.RARE || Treasure.rarity == Rarity.EPIC || Treasure.rarity == Rarity.LEGENDARY) && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
            case 5:
            {
                foreach (Treasure Treasure in AllTreasures)
                {
                    if ((Treasure.rarity == Rarity.EPIC || Treasure.rarity == Rarity.LEGENDARY) && Treasure.shoppable == true)
                    {
                        validTreasures.Add(Treasure);
                    }
                }
                break;
            }
        }
        int randomTreasure = UnityEngine.Random.Range(0, validTreasures.Count);
        return validTreasures[randomTreasure];
    }
}
