using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;


public class DamageHandler : MonoBehaviour
{
    private manager manager; 
    public Entity player;

    /*public void AttackTarget(Technique technique, Entity target, Entity caster);
    {
        int baseDamage = 0;
        if technique.variableDamage == true
        {
            baseDamage = UnityEngine.Random.Range(technique.damage, technique.lowerDamage);
        }
        else
        {
            baseDamage = technique.damage;
        }
        caster.Mana -= technique.manaCost; //implement treasure system relating to mana cost later
        int armourReduction = target.Armour - caster.ArmourPen;
        if armourReduction < 0
        {
            armourReduction = 0;
        }
        baseDamage -= armourReduction;
        TakeDamage(baseDamage, target, caster);
    
    }*/

    private void Start()
    {
        /*if (manager.AttackEntity == null)
            manager.AttackEntity = new UnityEvent<Technique, Entity, Entity>();
        manager.AttackEntity.AddListener(AttackTarget);
        2*/
        if (manager.UseTechnique == null)
            manager.UseTechnique = new UnityEvent<Assignment, Technique, Entity, Entity>();
        manager.UseTechnique.AddListener(AttackFrom);
        if (manager.GetDamageValue == null)
            manager.GetDamageValue = new UnityEvent<Technique, Entity, Entity>();
        manager.GetDamageValue.AddListener(DamageTest);
    }
      // this function assumes that the function that calls it checks mana.

    public void AttackFrom(Assignment input, Technique technique, Entity target, Entity caster)
    {
        Debug.Log("Current caster: " + caster);
        Debug.Log("Current target input: " + input);
        int damage = AttackTarget(technique, caster, target);
        manager.GetToEntity.Invoke(input, damage, technique);
    }

    public void DamageTest(Technique technique, Entity caster, Entity target)
    {
        int damage = AttackTarget(technique, caster, target);
        manager.ReturnDamageValue.Invoke(damage);
    }
    public int AttackTarget(Technique technique, Entity caster, Entity target)
    {
        int baseDamage = 0;
        int manaReduction = 0;
        int totalDamage = 0;
        if (caster.playableCharacter == false)
        {
            manaReduction = technique.manaCost - 1;
        }
        else
        {
            manaReduction = technique.manaCost; //implement treasure system relating to mana cost later
        }
        for (int i = 1; i <= technique.repeat; i++)
        {
            baseDamage = 0;
            if (technique.variableDamage == true)
            {
                baseDamage = UnityEngine.Random.Range(technique.damage, technique.lowerDamage);
            }
            else
            {
                baseDamage = technique.damage;
            }
            int armourReduction = target.Armour - caster.ArmourPen;
            if (armourReduction < 0)
            {
                armourReduction = 0;
            }
            baseDamage -= armourReduction;
            totalDamage += baseDamage;
        }
        Debug.Log("Current Damage: " + baseDamage);
        if (caster.playableCharacter == true)
        {
            totalDamage = manager.ProcessDamageTreasures(totalDamage, target, caster);
        }
        double damagedouble = Convert.ToDouble(totalDamage);
        damagedouble *= AffinityTest(target, technique.damageType);
        
        return Convert.ToInt32(damagedouble);


    }


    public double AffinityTest(Entity target, DamageType type)
    {   
        double mod = 1;
        if (type == DamageType.SLASHING)
        {
        mod = target.slashing;
        }
        if (type == DamageType.PIERCING)
        {
        mod = target.piercing;
        }
        if (type == DamageType.BLUDGEONING)
        {
        mod = target.bludgeoning;
        }
        if (type == DamageType.FIRE)
        {
        mod = target.fire;
        }
        if (type == DamageType.COLD)
        {
        mod = target.cold;
        }
        if (type == DamageType.FORCE)
        {
        mod = target.force;
        }
        if (type == DamageType.HOLY)
        {
        mod = target.holy;
        }
        if (type == DamageType.EVIL)
        {
        mod = target.evil;
        }
        if (type == DamageType.ALMIGHTY)
        {
        mod = 1;
        }
        return mod;
    }


    

 
    
}
