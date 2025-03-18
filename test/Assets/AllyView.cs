using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class AllyView : MonoBehaviour
{
    private manager manager;
    public GameObject AllyDisplay;
    public bool Active = false;
    public TMP_Text Name1, Name2, Name3;
    public TMP_Text HP1, HP2, HP3;
    public TMP_Text MP1, MP2, MP3;
    public TMP_Text ARM1, ARM2, ARM3;
    public TMP_Text SHLD1, SHLD2, SHLD3;
    public GameObject Ally1Obj, Ally2Obj, Ally3Obj;
    public GameObject Ally1Img, Ally2Img, Ally3Img;
    public Button Dismiss1, Dismiss2, Dismiss3, Close, Open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ally1Obj.SetActive(false);
        Ally2Obj.SetActive(false);
        Ally3Obj.SetActive(false);
        AllyDisplay.SetActive(false);
        Dismiss1.onClick.AddListener(OnClick1);
        Dismiss2.onClick.AddListener(OnClick2);
        Dismiss3.onClick.AddListener(OnClick3);
        Close.onClick.AddListener(ToggleDisplay);
        Open.onClick.AddListener(ToggleDisplay);
    }

    // Update is called once per frame
    void Update()
    {
       
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
        AllyDisplay.SetActive(Active);
        AllyDisplay.transform.SetAsLastSibling();
        UpdateDisplay();
    }

    public void DismissAlly(int index)
    {
        manager.allies.RemoveAt(index);
        UpdateDisplay();
    }

    public void OnClick1()
    {
        DismissAlly(0);
    }

    public void OnClick2()
    {
        DismissAlly(1);
    }

    public void OnClick3()
    {
        DismissAlly(2);
    }

    void UpdateDisplay()
    {
        if (manager.allies.Count == 0)
        {
            Ally1Obj.SetActive(false);
            Ally2Obj.SetActive(false);
            Ally3Obj.SetActive(false);
        }
        else
        {
            Ally1Obj.SetActive(true);
            Entity Ally1 = manager.allies[0];
            Name1.text = manager.allies[0].name;
            HP1.text = Ally1.MaxHealth.ToString();
            MP1.text = Ally1.MaxMana.ToString();
            ARM1.text = Ally1.Armour.ToString();
            SHLD1.text = Ally1.StartShield.ToString();
            RawImage img1 = (RawImage)Ally1Img.GetComponent<RawImage>();
            img1.texture = Ally1.bust;
            if (manager.allies.Count > 1)
            {
                Ally2Obj.SetActive(true);
                Entity Ally2 = manager.allies[1];
                Name2.text = Ally2.name;
                HP2.text = Ally2.MaxHealth.ToString();
                MP2.text = Ally2.MaxMana.ToString();
                ARM2.text = Ally2.Armour.ToString();
                SHLD2.text = Ally2.StartShield.ToString();
                RawImage img2 = (RawImage)Ally2Img.GetComponent<RawImage>();
                img2.texture = Ally2.bust;
            }
            if (manager.allies.Count > 2)
            {
                Ally3Obj.SetActive(true);
                Entity Ally3 = manager.allies[2];
                Name3.text = Ally3.name;
                HP3.text = Ally3.MaxHealth.ToString();
                MP3.text = Ally3.MaxMana.ToString();
                ARM3.text = Ally3.Armour.ToString();
                SHLD3.text = Ally3.StartShield.ToString();
                RawImage img3 = (RawImage)Ally3Img.GetComponent<RawImage>();
                img3.texture = Ally3.bust;
            }
        }
    }


}
