using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Show_Help : MonoBehaviour
{
    public GameObject help;
    public TMP_Text textoHelp;

    public Vector3 coordenadasExtra;

    private void Start()
    {
        Game_System.SetSingletone(this);
    }

    public void ShowHelpNow(card_data cardToShow, Card_Renderer rendererCard)
    {
        Vector3 cordenadasCarta = rendererCard.gameObject.transform.position + coordenadasExtra;
        Vector2 sp = Camera.main.WorldToScreenPoint(cordenadasCarta);
        help.transform.position = sp;   

        string textoAMostrar = "";

        string tipo = cardToShow.cardType == CardType.number ? "Recurso" : cardToShow.cardType == CardType.effect ? "Efecto" : "Efecto Instantaneo";

        textoAMostrar += "<b><size=120%>" + cardToShow.displayName + "<size=120%></b> <size=80%>(" + cardToShow.cardType.ToString() + ")\n";
        textoAMostrar += "<size=100%>" + cardToShow.description + "<size=20%>\n";

        for (int i = 0; i < cardToShow.card_Effects.Count; i++)
        {
            textoAMostrar += "\n<size=20%><size=100%><b>" + (cardToShow.card_Effects[i].description) + "</b>";
        }

        help.SetActive(true);
        textoHelp.text = textoAMostrar;
    }

    public void HideHelpNow()
    {
        help.SetActive(false);
        string textoAMostrar = "";
        textoHelp.text = textoAMostrar;
    }
}
