using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards_System : MonoBehaviour
{

    [Tooltip("mazo de cartas por defecto")]
    public List<card_data> numbered_cards;

    [Tooltip("mazo de cartas por defecto")]
    public List<card_data> default_deck;

    [Tooltip("cartas que se tienen seleccionadas como deck ahora mismo")]
    public List<card_data> actual_deck;

    [Tooltip("cartas desbloqueadas")]
    public List<card_data> unlocked_cards;

    [Tooltip("cartas desbloqueables")]
    public List<card_data> locked_cards;

    private void Start()
    {
        Game_System.SetSingletone(this);
    }

}
