using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Deck_System : MonoBehaviour
{
    public string textoBase = "<size=80%>(faltan [faltan] a�os para que explote la burbuja)<size=100%> \r\n<b> A�o [anio] </b>";
    public TMP_Text textoAnio;
    public int turno = 1994;
    public int turnos = 11;

    public int Puntuacion = 0;

    public int actualNumeredCards = 0;
    public int MaxNumeredCards = 3;

    public int actualEfectCards = 0;
    public int MaxEfectCards = 2;

    public int HandTargetCards = 8;

    public float Turn_Timer = 0;
    public float Turn_TimeForAction = 1f;
    public GameObject PrefabTextoTurn;
    public GameObject SpawnPosition;

    public bool canPlay = true;

    [Tooltip("cartas que se pueden robar")]
    public List<card_data> deck_cards;

    [Tooltip("cartas ya usadas")]
    public List<card_data> used_deck_cards;

    [Tooltip("cartas en la mano")]
    public List<card_data> hand_cards;

    [Tooltip("cartas que se tienen seleccionadas")]
    public List<card_data> selected_cards;

    public void RobarCartas()
    {
        int cartasQueFaltan = HandTargetCards - hand_cards.Count;

        for (int i = 0; i < cartasQueFaltan; i++)
        {
            card_data nuevaCartaRobada = deck_cards.Last();
            hand_cards.Add(nuevaCartaRobada);
            deck_cards.RemoveAt(deck_cards.Count-1);
        }

        Game_System.instance.hand.ResetearCartas3D();
        Game_System.instance.hand.ReinstanciarCartas();

        Game_System.instance.hand.CalcularDistancias();
        Game_System.instance.hand.AjustCardPos();
    }

    public void AvanzarTurno()
    {
        turnos -= 1;
        turno += 1;
        textoBase = textoBase.Replace("[anio]", turno.ToString());
        textoBase = textoBase.Replace("[faltan] ", turnos.ToString());
        textoAnio.text = textoBase;
    }

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
                    Game_System.PlaySound(soundNames.sfx_card_quick);
                    ActivateQuickEffect(card_Data, card_Renderer);

                    break;

                case CardType.effect:
                    Debug.Log("Carta effect seleccionada");
                    if (selected_cards.Count == 0)
                    {
                        //Debug.Log("Error = para seleccionar un efecto tienes que seleccionar numeros antes");
                        ErrorSound("para seleccionar un efecto tienes que seleccionar numeros antes");
                        break;
                    }
                    else
                    {
                        ActivarSeleccion(card_Data);
                        
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
                                    //Debug.Log("Esta carta es del mismo tipo de coin que la anterior");

                                    if (Consecutivas)
                                    {
                                        //Debug.Log(lastCard.name + " es consecutivo de " + card_Data.name);
                                        ActivarSeleccion(card_Data);
                                    }
                                    else
                                    {
                                        //Debug.Log("tienen que ser consecutivas");
                                        ErrorSound("Los n�meros no son consecutivos y del mismo tipo");
                                    }

                                }
                                else
                                {
                                    ErrorSound("Los n�meros no son consecutivos y del mismo tipo ni igual n�mero");
                                }
                            }

                            
                        }
                        else
                        {
                            ErrorSound("No se puede seleccionar numeros luego de seleccionar Cartas de Efecto");
                            //Debug.Log("Error = Como mierda hiciste para seleccionar un numero luego de algo que no es un numero!!!");
                        }
                    }

                    
                    break;
            }
        }
        else
        {
            if (selected_cards.Last() == card_Data)
            {
                //Debug.Log("se selecciono la ultima");
                UnselectCardHand(card_Data);
            }
            else
            {
                ErrorSound("Solo se puede des-seleccionar la ultima carta");
                //Debug.Log("ERROR: Solo se puede des-seleccionar la ultima carta");
            }
        }

    }

    public void DestroyCardsSelected()
    {
        actualNumeredCards = 0;
        actualEfectCards = 0;
        Game_System.instance.hand.desactivarTodasLasCartas();

        foreach (var card in selected_cards) 
        {
            Game_System.instance.deck.used_deck_cards.Add(card);
            Game_System.instance.deck.hand_cards.Remove(card);
        }

        selected_cards = new List<card_data>();

        Game_System.instance.hand.ResetearCartas3D();
        Game_System.instance.hand.ReinstanciarCartas();

        Game_System.instance.hand.CalcularDistancias();
        Game_System.instance.hand.AjustCardPos();

        AvanzarTurno();
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
        if(card_Data.cardType == CardType.number)
        {
            if (actualNumeredCards >= MaxNumeredCards)
            {
                ErrorSound("No se pueden marcar m�s de " + MaxNumeredCards + " Cartas numeradas");
                return;
            }
            else
            {
                actualNumeredCards++;
            }
        }

        if (card_Data.cardType == CardType.effect)
        {
            if (actualEfectCards >= MaxEfectCards)
            {
                ErrorSound("No se pueden marcar m�s de " + MaxEfectCards + " Cartas de efecto");
                return;
            }
            else
            {
                actualEfectCards++;
            }
        }

        selected_cards.Add(card_Data);
        Game_System.instance.hand.ActivarCarta(true, card_Data, selected_cards.Count);
        Game_System.PlaySound(soundNames.sfx_card_select);
    }

    internal void UnselectCardHand(card_data card_Data)
    {
        if (card_Data.cardType == CardType.number)
        {
                actualNumeredCards--;
        }

        if (card_Data.cardType == CardType.effect)
        {
                actualEfectCards--;
        }

        selected_cards.Remove(card_Data);
        Game_System.instance.hand.ActivarCarta(false, card_Data, selected_cards.Count);
        Game_System.PlaySound(soundNames.sfx_card_select);
    }

    private void Start()
    {
        Game_System.SetSingletone(this);

        deck_cards.Shuffle();
        AvanzarTurno();
        RobarCartas();
    }

    public void ErrorSound(string errorLog)
    {
        Debug.Log("Jugada Invalida" + errorLog);
        Game_System.PlaySound(soundNames.sfx_card_error);
    }

    public void Update()
    {
        
    }

    public void RobarUnaCarta()
    {
        card_data cartaDelMazo = deck_cards.Last();
        deck_cards.Remove(cartaDelMazo);
        Game_System.instance.hand.instanciarCarta(cartaDelMazo);
    }


    IEnumerator ExecuteTurn()
    {
        canPlay = false;
        List<card_data> ListaNum = listaDeCartasTypo(selected_cards, CardType.number);
        List<card_data> ListaEff = listaDeCartasTypo(selected_cards, CardType.effect);

        List<List<Card_Renderer>> listaDeCombos = new List<List<Card_Renderer>>();

        for (int i = 0; i < ListaNum.Count; i++)
        {
            List<int> Calculos = new List<int>();
            Coin ListNumCoinType = ListaNum[i].Get_cardNumberCoinType();
            int ListNumValue = ListaNum[i].Get_cardNumber();

            //List<Card_Renderer> cartas = new List<Card_Renderer>();
            //cartas.Add(ListaNum.)

            for (int j = 0; j < ListaEff.Count; j++)
            {
                Calculos.Add(ListaEff[j].Get_Multy_For_Number(ListNumValue, ListNumCoinType));
                
                //listaDeCombos.Add(cartas);
            }

            int restult = ListNumValue;
            string message = ListNumValue + " " + ListNumCoinType.displayName + " Coin ";

            for (int k = 0; k < Calculos.Count; k++)
            {
                restult *= Calculos[k];
                message = message + " x " + Calculos[k];
            }

            if (Calculos.Count < 1)
            {
                message = ListNumValue + " " + ListNumCoinType.displayName + " Coin " + " = " + ListNumValue;
                Puntuacion = ListNumValue;
                ShowMessageTurn(message);
            }
            else
            {
                message = message + " = " + restult;
                Puntuacion = restult;
                ShowMessageTurn(message);
            }

            yield return new WaitForSeconds(2);
        }

        DestroyCardsSelected();
        canPlay = true;

    }

    public void IniciarTurno()
    {
        StartCoroutine(ExecuteTurn());
    }

    public void JugarMano()
    {
        
        List<card_data> ListaNum = listaDeCartasTypo(selected_cards, CardType.number);
        List<card_data> ListaEff = listaDeCartasTypo(selected_cards, CardType.effect);

        for (int i = 0; i < ListaNum.Count; i++)
        {
            List<int> Calculos = new List<int>();
            Coin ListNumCoinType = ListaNum[i].Get_cardNumberCoinType();
            int ListNumValue = ListaNum[i].Get_cardNumber();

            for (int j = 0; j < ListaEff.Count; j++)
            {
                Calculos.Add(ListaEff[j].Get_Multy_For_Number(ListNumValue, ListNumCoinType));
            }

            int restult = ListNumValue;
            string message = ListNumValue + " " + ListNumCoinType.displayName + " Coin "; 

            for (int k= 0; k < Calculos.Count; k++)
            {
                restult *= Calculos[k];
                message = message + " x " + Calculos[k];
            }

            if(Calculos.Count < 1)
            {
                message = ListNumValue + " " + ListNumCoinType.displayName + " Coin " + " = " + ListNumValue;
                Puntuacion = ListNumValue;
                ShowMessageTurn(message);
            }
            else
            {
                message = message + " = " + restult;
                Puntuacion = restult;
                ShowMessageTurn(message);
            }
           
        }
    }

    public List<card_data> listaDeCartasTypo(List<card_data> cards, CardType cardtype)
    {
        List<card_data> listaResultado = new List<card_data>();

        for (int i = 0;i < cards.Count; i++)
        {
            if (cards[i].cardType == cardtype)
            {
                listaResultado.Add(cards[i]);
            }
        }

        return listaResultado;
    }

    public void BarajarMazo()
    {
        deck_cards.Shuffle();
    }

    public void ShowMessageTurn(string message)
    {
        Debug.Log(message);
        GameObject newMessage = Instantiate(PrefabTextoTurn, SpawnPosition.transform.position, Quaternion.identity);
        newMessage.GetComponent<Sow_Message>().showTextAnim(message);
    }
}
public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}