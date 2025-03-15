using UnityEngine;

[CreateAssetMenu(fileName = "Treasure", menuName = "Scriptable Objects/Treasure")]
public class Treasure : ScriptableObject
{
    public string name;
    public int cost;
    public Rarity rarity;
    public Texture treasureImage;
    public string description;
    public bool shoppable;
}
