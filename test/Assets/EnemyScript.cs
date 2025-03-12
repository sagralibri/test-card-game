using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class EntityScript : MonoBehaviour
{
    private manager manager;
    public Entity creature;
    public Entity player;
    public TMP_Text health;
    public TMP_Text entityName;
    public GameObject infoPanel;
    public bool enemy;
    public bool unconscious;
    // reveals
    bool piercingRevealed;
    bool slashingRevealed;
    bool bludgeoningRevealed;
    bool fireRevealed;
    bool coldRevealed;
    bool lightningRevealed;
    bool forceRevealed;
    bool holyRevealed;
    bool evilRevealed;
    public TMP_Text piercingAffinity;
    public TMP_Text slashingAffinity;
    public TMP_Text bludgeoningAffinity;
    public TMP_Text fireAffinity;
    public TMP_Text coldAffinity;
    public TMP_Text lightningAffinity;
    public TMP_Text forceAffinity;
    public TMP_Text holyAffinity;
    public TMP_Text evilAffinity;
    public GameObject entityObject;
    public Assignment assignment;
    private bool mouseOver = false;
    // stats
    public int MaxHealth;
    public int Health;
    public int MaxMana;
    public int Mana;
    public int Armour;
    public int Shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetStats();
        infoPanel.SetActive(false);
        TextUpdate();
        if (manager.ObjectHover == null)
            manager.ObjectHover = new UnityEvent<Assignment>();
        manager.ObjectHover.AddListener(OnHover);
        TextUpdate();
        if (manager.ObjectUnhover == null)
            manager.ObjectUnhover = new UnityEvent<Assignment>();
        manager.ObjectUnhover.AddListener(OnExit);
        if (manager.GetToEntity == null)
            manager.GetToEntity = new UnityEvent<Assignment, int, Technique>();
        manager.GetToEntity.AddListener(TakeDamage);
        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver == true)
        {
            infoPanel.SetActive(true);
            manager.GetAlignment.Invoke(assignment, enemy);
            TextUpdate();
            if(Input.GetMouseButtonDown(0) == true && manager.canAct == true && unconscious == false)
            {
                Debug.Log("" + creature);
                Debug.Log(player);
                manager.AttackEntity.Invoke(assignment, creature, enemy, player);
            }
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    void OnHover(Assignment input)
    {
        if (input == assignment)
            mouseOver = true;
    }

    void OnExit(Assignment input)
    {   
        if (input == assignment)
            mouseOver = false;
    }

    public void TakeDamage(Assignment input, int damage ,Technique technique)
    { 
        if (input == assignment)
            Health -= damage;
            if (Health <= 0)
                Health = 0;
                Die(input);
        switch (technique.damageType)
        {
            case DamageType.SLASHING:
                slashingRevealed = true;
                break;
            case DamageType.PIERCING:
                piercingRevealed = true;
                break;
            case DamageType.BLUDGEONING:
                bludgeoningRevealed = true;
                break;
            case DamageType.FIRE:
                fireRevealed = true;
                break;
            case DamageType.COLD:
                coldRevealed = true;
                break;
            case DamageType.LIGHTNING:
                lightningRevealed = true;
                break;
            case DamageType.FORCE:
                forceRevealed = true;
                break;
            case DamageType.HOLY:
                holyRevealed = true;
                break;
            case DamageType.EVIL:
                evilRevealed = true;
                break;
        }
    }

    void TextUpdate()
    {
        health.text = Health + " / " + creature.MaxHealth;
        entityName.text = creature.name;
        if (piercingRevealed == true)
        {
            piercingAffinity.text = creature.piercing + "";
        }
        if (slashingRevealed == true)
        {
            slashingAffinity.text = creature.slashing + "";
        }
        if (bludgeoningRevealed == true)
        {
            bludgeoningAffinity.text = creature.bludgeoning + "";
        }
        if (fireRevealed == true)
        {
            fireAffinity.text = creature.fire + "";
        }
        if (coldRevealed == true)
        {
            coldAffinity.text = creature.cold + "";
        }
        if (forceRevealed == true)
        {
            forceAffinity.text = creature.force + "";
        }
        if (lightningRevealed == true)
        {
            lightningAffinity.text = creature.lightning + "";
        }
        if (holyRevealed == true)
        {
            holyAffinity.text = creature.holy + "";
        }
        if (evilRevealed == true)
        {
            evilAffinity.text = creature.evil + "";
        }
    }

    void ResetStats()
    {
        double clampValue = manager.difficultyValue - 1;
        if (clampValue < 1)
        {
            clampValue = 1;
        }
        if (manager.difficultyValue > 1)
        {
            MaxHealth = Mathf.RoundToInt(UnityEngine.Random.Range(creature.MaxHealth * (float)clampValue, creature.MaxHealth * (float)manager.difficultyValue));
            MaxMana = Mathf.RoundToInt(UnityEngine.Random.Range(creature.MaxMana * (float)clampValue, creature.MaxMana * (float)manager.difficultyValue));
        }
        else
        {
            Health = creature.MaxHealth;
            Mana = creature.MaxMana;
        }
    }

    void Die(Assignment input)
    {
        unconscious = true;
        manager.KillEntity.Invoke(input);
    }
}
