using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;


public class Event1 : UnityEvent<int>
{

}

public enum TechniqueType
{
    DAMAGE,
    HEALING,
    UTILITY
}

public enum DamageType
{
    SLASHING,
    PIERCING,
    BLUDGEONING,
    FIRE,
    COLD,
    FORCE,
    LIGHTNING,
    HOLY,
    EVIL,
    ALMIGHTY
}

public enum Affinity
{
    NONE,
    ABSORB,
    REFLECT,
    IMMUNE,
    RESIST,
    WEAK
}

public enum Target
{
    SELF,
    ONEENEMY,
    ALLENEMIES,
    ONEALLY,
    ALLALLIES,
    ALL
}

public enum Rarity
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}
public enum Turn 
{
    ALLY,
    ENEMY,
    OTHER
}

public enum Assignment
{
    PLAYER,
    A1,
    A2,
    A3,
    E1,
    E2,
    E3,
    E4
}

public enum AIType
{
    RANDOM,
    SMART
}
public class manager
{
    // events
    public static Event1 moneyUpdated;
    public static UnityEvent ModUpdated;
    public static Event1 RollUsed;
    public static UnityEvent NewTreasure;
    public static UnityEvent NewConsumable;
    public static UnityEvent LimitUpdate;
    public static UnityEvent NextStage;
    public static UnityEvent GameStart;
    public static UnityEvent PlayerStatChanged;
    public static UnityEvent<int,Entity> DamageTaken;
    public static UnityEvent DeselectAll;
    public static UnityEvent<bool> DiscardSelected;
    public static UnityEvent UpdateDiscardCounter;
    public static UnityEvent NextTurn;
    public static UnityEvent SideChange;
    public static UnityEvent<int, int> PlayerDamage;
    public static UnityEvent<Assignment> ObjectHover;
    public static UnityEvent<Assignment> ObjectUnhover;
    public static UnityEvent<Assignment, Technique, Entity, Entity> UseTechnique;
    public static UnityEvent<Assignment, Entity, bool, Entity> AttackEntity;
    public static UnityEvent<Assignment, int, Technique> GetToEntity;
    public static UnityEvent<Assignment, bool> GetAlignment;
    public static UnityEvent<Assignment> KillEntity;
    public static UnityEvent<Assignment> ProcessAI;
    public static UnityEvent<Entity, bool> GetEntityBH;
    public static UnityEvent<Technique, Entity, Entity> GetDamageValue;
    public static UnityEvent<int> ReturnDamageValue;
    public static UnityEvent<Assignment> GetEntityBH2;
    public static UnityEvent<Entity> ReturnEntityBH2;

    // lists
    public static List<Technique> consumables = new List<Technique>();

    public static List<Treasure> treasures = new List<Treasure>();

    public static List<Entity> enemies = new List<Entity>();
    public static List<Entity> allies = new List<Entity>();

    public static List<Technique> drawnCards = new List<Technique>();

    public static List<Technique> playerDeck = new List<Technique>();
    public static List<Technique> drainableDeck = new List<Technique>();
    public static List<GameObject> drawnCardObjects = new List<GameObject>();
    public static List<GameObject> drawnTreasureObjects = new List<GameObject>();
    public static List<GameObject> drawnConsumableObjects = new List<GameObject>();
    public static List<GameObject> enemyObjects = new List<GameObject>();
    public static List<GameObject> allyObjects = new List<GameObject>();
    public Dictionary<Entity, GameObject> convert = new Dictionary<Entity, GameObject>();

    // var
    public static int currentStage;
    public static int currentLevel;
    public static double difficultyValue = 1;
    public static int money = 0;
    public static int mod1 = 0;
    public static int mod2 = 0;
    public static int mod3 = 1;
    public static int mod4 = 1;
    public static int defaultRolls = 3;
    public static int rolls = defaultRolls;
    public static int consumableCount = consumables.Count;
    public static int consumableMax = 2;
    public static int treasureCount = treasures.Count;
    public static int treasureMax = 5;
    public static int drawnCount = drawnCards.Count;
    public static int drawnMax = 5;
    public static int discardMax = 4;
    public static int discards = discardMax;
    public static bool canAct = true;


    public static void SetDefaultDeck()
    {
        
    }

    public static void ResetDrainableDeck()
    {
        drainableDeck = playerDeck;
    }

    public static void ProceedStage()
    {
        ResetShop();
        rolls = defaultRolls;
        discards = discardMax;
        NextStage.Invoke();
    }

    public static void UpdateMoney(int amount)
    {
        money += amount;
        moneyUpdated.Invoke(money);
    }

    public static void RollMachine()
    {
        if (rolls > 0)
        {
        int output = UnityEngine.Random.Range(1+ mod1 * mod3, 11 + mod2 * mod4);
        UpdateMoney(output);
        rolls -= 1;
        RollUsed.Invoke(output);
        }
        else
        {
        Debug.Log("not enough rolls");
        }
    }

    public static void ChangeMod(int mod1c, int mod2c, int mod3c, int mod4c, int mod1m, int mod2m, int mod3m, int mod4m)
    {
        mod1 += mod1c;
        mod2 += mod2c;
        mod3 += mod3c;
        mod4 += mod4c;
        mod1 *= mod1m + 1;
        mod2 *= mod2m + 1;
        mod3 *= mod3m + 1;
        mod4 *= mod4m + 1;
        ModUpdated.Invoke();
    }

    public static void ResetShop()
    {

    }

    public static void AddConsumable(Technique consumable)
    {
        consumables.Add(consumable);
        NewConsumable.Invoke();
    }

    public static int ProcessDamageTreasures(int input, Entity caster, Entity target)
    {
        // this is a placeholder
        int output;
        output = input;
        return output;
    }


}
