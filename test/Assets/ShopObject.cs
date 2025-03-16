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
    public Technique usedTechnique;
    public Treasure usedTreasure;
    public Entity usedAlly;
    public TMP_Text cost;
    public TMP_Text manaCost;
    public TMP_Text rarityTechnique;
    public TMP_Text rarityTreasure;
    public TMP_Text nameTechnique;
    public TMP_Text nameTreasure;
    public TMP_Text descriptionTechnique;
    public TMP_Text descriptionTreasure;
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
        cost.text = GetCost().ToString();
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
		img.texture = (Texture)NewTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_over == true)
        {
            UpdateText();
            if (isTechnique == true || isConsumable == true)
            {
                InfoTechnique.SetActive(true);
            }
            else if (isTreasure == true)
            {
                InfoTreasure.SetActive(true);
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
            InfoTreasure.SetActive(false);
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
                Destroy(thisObject); // replace with invoke later
                // Sayonara.
            }
            else
            {
                // pack opening list
                Destroy(thisObject); // replace with invoke later
            }

        }
        else
        {
            Debug.Log("Insufficient Funds");
        }
    }

    public void UpdateText()
    {
        if (isTechnique == true || isConsumable == true)
        {
            manaCost.text = usedTechnique.manaCost.ToString();
            rarityTechnique.text = usedTechnique.rarity.ToString();
            nameTechnique.text = GetName();
            descriptionTechnique.text = GetDescription();
            cost.text = GetCost().ToString();
        }
        else if (isTreasure == true)
        {
            rarityTreasure.text = usedTreasure.rarity.ToString();
            nameTreasure.text = GetName();
            descriptionTreasure.text = GetDescription();
            cost.text = GetCost().ToString();
        }
        else if (isRoller == true || isPower == true)
        {

        }
        else if (isAlly == true)
        {

        }
    }
    


    public string GetDescription()
    {
        string desc = "";
        if (isTechnique == true || isConsumable == true)
        {
            string explanationtext = "";
            string targetText = "";
            switch (usedTechnique.targeting) // update this later
            {
                case (Target.SELF):
                    targetText = " to yourself";
                    break;
                case (Target.ONEENEMY):
                    targetText = " to one enemy";
                    break;
                case (Target.ALLENEMIES):
                    targetText = " to all enemies";
                    break;
                case (Target.ONEALLY):
                    targetText = " to one ally";
                    break;
                case (Target.ALLALLIES):
                    targetText = " to all allies";
                    break;
                case (Target.ALL):
                    targetText = " to all creatures";
                    break;
            }

            if (usedTechnique.variableDamage == true)
            {
                explanationtext = "Deal " + usedTechnique.damage + " to " + usedTechnique.lowerDamage + " " + usedTechnique.damageType + targetText;
            }
            else if (usedTechnique.damage != 0)
            {
                explanationtext = "Deal " + usedTechnique.damage + " " + usedTechnique.damageType + targetText;
            }

            if (usedTechnique.repeat > 1)
            {
                explanationtext = explanationtext + " " + usedTechnique.repeat + " times";
            }
            if (usedTechnique.healingFlat > 0 || usedTechnique.healingPercent > 0)
            {
                
            }
            if (usedTechnique.description != "")
            {
                explanationtext = usedTechnique.description;
            }
            desc = explanationtext;
        }
        else if (isTreasure == true)
        {
            desc = usedTreasure.description;
        }
        else if (isRoller == true || isPower == true)
        {

        }
        else if (isAlly == true)
        {

        }
        return desc;
    }

    public string GetName()
    {  
        string rname = "";
        if (isTechnique == true || isConsumable == true)
        {
            rname = usedTechnique.name;
        }
        else if (isTreasure == true)
        {
            rname = usedTreasure.name;
        }
        else if (isRoller == true || isPower == true)
        {

        }
        else if (isAlly == true)
        {

        }
        return rname;
    }
}
