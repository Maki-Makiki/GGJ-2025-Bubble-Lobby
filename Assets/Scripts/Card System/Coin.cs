using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "Bubble Lobby/System/Coin Data")]
public class Coin : ScriptableObject
{
    public string displayName;
    [TextArea()]
    public string description;

    public Sprite icon;
    public Color color;
}
