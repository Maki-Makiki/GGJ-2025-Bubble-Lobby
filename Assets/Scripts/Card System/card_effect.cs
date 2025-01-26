using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Effect", menuName = "Bubble Lobby/Cards/Card Effect Data")]
public class card_effect : ScriptableObject
{
    public string displayName;
    [TextArea()]
    public string description;

    public List<int> MultyValue;
    public List<int> AfectedNumbers;
    public List<Coin> AfectedCoins;

    public Sprite icon;
}
