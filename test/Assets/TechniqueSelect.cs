using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TechniqueSelect : MonoBehaviour
{
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
    List<Technique> AllTechniques = new List<Technique>();


    void AddAll()
    {
        // this is horrible
        AllTechniques.AddRange(new List<Technique>() {knives, twinKnives, cutOff, punch, staggeringPunch, lightspeedFist, flurryOfBlows, quickSlash, flurrySlash, fireBolt, iceBeam, windShear, lightningBolt, cleansingLight, siphon, blackHole, trueStrike, megidola, megidolaon, decimate, worldEndingStrike, daqAttack});
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Technique GetTechniqueFinds()
    {
        int difficulty = (int)Mathf.Clamp((float)manager.difficultyValue / 5, 0 , 5);
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
        int randomTechnique = UnityEngine.Random.Range(0, validTechniques.Count);
        return validTechniques[randomTechnique];
    }
}
