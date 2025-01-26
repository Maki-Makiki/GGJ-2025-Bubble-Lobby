using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    internal void SelectCardHand(card_data card_Data, Card_Renderer card_Renderer)
    {
        if (selected_cards.IndexOf(card_Data) == -1)
        {

            switch (card_Data.cardType)
            {
                case CardType.quick_effect:
                    Debug.Log("Carta quick effect activada");

                    ActivateQuickEffect(card_Data, card_Renderer);

                    break;

                case CardType.effect:
                    Debug.Log("Carta effect seleccionada");
                    if (selected_cards.Count == 0)
                    {
                        Debug.Log("Error = para seleccionar un efecto tienes que seleccionar numeros antes");
                        break;
                    }
                    else
                    {
                        if (selected_cards.Last().cardType == CardType.number)
                        {
                            ActivarSeleccion(card_Data);
                        }
                        else
                        {
                            Debug.Log("Error = para seleccionar un efecto tienes que seleccionar numeros antes");
                        }
                        
                    }
                    break;


                case CardType.number:
                    Debug.Log("Carta Number seleccionada");

                    if(selected_cards.Count == 0)
                    {
                        ActivarSeleccion(card_Data);
                    }
                    else
                    {
                        if (selected_cards.Last().cardType == CardType.number)
                        {
                            card_data lastCard = selected_cards.Last();

                            bool MismoTipoCoin = lastCard.card_Effects[0].AfectedCoins[0] == card_Data.card_Effects[0].AfectedCoins[0];

                            int numeroAnterior = lastCard.card_Effects[0].MultyValue[0];
                            int numeroActual = card_Data.card_Effects[0].MultyValue[0];
                            bool Consecutivas = Math.Abs(numeroAnterior - numeroActual) <2;
                            
                            bool MismoNumero = lastCard.card_Effects[0].MultyValue[0] == card_Data.card_Effects[0].MultyValue[0];

                            if (MismoNumero)
                            {
                                ActivarSeleccion(card_Data);
                            }
                            else
                            {
                                if (MismoTipoCoin)
                                {
                                    Debug.Log("Esta carta es del mismo tipo de coin que la anterior");

                                    if (Consecutivas)
                                    {
                                        Debug.Log(lastCard.name + " es consecutivo de " + card_Data.name);
                                        ActivarSeleccion(card_Data);
                                    }
                                }
                            }

                            
                        }
                        else
                        {
                            Debug.Log("Error = Como mierda hiciste para seleccionar un numero luego de algo que no es un numero!!!");
                        }
                    }

                    
                    break;
            }
        }
        else
        {
            if (selected_cards.Last() == card_Data)
            {
                Debug.Log("se selecciono la ultima");
                UnselectCardHand(card_Data);
            }
            else
            {
                Debug.Log("ERROR: Solo se puede des-seleccionar la ultima carta");
            }
        }

    }

    private void ActivateQuickEffect(card_data card_Data, Card_Renderer card_Renderer)
    {

        Game_System.instance.deck.used_deck_cards.Add(card_Data);
        Game_System.instance.deck.hand_cards.Remove(card_Data);

        Game_System.instance.hand.Cartas.Remove(card_Renderer.gameObject);
        Destroy(card_Renderer.gameObject);

        Game_System.instance.hand.CalcularDistancias();
        Game_System.instance.hand.AjustCardPos();
    }

    private void ActivarSeleccion(card_data card_Data)
    {
        selected_cards.Add(card_Data);
        Game_System.instance.hand.ActivarCarta(true, card_Data, selected_cards.Count);
    }


    internal void UnselectCardHand(card_data card_Data)
    {
        selected_cards.Remove(card_Data);
        Game_System.instance.hand.ActivarCarta(false, card_Data, selected_cards.Count);
    }

    private void Start()
    {
        Game_System.SetSingletone(this);
    }


}
