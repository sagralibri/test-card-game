using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool shop;
    public bool isTechnique;
    public bool isTreasure;
    public bool isRoller;
    public bool isPower;
    public bool isAlly;
    public bool isConsumable;
    Technique usedTechnique;
    Treasure usedTreasure;
    Entity usedAlly;
    public TMP_Text cost;
    public TMP_Text manaCost;
    public TMP_Text rarity;
    public TMP_Text nameText;
    public TMP_Text description;
    public GameObject ImageOnPanel;
    public GameObject InfoTechnique;
    public GameObject InfoTreasure;
    Texture NewTexture;
    public GameObject thisObject;
	private RawImage img;
    private bool mouse_over = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InfoTechnique.SetActive(false);
        InfoTreasure.SetActive(false);
        if (isTechnique == true || isConsumable == true)
        {
            NewTexture = usedTechnique.cardImage;
        }
        else if (isTreasure == true)
        {
            NewTexture = usedTreasure.treasureImage;
        }
        else if (isRoller == true || isPower == true)
        {

        }
        else if (isAlly == true)
        {

        }
        
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
		img.texture = (Texture)NewTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_over == true)
        {
            if (isTechnique == true || isConsumable == true)
            {
                InfoTechnique.SetActive(true);
            }
            else if (isTreasure == true)
            {
                InfoTechnique.SetActive(true);
            }
            else if (isRoller == true || isPower == true)
            {

            }
            else if (isAlly == true)
            {

            }
            if(Input.GetMouseButtonDown(0) == true)
            {
                BuyCard();
            }
        }
        else
        {
            InfoTechnique.SetActive(false);
            InfoTechnique.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    public int GetCost()
    {
        int cost = 0;
        if (isTechnique == true || isConsumable == true)
        {
            cost = usedTechnique.cost;
        }
        else if (isTreasure == true)
        {
            cost = usedTreasure.cost;
        }
        else if (isRoller == true || isPower == true)
        {

        }
        else if (isAlly == true)
        {

        }
        return cost;
    }

    public void BuyCard()
    {
        if (manager.money >= GetCost() || shop == false)
        {
            if (shop == true)
            {
            manager.money -= GetCost();
            }
            if (isTechnique == true)
            {
                manager.playerDeck.Add(usedTechnique);
            }
            else if (isConsumable == true)
            {
                manager.consumables.Add(usedTechnique);
            }
            else if (isTreasure == true)
            {
                manager.treasures.Add(usedTreasure);
            }
            else if (isRoller == true || isPower == true)
            {

            }
            else if (isAlly == true)
            {
                manager.allies.Add(usedAlly);
            }
            if (shop == true)
            {
                manager.localFinds.Remove(thisObject);
                Destroy(thisObject);
                // Sayonara.
            }
            else
            {
                // pack opening list
                Destroy(thisObject);
            }

        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }

}
