using UnityEngine;

[CreateAssetMenu(fileName = "Treasure", menuName = "Scriptable Objects/Treasure")]
public class Treasure : ScriptableObject
{
    public string name;
    public Rarity rarity;
    public Texture treasureImage;
    public string description;
}
