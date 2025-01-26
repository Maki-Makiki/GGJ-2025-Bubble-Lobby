using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Instant Effect", menuName = "Bubble Lobby/Cards/Card Instant Effect Data")]
public class card_instant_effect : ScriptableObject
{
    public instantEffectType instantEffectType;
}

public enum instantEffectType
{
    SacrificeCard,
    GiveCard,
    ChangeCard,
}