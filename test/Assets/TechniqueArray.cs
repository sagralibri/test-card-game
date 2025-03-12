using UnityEngine;

public class TechniqueArray : MonoBehaviour
{
    public static GameObject NAC;
    public static GameObject knives;
    // GameObject twinKnives;
    // GameObject cutOff;

    public static GameObject[] techniqueIDtoCard = {
        knives, // 1
        NAC, // 2
        NAC, // 3
        NAC, // 4
        NAC, // 5
        NAC, // 6
        NAC, // 7
        NAC, // 8
        NAC, // 9
        NAC, // 10
        NAC // 11
        };


    public static GameObject GetCardFromID(int ID)
    {
        return techniqueIDtoCard[ID + 1];
    }

}
