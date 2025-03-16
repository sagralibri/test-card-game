using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class DeckView : MonoBehaviour
{
    private manager manager;
    public GameObject cardPrefab;
    public GameObject deckDisplay;
    public bool Active = false;
    List<GameObject> removing = new List<GameObject>();
    public Button Close, Open;
    int normalizex = 1280;
    int normalizey = 720;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deckDisplay.SetActive(false);
        Close.onClick.AddListener(ToggleDisplay);
        Open.onClick.AddListener(ToggleDisplay);
    }

    public void ToggleDisplay()
    {
        if (Active == false)
        {
            Active = true;
        }
        else
        {
            Active = false;
        }
        deckDisplay.SetActive(Active);
        deckDisplay.transform.SetAsLastSibling();
        if (Active == true)
        {
            GetCards();
        }
    }

    void GetCards()
    {
        foreach (GameObject objeect in manager.drawnCardObjects)
        {
            removing.Add(objeect);
        }
        for (int index = 0; index < removing.Count; index++)
        {
            manager.drawnCardObjects.Remove(removing[index]);
            Destroy(removing[index]);
        }
        List<Technique> damageCards = new List<Technique>();
        List<Technique> healingCards = new List<Technique>();
        List<Technique> utilityCards = new List<Technique>();
        Debug.Log("total cards in deck: " + manager.playerDeck.Count);
        foreach (Technique card in manager.playerDeck)
        {
            // fuck switch statements
            if (card.techniqueType == TechniqueType.DAMAGE)
            {
                damageCards.Add(card);
            }
            else if (card.techniqueType == TechniqueType.HEALING)
            {
                healingCards.Add(card);
            }
            else if (card.techniqueType == TechniqueType.UTILITY)
            {
                utilityCards.Add(card);
            }
        }
        Debug.Log("total damage cards: " + damageCards.Count);
        int i = 1;
        int modBoundRight = -375;
        int modBoundLeft = -2150;
        foreach (Technique card in damageCards)
        {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)damageCards.Count-(float)1)));
            GameObject clone = Instantiate(cardPrefab, new Vector2(posx+normalizex, -430+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("DeckView").transform);
            clone.GetComponent<Knives>().usedTechnique = card;
            manager.drawnCardObjects.Add(clone);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
