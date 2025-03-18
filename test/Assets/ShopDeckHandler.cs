using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ShopDeckHandler : MonoBehaviour
{
    private manager manager;
    int excludedType = 100;
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
    public GameObject shopPrefab;
    public GameObject packPrefab;

    List<GameObject> removing = new List<GameObject>();
    public static List<GameObject> partitionsDeck = new List<GameObject>();
    public static List<GameObject> partitionsTreasure = new List<GameObject>();
    public static List<GameObject> partitionsConsumable = new List<GameObject>();
    public Entity player;
    public Button RerollButton;
    public TMP_Text rerollCost;
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
    public Entity friend;
    List<Technique> AllTechniques = new List<Technique>();
    List<Treasure> AllTreasures = new List<Treasure>();
    List<Treasure> foundTreasures = new List<Treasure>();
    List<Treasure> removingTreasure = new List<Treasure>();
    List<Technique> foundTechniques = new List<Technique>();
    List<Technique> removingTechnique = new List<Technique>();
    // ....
    public Texture smallTechniquePack, bigTechniquePack;

    

    int normalizex = 1280;
    int normalizey = 720;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.rolls = manager.defaultRolls;
        rerollCost.text = manager.rerollCost.ToString();
        AddTreasure(testTreasure);
        AddTreasure(testTreasure);
        AddConsumable(testConsumable);
        if (manager.allies.Count == 0)
        {
            manager.allies.Add(friend);
        }
        AddAll();
        RefreshCards();
        GetNewFinds();
        CreateLocalShops();
        if (manager.NewTreasure == null)
        {
            manager.NewTreasure = new UnityEvent();
        }
        if (manager.NewConsumable == null)
        {
            manager.NewConsumable = new UnityEvent();
        }
        if (manager.Reroll == null)
        {
            manager.Reroll = new UnityEvent();
        }
        manager.NewTreasure.AddListener(RefreshCards);
        manager.NewConsumable.AddListener(RefreshCards);
        RerollButton.onClick.AddListener(Reroll);


        Debug.Log(manager.treasures.Count);
        manager.NewTreasure.Invoke();
        manager.NewConsumable.Invoke(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reroll()
    {
        if (manager.money >= manager.rerollCost)
        {
            manager.money -= manager.rerollCost;
            manager.rerollCost += 4;
            rerollCost.text = manager.rerollCost.ToString();
            manager.moneyUpdated.Invoke();
            GetNewFinds();
        }
    }


    public void RefreshCards()
    {
        CreatePartitionObjects();
        SetToIndexDeck();
    }

    public void GetNewFinds()
    {
        GetLocalFinds();
        CreateLocalFinds();
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

    public void CreateLocalFinds()
    {
        foreach (GameObject find in manager.localFinds)
        {
            removing.Add(find);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            manager.localFinds.Remove(removing[index]);
            Destroy(removing[index]);
        }
        int i = 1;
        int modBoundRight = -2075;
        int modBoundLeft = -2450;
        foreach (Technique technique in foundTechniques)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)2)));
            GameObject clone = Instantiate(shopPrefab, new Vector2(posx+normalizex, -550+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            manager.localFinds.Add(clone);
            clone.GetComponent<ShopObject>().usedTechnique = technique;
            clone.GetComponent<ShopObject>().thisObject = clone;
            clone.GetComponent<ShopObject>().isTechnique = true;
            clone.GetComponent<ShopObject>().shop = true;
            i++;
        }
        foreach (Treasure treasure in foundTreasures)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)3-(float)1)));
            GameObject clone = Instantiate(shopPrefab, new Vector2(posx+normalizex, -550+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            manager.localFinds.Add(clone);
            clone.GetComponent<ShopObject>().usedTreasure = treasure;
            clone.GetComponent<ShopObject>().thisObject = clone;
            clone.GetComponent<ShopObject>().isTreasure = true;
            clone.GetComponent<ShopObject>().shop = true;
            i++;
        }
    }

    public void CreateLocalShops()
    {
        int i = 1;
        int modBoundRight = -2075;
        int modBoundLeft = -2450;
        float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)2-(float)1)));
        GetShop(posx);
        i++;
    }

    // this will crash if you run it twice right now. or just not work in some horrible way. please wait until other things are implemented then run it twice
    public void GetShop(float posx)
    {
        int isBig = UnityEngine.Random.Range(0,5);
        bool big = false;
        int cost = 0;
        Texture texture;
        if (isBig == 4)
        {
            big = true;
        }
        int getType = UnityEngine.Random.Range(0,6);
        while (getType == excludedType)
        {
            getType = UnityEngine.Random.Range(0,6);
        }
        getType = 0; //for testing, remove later
        switch (getType)
        {
            case 0:
            {
                foreach (Technique technique in manager.techniqueShop)
                {
                    removingTechnique.Add(technique);
                }
                for (int index = 0; index < removingTechnique.Count; index++)
                {
                    manager.techniqueShop.Remove(removingTechnique[index]);
                }
                if (big == true)
                {
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    cost = 6;
                }
                else
                {
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    manager.techniqueShop.Add(GetTechniqueFinds());
                    cost = 4;
                }
                GameObject clone = Instantiate(packPrefab, new Vector2(posx+normalizex, -1125+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                clone.GetComponent<PackObject>().costMoney = cost;
                clone.GetComponent<PackObject>().shopType = PackType.TECHNIQUE;
                clone.GetComponent<PackObject>().bigPack = big;
                clone.GetComponent<PackObject>().thisObject = clone;
                excludedType = 0;
                break;
            }
            case 1:
            {
                // treasure
                break;
            }
            case 2:
            {
                // consumable
                break;
            }
            case 3:
            {
                // potion
                break;
            }
            case 4:
            {
                // roller
                break;
            }
            case 5:
            {
                // ally
                break;
            }
        }
    }

    public void GetLocalFinds()
    {
        int treasureFinds = UnityEngine.Random.Range(1, 4);
        int techniqueFinds = 3 - treasureFinds;
        treasureFinds = 0;
        techniqueFinds = 3; // these are for testing while there are little treasures
        Debug.Log("Treasure Found: " + treasureFinds);
        Debug.Log("Techniques Found: " + techniqueFinds);
        foreach (Treasure find in foundTreasures)
        {
            removingTreasure.Add(find);
        }
        for (int index = 0; index < removingTreasure.Count; index++)
        {
            foundTreasures.Remove(removingTreasure[index]);
        }
        foreach (Technique find in foundTechniques)
        {
            removingTechnique.Add(find);
        }
        for (int index = 0; index < removingTechnique.Count; index++)
        {
            foundTechniques.Remove(removingTechnique[index]);
        }
        for (int i = 0; i < techniqueFinds; i++)
        {
            foundTechniques.Add(GetTechniqueFinds());
        }
        for (int i = 0; i < treasureFinds; i++)
        {
            foundTreasures.Add(GetTreasureFinds());
        }
    }

    void AddTreasure(Treasure treasure)
    {
        if (manager.treasures.Count < manager.treasureMax)
        {
            manager.treasures.Add(treasure);
        }
    }

    void AddConsumable(Technique consumable)
    {
        if (manager.consumables.Count < manager.consumableMax)
        {
            manager.consumables.Add(consumable);
        }
    }


    void AddAll()
    {
        // this is horrible
        AllTreasures.AddRange(new List<Treasure>() {machineKey});
        AllTechniques.AddRange(new List<Technique>() {knives, twinKnives, cutOff, punch, staggeringPunch, lightspeedFist, flurryOfBlows, quickSlash, flurrySlash, fireBolt, iceBeam, windShear, lightningBolt, cleansingLight, siphon, blackHole, trueStrike, megidola, megidolaon, decimate, worldEndingStrike, daqAttack});
        Debug.Log("All Treasure count: " + AllTreasures.Count);
        Debug.Log("All Technique count: " + AllTechniques.Count);    
    }


    public Technique GetTechniqueFinds()
    {
        int difficulty = (int)Mathf.Clamp((float)manager.difficultyValue / 5, 0 , 5);
        Debug.Log("Difficulty value: " + difficulty);
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
        int randomTechnique = UnityEngine.Random.Range(0, validTechniques.Count - 1);
        Debug.Log("Random value: " + randomTechnique);
        Debug.Log("Valid Techniques: " + validTechniques.Count);
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
        int randomTreasure = UnityEngine.Random.Range(0, validTreasures.Count - 1);
        Debug.Log("Valid Treasures: " + validTreasures.Count);
        return validTreasures[randomTreasure];
    }

}
