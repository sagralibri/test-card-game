/* using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Knives : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    private manager manager;
    public TMP_Text explanation;
    public TMP_Text title;
    public TMP_Text manaCost;
    public TMP_Text rarity;
    public Technique usedTechnique;
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
        NewTexture = usedTechnique.cardImage;
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
	
		img.texture = (Texture)NewTexture;
        manaCost.text = usedTechnique.manaCost + " ";
        rarity.text = usedTechnique.rarity + " ";
        title.text = usedTechnique.name + " ";
        Info.SetActive(false);

        if (manager.DeselectAll == null)
            manager.DeselectAll = new UnityEvent();
        manager.DeselectAll.AddListener(Deselect); 
    }

    void OnMouseDown()
    {
        Debug.Log("you clicked!");
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

    public void Deselect()
    {
        selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    string GetFuncText()
    {
        string explanationtext = "";
        string targetText = "";
        switch (usedTechnique.targeting)
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

        if (usedTechnique.variableDamage == true)
        {
            explanationtext = "Deal " + usedTechnique.damage + " to " + usedTechnique.lowerDamage + " " + usedTechnique.damageType + targetText;
        }
        else if (usedTechnique.damage != 0)
        {
            explanationtext = "Deal " + usedTechnique.damage + " " + usedTechnique.damageType + targetText;
        }

        if (usedTechnique.repeat > 1)
        {
            explanationtext = explanationtext + " " + usedTechnique.repeat + " times";
        }
        if (usedTechnique.healingFlat > 0 || usedTechnique.healingPercent > 0)
        {
            
        }
        if (usedTechnique.description != "")
        {
            explanationtext = usedTechnique.description;
        }
        return explanationtext;
    }
}
*/
