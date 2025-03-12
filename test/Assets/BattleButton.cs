using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;


public class BattleButton : MonoBehaviour
{
 
    //Make sure to attach these Buttons in the Inspector
    public Button DiscardButton;

    
    void Start()
    {
        DiscardButton.onClick.AddListener(DiscardOnClick);
    }

    void DiscardOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        manager.DiscardSelected.Invoke(true);
    }


}

