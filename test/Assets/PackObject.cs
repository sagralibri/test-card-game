using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PackObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text cost;
    public Texture newText;
    public GameObject ImageOnPanel;
    public GameObject thisObject;
    public int costMoney;
    public bool bigPack;
    public PackType shopType;
    public bool isBigShop = false;
    private bool mouse_over;
    public bool chosenOne;
    private RawImage img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = (RawImage)ImageOnPanel.GetComponent<RawImage>();
		img.texture = (Texture)newText;
        cost.text = costMoney + " ";

        if (manager.InitializeShop == null)
        {
            manager.InitializeShop = new UnityEvent();
        }
        manager.InitializeShop.AddListener(ReturnToShop);

    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_over)
        {
            if(Input.GetMouseButtonDown(0) == true)
            {
                chosenOne = true;
                OpenPack();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    void OpenPack()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    void ReturnToShop()
    {
        if (chosenOne == true)
        {
            manager.ReturnShopInfo.Invoke(shopType, bigPack);
            Destroy(thisObject);
            // sayonara
        }
    }
}
