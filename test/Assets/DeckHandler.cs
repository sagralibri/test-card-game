using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeckHandler : MonoBehaviour
{
    private manager manager;
    //partition lines
    int partitionLinesDeck = manager.drawnMax;
    int partitionLinesTreasure = manager.treasureMax;
    int partitionLinesConsumable = manager.consumableMax;
    //test input
    public GameObject empty;
    public Technique Knives;
    public Technique TwinKnives;
    public Technique WorldEnd;
    public Technique FuckYou;
    public Treasure testTreasure;
    public Technique testConsumable;
    // prefabs
    public GameObject cardPrefab;
    public GameObject treasurePrefab;
    public GameObject consumablePrefab;

    List<GameObject> removing = new List<GameObject>();
    List<Technique> removingTechnique = new List<Technique>();
    // how tf do i set these automatically
    int topDeckBound = 550;
    int bottomDeckBound = -670;
    public static List<GameObject> partitionsDeck = new List<GameObject>();
    public static List<GameObject> partitionsTreasure = new List<GameObject>();
    public static List<GameObject> partitionsConsumable = new List<GameObject>();
    public Entity player;
    

    int normalizex = 1280;
    int normalizey = 720;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager.playerDeck.Add(Knives);
        manager.playerDeck.Add(Knives);
        manager.playerDeck.Add(Knives);
        FillDrainableDeck();
        DrawAll();
        CreatePartitionObjects();
        SetToIndexDeck();


        if (manager.DiscardSelected == null)
            manager.DiscardSelected = new UnityEvent<bool>();
        manager.DiscardSelected.AddListener(DiscardSelected);

        if (manager.NewTreasure == null)
            manager.NewTreasure = new UnityEvent();
        manager.NewTreasure.AddListener(RefreshCards);

        if (manager.AttackEntity == null)
            manager.AttackEntity = new UnityEvent<Assignment, Entity, bool, Entity>();
        manager.AttackEntity.AddListener(UseTechnique);

        if (manager.EraseExcess == null)
            manager.EraseExcess = new UnityEvent();
        manager.EraseExcess.AddListener(RemoveExcess);

        manager.NewTreasure.Invoke();
        manager.NewConsumable.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(partitionsDeck.Count != partitionLinesDeck)
        {
        CreatePartitionObjects();
        SetToIndexDeck();
        }

        if (GetSelected() != null)
        {
            
        }
    }

    void DiscardSelected(bool discard)
    {
        if((manager.drawnCards.Count > 1 && manager.discards > 0) || (discard == false))
        {
        int selected = -1;
        int i = 0;
        foreach (GameObject drawnCard in manager.drawnCardObjects)
        {
            if (drawnCard.GetComponent<Knives>().selected == true)
            {
                selected = i;
            }
            i++;
        }
        if (selected != -1)
        {
            if (discard == true)
            {
                manager.discards -= 1;
            }
            manager.drawnCards.RemoveAt(selected);
            DrawRandCard();
            RefreshCards();
            manager.UpdateDiscardCounter.Invoke();
        }
        }
        else
        {
            Debug.Log("You cannot discard your last card, or not enough discards.");
        }
    }

    public void RefreshCards()
    {
        CreatePartitionObjects();
        SetToIndexDeck();
    }

    public void SetToIndexDeck()
    {
        int i = 0;
        foreach (GameObject objeect in manager.drawnCardObjects)
        {
            removing.Add(objeect);
        }
        foreach (GameObject objeect in manager.drawnTreasureObjects)
        {
            removing.Add(objeect);
        }
        foreach (GameObject objeect in manager.drawnConsumableObjects)
        {
            removing.Add(objeect);
        } 
        for (int index = 0; index < removing.Count; index++)
        {
            manager.drawnCardObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
        foreach (Technique cardd in manager.drawnCards)
        {
            GameObject currentpartition = partitionsDeck[i];
            Vector2 pos = new Vector3(-normalizex, -normalizey, 0) + currentpartition.transform.position;
            GameObject card = Instantiate(cardPrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            card.GetComponent<Knives>().usedTechnique = cardd;
            manager.drawnCardObjects.Add(card);
            i++;
        }
        i = 0;
        foreach (Treasure treasure in manager.treasures)
        {
            GameObject currentpartition = partitionsTreasure[i];
            Vector2 pos = new Vector3(-normalizex, -normalizey, 0) + currentpartition.transform.position;
            GameObject newTreasure = Instantiate(treasurePrefab, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
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
            newConsumable.GetComponent<ConsumableScript>().usedConsumable = consumable;
            manager.drawnConsumableObjects.Add(newConsumable);
            i++;
        }
    }



    public void CreatePartitionObjects()
    {
        int modBoundRight = -210;
        int modBoundLeft = -900;
        int partitionLinesDeck = manager.drawnMax;
        int posy = -(topDeckBound - ((bottomDeckBound - topDeckBound)/2));
        partitionLinesDeck = manager.drawnMax;
        partitionLinesTreasure = manager.treasureMax;
        partitionLinesConsumable = manager.consumableMax;
        foreach (GameObject partition in partitionsDeck)
        {
            removing.Add(partition);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            partitionsDeck.Remove(removing[index]);
            Destroy(removing[index]);
        }
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
        for (int i = 1; i <= partitionLinesDeck; i++)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesDeck-(float)1)));
            GameObject clone = Instantiate(empty, new Vector2(posx+normalizex, -580+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsDeck.Add(clone);
        }
        modBoundRight = 1156;
        modBoundLeft = 431;
        for (int i = 1; i <= partitionLinesTreasure; i++)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesTreasure-(float)1)));
            GameObject clone = Instantiate(empty, new Vector2(posx+normalizex, -580+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsTreasure.Add(clone);
        }
        modBoundRight = 950; // change these
        modBoundLeft = 481;
        for (int i = 1; i <= partitionLinesConsumable; i++)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)partitionLinesConsumable-(float)1)));
            GameObject clone = Instantiate(empty, new Vector2(posx+normalizex, 660+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            partitionsConsumable.Add(clone);
        }
    }


    public void DrawCardfromDeck(int drawncard)
    {
        if (manager.drainableDeck.Count > 0 && manager.drawnCards.Count != manager.drawnMax)
        {
        Technique card = manager.drainableDeck[drawncard];
        manager.drainableDeck.Remove(card);
        manager.drawnCards.Add(card);
        }
    }

    public void DrawAll()
    {
        while (manager.drainableDeck.Count > 0 && manager.drawnCards.Count != manager.drawnMax)
        {
            DrawRandCard();
        }
    }

    public void DrawRandCard()
    {
        if (manager.drainableDeck.Count > 0 && manager.drawnCards.Count != manager.drawnMax)
        {
            int random = UnityEngine.Random.Range(0, manager.drainableDeck.Count);
            DrawCardfromDeck(random);
        }
    
    }

    public Technique GetSelected()
    {
        int selected = -1;
        int i = 0;
        foreach (GameObject drawnCard in manager.drawnCardObjects)
        {
            if (drawnCard.GetComponent<Knives>().selected == true)
            {
                selected = i;
            }
            i++;
        }
        if (selected != -1)
            return manager.drawnCards[selected];
        else
            return null;
    }


    public void UseTechnique(Assignment input, Entity target, bool enemy, Entity caster)
    {
        Technique usedTechnique = GetSelected();
        if (usedTechnique)
        {
            if (usedTechnique.manaCost <= manager.uploadMP)
            {
                switch (usedTechnique.targeting)
                {
                    case Target.SELF:
                        if (target == player)
                            RunTechnique(input, usedTechnique, target, caster);
                            manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                            DiscardSelected(false);
                        break;
                    case Target.ONEENEMY:
                        if (enemy == true)
                            RunTechnique(input, usedTechnique, target, caster);
                            manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                            DiscardSelected(false);
                        break;
                    case Target.ALLENEMIES:
                        if (enemy == true)
                            RunAllEnemies(usedTechnique, enemy, caster);
                            manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                            DiscardSelected(false);
                        break;
                    case Target.ONEALLY:
                        if (enemy != true)
                            RunTechnique(input, usedTechnique, target, caster);
                            manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                            DiscardSelected(false);
                        break;
                    case Target.ALLALLIES:
                        if (enemy != true)
                            RunAllAllies(usedTechnique, enemy, caster);
                            manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                            DiscardSelected(false);
                        break;
                    case Target.ALL:
                        RunTechnique(input, usedTechnique, player, caster);
                        RunAllEnemies(usedTechnique, enemy, caster);
                        RunAllAllies(usedTechnique, enemy, caster);
                        manager.GetMana.Invoke(Assignment.PLAYER, usedTechnique, target, caster);
                        DiscardSelected(false);
                        break; 
                }
                if (usedTechnique.consumable == false)
                {
                    manager.NextTurn.Invoke();
                }
            }
        }
    }

    public void FillDrainableDeck()
    {
        foreach (Technique card in manager.playerDeck)
        {
            manager.drainableDeck.Add(card);
        }
    }

    public void RunTechnique(Assignment input, Technique usedTechnique, Entity target, Entity caster)
    {
        manager.UseTechnique.Invoke(input, usedTechnique, target, caster);
    }

    public void RunAllEnemies(Technique usedTechnique, bool enemy, Entity caster)
    {
        if (enemy == true)
            if (manager.enemies.Count > 0)
                RunTechnique(Assignment.E1, usedTechnique, manager.enemies[0], caster);
            if (manager.enemies.Count > 1)
                RunTechnique(Assignment.E2, usedTechnique, manager.enemies[1], caster);
            if (manager.enemies.Count > 2)
                RunTechnique(Assignment.E3, usedTechnique, manager.enemies[2], caster);
            if (manager.enemies.Count > 3)
                RunTechnique(Assignment.E4, usedTechnique, manager.enemies[3], caster);
        else 
            if (manager.allies.Count > 0)
                RunTechnique(Assignment.A1, usedTechnique, manager.allies[0], caster);
            if (manager.allies.Count > 1)
                RunTechnique(Assignment.A2, usedTechnique, manager.allies[1], caster);
            if (manager.allies.Count > 2)
                RunTechnique(Assignment.A3, usedTechnique, manager.allies[2], caster);
    }

    public void RunAllAllies(Technique usedTechnique, bool enemy, Entity caster)
    {
        if (enemy == false)
            if (manager.enemies.Count > 0)
                RunTechnique(Assignment.E1, usedTechnique, manager.enemies[0], caster);
            if (manager.enemies.Count > 1)
                RunTechnique(Assignment.E2, usedTechnique, manager.enemies[1], caster);
            if (manager.enemies.Count > 2)
                RunTechnique(Assignment.E3, usedTechnique, manager.enemies[2], caster);
            if (manager.enemies.Count > 3)
                RunTechnique(Assignment.E4, usedTechnique, manager.enemies[3], caster);
        else 
            if (manager.allies.Count > 0)
                RunTechnique(Assignment.A1, usedTechnique, manager.allies[0], caster);
            if (manager.allies.Count > 1)
                RunTechnique(Assignment.A2, usedTechnique, manager.allies[1], caster);
            if (manager.allies.Count > 2)
                RunTechnique(Assignment.A3, usedTechnique, manager.allies[2], caster);
    }


    public void RemoveExcess()
    {
        foreach (GameObject partition in partitionsDeck)
        {
            removing.Add(partition);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            partitionsDeck.Remove(removing[index]);
            Destroy(removing[index]);
        }
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
        foreach(Technique card in manager.drawnCards)
        {
            removingTechnique.Add(card);
        }
        for (int index = 0; index < removingTechnique.Count; index++)
        {
            manager.drawnCards.Remove(removingTechnique[index]);
        }
        foreach(Technique card in manager.drainableDeck)
        {
            removingTechnique.Add(card);
        }
        for (int index = 0; index < removingTechnique.Count; index++)
        {
            manager.drainableDeck.Remove(removingTechnique[index]);
        }
    }
}
