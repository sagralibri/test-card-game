using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{

     
    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton;

    
    void Start()
    {
        m_YourFirstButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        manager.RollMachine();
    }


}
