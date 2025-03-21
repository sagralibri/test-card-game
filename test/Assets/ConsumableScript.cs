using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsumableScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private manager manager;
    public TMP_Text explanation;
    public TMP_Text title;
    public TMP_Text rarity;
    public Technique usedConsumable;
    public GameObject ImageOnPanel;
    public GameObject Info;
    public GameObject Outline;
    Texture NewTexture;
	private RawImage img;
    private bool mouse_over = false;
    public bool selected = false;

    void Start()
    {
        Outline.SetActive(false);
        NewTexture = usedConsumable.cardImage;
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
	
		img.texture = (Texture)NewTexture;
        rarity.text = usedConsumable.rarity + " ";
        title.text = usedConsumable.name + " ";
        Info.SetActive(false);

        if (manager.DeselectAll == null)
            manager.DeselectAll = new UnityEvent();
        manager.DeselectAll.AddListener(Deselect); 
        Debug.Log("Start Functional");
    }

    void Update()
    {
        if (mouse_over)
        {
		    explanation.text = GetFuncText();
            Info.SetActive(true);
            if(Input.GetMouseButtonDown(0) == true)
            {
                if (selected == false)
                {
                    manager.DeselectAll.Invoke();
                    Debug.Log("true");
                    selected = true;
                }
                else if (selected == true)
                {
                    Debug.Log("false");
                    selected = false;
                }
            }
        }
        else
        {
            explanation.text = "";
            Info.SetActive(false);
        }
        if (selected == true)
        {
            Outline.SetActive(true);
        }
        else
        {
            Outline.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("YOU WIN");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
    public void Deselect()
    {
        selected = false;
    }


    string GetFuncText()
    {
        string explanationtext = "";
        string targetText = "";
        if (usedConsumable.description != "")
        {
            explanationtext = usedConsumable.description;
        }
        else
        {
            switch (usedConsumable.targeting)
            {
                case (Target.SELF):
                    targetText = " to yourself";
                    break;
                case (Target.ONEENEMY):
                    targetText = " to one enemy";
                    break;
                case (Target.ALLENEMIES):
                    targetText = " to all enemies";
                    break;
                case (Target.ONEALLY):
                    targetText = " to one ally";
                    break;
                case (Target.ALLALLIES):
                    targetText = " to all allies";
                    break;
                case (Target.ALL):
                    targetText = " to all creatures";
                    break;
            }

            if (usedConsumable.variableDamage == true)
            {
                explanationtext = "Deal " + usedConsumable.damage + " to " + usedConsumable.lowerDamage + " " + usedConsumable.damageType + targetText;
            }
            else if (usedConsumable.damage != 0)
            {
                explanationtext = "Deal " + usedConsumable.damage + " " + usedConsumable.damageType + targetText;
            }

            if (usedConsumable.repeat > 1)
            {
                explanationtext = explanationtext + " " + usedConsumable.repeat + " times";
            }
            if (usedConsumable.healingFlat > 0 || usedConsumable.healingPercent > 0)
            {
                
            }
        }
        return explanationtext;
    }
}
