using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hand_System : MonoBehaviour
{
    public List<GameObject> Cartas;
    public float distanciaMano = 0f;
    public float distanciaEntreCartas = 0f;
    public GameObject StartHandPosition;
    public GameObject EndHandPosition;
    public GameObject base_card_prefab;

    public Vector3 cardSpawnAnlge;
    public Vector3 cardExtraPosition;

    private void Start()
    {
        Game_System.SetSingletone(this);
    }

    public void instanciarCartas()
    {

    }

    public void instanciarCarta(card_data card_Data)
    {
        //creo la instancia
        GameObject newCard = Instantiate(base_card_prefab, StartHandPosition.transform.position + cardExtraPosition, Quaternion.Euler(cardSpawnAnlge), this.transform);
        Cartas.Add(newCard);

        //obtengo el renderer y lo configuro
        Card_Renderer newCard_renderer = newCard.GetComponent<Card_Renderer>();
        newCard_renderer.CardToRender = card_Data;
        Game_System.instance.deck.AgregarCartaMano(card_Data);
        newCard_renderer.update_card_renderer();

        //calculo distancia entre cartas
        CalcularDistancias();

        //austo las cartas
        AjustarPosicinCartas();
    }

    public void CalcularDistancias()
    {
        //Debug.Log("Calculo Distancia");
        //calculo distancia entre cartas
        distanciaMano = Vector3.Distance(StartHandPosition.transform.position, EndHandPosition.transform.position);
        distanciaEntreCartas = distanciaMano / (Cartas.Count + 1);
    }
    public void AjustarPosicinCartas()
    {
        //Debug.Log("Ajusto posiciones cartas");

        for (int i = 0; i < Cartas.Count; i++){
            Vector3 originalPosition = Cartas[i].transform.position;
            float StartPosX = StartHandPosition.transform.position.x;

            Cartas[i].transform.position = new Vector3(
                StartPosX + distanciaEntreCartas * (i+1),
                originalPosition.y,
                originalPosition.z);
        }
    }

    public void ActivarCarta(card_data card_Data, int number)
    {
        for (int i = 0; i < Cartas.Count; i++)
        {
            Card_Renderer i_cardRenderer = Cartas[i].GetComponent<Card_Renderer>();
            if(i_cardRenderer.CardToRender == card_Data)
            {
                i_cardRenderer.ActivateCard(true, number);
                break;
            }
        }
    }
}
