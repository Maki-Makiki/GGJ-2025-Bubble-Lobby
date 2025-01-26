using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Subtype", menuName = "Bubble Lobby/Cards/Card Subtype Data")]
public class card_subtype : ScriptableObject
{
    public string displayName;
    [TextArea()]
    public string description;

    public Sprite icon;

    public string GetDescriptionString()
    {
        return description.Replace("[name]", displayName);
    }
}
