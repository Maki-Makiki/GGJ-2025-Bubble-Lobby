using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_System : MonoBehaviour
{
    [Tooltip("cartas que se pueden robar")]
    public List<card_data> deck_cards;

    [Tooltip("cartas ya usadas")]
    public List<card_data> used_deck_cards;

    [Tooltip("cartas en la mano")]
    public List<card_data> hand_cards;

    [Tooltip("cartas que se tienen seleccionadas")]
    public List<card_data> selected_cards;

    internal void AgregarCartaMano(card_data card_Data)
    {
        hand_cards.Add(card_Data);
    }

    internal void SelectCardHand(card_data card_Data)
    {
        if (selected_cards.IndexOf(card_Data) == -1)
        {
            selected_cards.Add(card_Data);

            //activar animacion
            //activar numerito y poner el numero correcto
            Game_System.instance.hand.ActivarCarta(card_Data, selected_cards.Count);
        }
    }

    internal void UnselectCardHand(card_data card_Data)
    {
        if (selected_cards.IndexOf(card_Data) == -1)
        {
            selected_cards.Add(card_Data);
        }
    }

    private void Start()
    {
        Game_System.SetSingletone(this);
    }


}
