using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Entity", menuName = "Scriptable Objects/Entity")]
public class Entity : ScriptableObject
{
    public bool playableCharacter;
    public int MaxHealth;
    public int Health;
    public int MaxMana;
    public int Mana;
    public int Armour;
    public int StartShield;
    public int Shield;
    public int ArmourPen;

    public List<Technique> techniques = new List<Technique>();

    public double slashing = 1;
    public double piercing = 1;
    public double bludgeoning = 1;
    public double fire = 1;
    public double cold = 1;
    public double force = 1;
    public double lightning = 1;
    public double holy = 1;
    public double evil = 1;
}
