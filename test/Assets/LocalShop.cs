using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LocalShop : MonoBehaviour
{
    public PackType shopType;
    public bool isBigShop = false;
    public GameObject objectPrefab;
    int normalizex = 1280;
    int normalizey = 720;
    int selectionCount = 1;
    int selectedCards = 0;
    public TMP_Text typeOfShop;
    public TMP_Text amountofGold;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (manager.ShopCardTaken == null)
        {
            manager.ShopCardTaken = new UnityEvent();
        }
        if (manager.ReturnShopInfo== null)
        {
            manager.ReturnShopInfo = new UnityEvent<PackType, bool>();
        }
        manager.ShopCardTaken.AddListener(CardTaken);
        manager.ReturnShopInfo.AddListener(ShopTime);
        manager.InitializeShop.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateShop()
    {
        int cardCount = 2;
        int modBoundRight = -800;
        int modBoundLeft = -1800;
        int i = 1;
        if (isBigShop == true)
        {
            cardCount = 3;
            selectionCount = 2;
        }
        if (shopType == PackType.TECHNIQUE)
        {
            foreach (Technique technique in manager.techniqueShop)
            {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)cardCount)));
            GameObject clone = Instantiate(objectPrefab, new Vector2(posx+normalizex, -700+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas2").transform);
            clone.GetComponent<ShopObject>().usedTechnique = technique;
            clone.GetComponent<ShopObject>().isTechnique = true;
            clone.GetComponent<ShopObject>().thisObject = clone;
            clone.GetComponent<ShopObject>().shop = false;
            i++;
            }
        }
        else if (shopType == PackType.TREASURE)
        {
            foreach (Treasure treasure in manager.treasureShop)
            {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)cardCount)));
            GameObject clone = Instantiate(objectPrefab, new Vector2(posx+normalizex, -700+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas2").transform);
            clone.GetComponent<ShopObject>().usedTreasure = treasure;
            clone.GetComponent<ShopObject>().isTreasure = true;
            clone.GetComponent<ShopObject>().thisObject = clone;
            clone.GetComponent<ShopObject>().shop = false;
            i++;
            }
        }
        else if (shopType == PackType.CONSUMABLE)
        {
            foreach (Technique technique in manager.consumableShop)
            {
            float posx = (float)modBoundLeft + (((float)modBoundRight - (float)modBoundLeft)*(((float)i-(float)1) / ((float)cardCount)));
            GameObject clone = Instantiate(objectPrefab, new Vector2(posx+normalizex, -700+normalizey), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas2").transform);
            clone.GetComponent<ShopObject>().usedTechnique = technique;
            clone.GetComponent<ShopObject>().isConsumable = true;
            clone.GetComponent<ShopObject>().thisObject = clone;
            clone.GetComponent<ShopObject>().shop = false;
            i++;
            }
        }
    }

    void CardTaken()
    {
        selectedCards += 1;
        if (selectedCards >= selectionCount)
        {
            EndShop();
        }
    }

    void EndShop()
    {
        SceneManager.UnloadSceneAsync(2);
    }

    public void ShopTime(PackType shop, bool isBig)
    {
        isBigShop = isBig;
        shopType = shop;
        CreateShop();
        UpdateText();
    }

    void UpdateText()
    {
        amountofGold.text = "You have brought enough funds to purchase " + selectionCount + " items.";
        typeOfShop.text = "This seems to be a " + shopType + " shop.";
    }
}
