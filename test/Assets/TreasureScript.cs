using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private manager manager;
    public TMP_Text explanation;
    public TMP_Text title;
    public TMP_Text rarity;
    public Treasure usedTreasure;
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
        NewTexture = usedTreasure.treasureImage;
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
	
		img.texture = (Texture)NewTexture;
        rarity.text = usedTreasure.rarity + " ";
        title.text = usedTreasure.name + " ";
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
        return usedTreasure.description;
    }
}

/*     public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("mouse exit");
    }
    */
