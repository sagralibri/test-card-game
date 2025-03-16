using UnityEngine;

[CreateAssetMenu(fileName = "Technique", menuName = "Scriptable Objects/Technique")]
public class Technique : ScriptableObject
{
    public string namee;
    public int ID;
    public bool consumable;
    public int cost;
    public Texture cardImage;
    public TechniqueType techniqueType;
    public Target targeting;
    public DamageType damageType;
    public int manaCost;
    public bool variableDamage;
    public int damage;
    public int lowerDamage;
    public int repeat;
    public int healingFlat;
    public int healingPercent;
    public int extraEffectID;
    public Rarity rarity;
    public bool shoppable = true;
    public string description;
}
