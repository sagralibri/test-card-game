using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class counter : MonoBehaviour
{
    private manager manager;
    public Entity player;
    public TMP_Text counterMoney, counterRange, counterLast, counterRoll, consumableLimit, treasureLimit, HPText, MPText, shieldText, armourText, enemyTotal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (manager.moneyUpdated == null)
            manager.moneyUpdated = new UnityEvent();

        manager.moneyUpdated.AddListener(UpdateCounter);

        if (manager.ModUpdated == null)
            manager.ModUpdated = new UnityEvent();
        manager.ModUpdated.AddListener(UpdateRange);

        if (manager.RollUsed == null)
         manager.RollUsed = new Event1();
        manager.RollUsed.AddListener(UpdateLastCashout); 

        // these are placeholders until the treasure system is further developed, these will be replaced with Event1(); that invoke functions that call their respective functions
        // to update the text
        if (manager.NewConsumable == null)
         manager.NewConsumable = new UnityEvent();
        manager.NewConsumable.AddListener(UpdateConsumableLimit); 

        if (manager.NewTreasure == null)
         manager.NewTreasure = new UnityEvent();
        manager.NewTreasure.AddListener(UpdateTreasureLimit); 
        // end of placeholders

        if (manager.LimitUpdate == null)
         manager.LimitUpdate = new UnityEvent();
        manager.LimitUpdate.AddListener(UpdateTreasureLimit);
        manager.LimitUpdate.AddListener(UpdateConsumableLimit); 

        // this will be replaced with an event that is called by a function that calls NextStage to assure that the updated enemy HP is accurate.
        if (manager.NextStage == null)
         manager.NextStage = new UnityEvent();
        manager.NextStage.AddListener(UpdateEnemyTotalHP);


        UpdateRange();
        UpdateCounter();
        UpdateRollCounter();
        UpdateConsumableLimit();
        UpdateTreasureLimit();
        UpdatePlayerStatPanel();
        UpdateEnemyTotalHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounter()
    {
        counterMoney.text = manager.money.ToString();
    }

    public void UpdateRange()
    {
        int lowerBound = 0 + manager.mod1 * manager.mod3;
        int upperBound = 10 + manager.mod2 * manager.mod4;
        string lowerBound2 = lowerBound.ToString();
        string upperBound2 = upperBound.ToString();
        counterRange.text = lowerBound2 + " to " + upperBound2;
    }

    public void UpdateLastCashout(int cashout)
    {
        counterLast.text = cashout.ToString();
        UpdateRollCounter();
    }

    public void UpdateRollCounter()
    {
        counterRoll.text = manager.rolls.ToString();
    }

    public void UpdateConsumableLimit()
    {
        consumableLimit.text = manager.consumables.Count.ToString() + " / " + manager.consumableMax.ToString();
    }

    public void UpdateTreasureLimit()
    {
        treasureLimit.text = manager.treasures.Count.ToString() + " / " + manager.treasureMax.ToString();
    }

    public void UpdatePlayerStatPanel()
    {
        HPText.text = player.MaxHealth.ToString() + " / " + player.Health.ToString();
        MPText.text = player.MaxMana.ToString() + " / " + player.Mana.ToString();
        shieldText.text = player.StartShield.ToString() + " / " + player.Shield.ToString();
        armourText.text = player.Armour.ToString();
    }

    public void UpdateEnemyTotalHP()
    {      
        int totalHP = 0;
        foreach (Entity enemy in manager.enemies)
        {
            totalHP += enemy.Health;
        }
        enemyTotal.text = totalHP.ToString();
    }
    
}
