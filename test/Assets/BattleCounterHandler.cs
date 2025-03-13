using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BattleCounterHandler : MonoBehaviour
{
    private manager manager;
    public Entity player;
    public TMP_Text counterMoney, consumableLimit, treasureLimit, HPText, MPText, DiscardText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (manager.moneyUpdated == null)
            manager.moneyUpdated = new Event1();

        manager.moneyUpdated.AddListener(UpdateCounter);

        if (manager.NewConsumable == null)
         manager.NewConsumable = new UnityEvent();
        manager.NewConsumable.AddListener(UpdateConsumableLimit); 

        if (manager.NewTreasure == null)
         manager.NewTreasure = new UnityEvent();
        manager.NewTreasure.AddListener(UpdateTreasureLimit); 

        if (manager.LimitUpdate == null)
         manager.LimitUpdate = new UnityEvent();
        manager.LimitUpdate.AddListener(UpdateTreasureLimit);
        manager.LimitUpdate.AddListener(UpdateConsumableLimit); 

        if (manager.PlayerDamage == null)
            manager.PlayerDamage = new UnityEvent<int, int>();
        manager.PlayerDamage.AddListener(UpdatePlayerStats);

        if (manager.UpdateDiscardCounter == null)
            manager.UpdateDiscardCounter = new UnityEvent();
        manager.UpdateDiscardCounter.AddListener(UpdateDiscards);

        UpdateCounter(manager.money);
        UpdateConsumableLimit();
        UpdatePlayerStats(10,10);
        UpdateTreasureLimit();
        UpdateDiscards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounter(int amount)
    {
        counterMoney.text = amount.ToString();
    }

    public void UpdateConsumableLimit()
    {
        manager.consumableCount = manager.consumables.Count;
        consumableLimit.text = manager.consumableCount.ToString() + " / " + manager.consumableMax.ToString();
    }

    public void UpdateTreasureLimit()
    {
        manager.treasureCount = manager.treasures.Count;
        treasureLimit.text = manager.treasureCount.ToString() + " / " + manager.treasureMax.ToString();
    }

    public void UpdatePlayerStats(int Health, int Mana)
    {
        HPText.text = Health.ToString() + " / " + player.MaxHealth.ToString();
        MPText.text = Mana.ToString() + " / " + player.MaxMana.ToString();
    }

    public void UpdateDiscards()
    {
        DiscardText.text = manager.discards.ToString();
    }
}
