using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class EntityObjectScript : MonoBehaviour
{
    private manager manager;
    public Assignment assignment;
    public bool enemy;
    public bool unconscious = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (manager.GetAlignment == null)
            manager.GetAlignment = new UnityEvent<Assignment, bool>();
        manager.GetAlignment.AddListener(GetAlignment);
        if (manager.KillEntity == null)
            manager.KillEntity = new UnityEvent<Assignment>();
        manager.KillEntity.AddListener(Die);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        manager.ObjectHover.Invoke(assignment);
    }

    void OnMouseExit()
    {
        manager.ObjectUnhover.Invoke(assignment);
    }

    void GetAlignment(Assignment input, bool isEnemy)
    {   
        if (input == assignment)
        {
            enemy = isEnemy;
        }
    }

    void Die(Assignment input)
    {
        Debug.Log("Killed ");
        if (input == assignment)
        {
            unconscious = true;
        }
    }
}
