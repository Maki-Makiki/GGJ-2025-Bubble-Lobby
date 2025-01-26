using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Bubble Lobby/Cards/Card Data")]
public class card_data : ScriptableObject
{
    public string CardID;
    public string displayName;
    [TextArea()]
    public string description;
    public CardType cardType;
    public List<card_subtype> card_Subtypes;
    public List<card_effect> card_Effects;
    public List<card_instant_effect> card_InstantEffects;

    public Texture2D card_Image;

    public string Get_HelpText()
    {
        return description;
    }
}

public enum CardType
{
    number,
    effect,
    quick_effect
}

