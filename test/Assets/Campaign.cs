using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Campaign", menuName = "Scriptable Objects/Campaign")]
public class Campaign : ScriptableObject
{
    public List<Stage> Stages = new List<Stage>(); // maybe dictionary?
}
