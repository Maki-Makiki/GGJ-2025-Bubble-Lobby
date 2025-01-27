using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public int Get_cardNumber()
    {
        return card_Effects[0].MultyValue[0];
    }

    public Coin Get_cardNumberCoinType()
    {
        return card_Effects[0].AfectedCoins[0];
    }

    public int Get_Multy_For_Number(int number, Coin coin)
    {
        //Debug.Log("Se va a chequear " + number + " " + coin);
        for (int i = 0; i < card_Effects.Count; i++)
        {
            List<int> multyValueList = card_Effects[i].MultyValue;
            List<int> afectedNumbers = card_Effects[i].AfectedNumbers;
            List<Coin> afectedCoins = card_Effects[i].AfectedCoins;

            for (int j = 0; j < afectedNumbers.Count; j++)
            {
                //Debug.Log("> " + afectedNumbers[j] + " == " +  number +"[es "+ afectedNumbers[j] == number + "]");
                if (afectedNumbers[j] == number || afectedNumbers[j] == -1)
                {
                    if (afectedCoins[j] == coin || afectedCoins[j] == null)
                    {
                        //Debug.Log(" return multyValueList[j] = " + multyValueList[j]);
                        return multyValueList[j];
                    }
                }
            }
        }
        
        return 1;
    }
}

public enum CardType
{
    number,
    effect,
    quick_effect
}

