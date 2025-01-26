using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Select_Card : MonoBehaviour
{
    public Card_Renderer card_Renderer;

    private void OnMouseEnter()
    {
        if(Game_System.instance.deck.canPlay)
            card_Renderer.Select(true);
    }

    private void OnMouseExit()
    {
        if (Game_System.instance.deck.canPlay)
            card_Renderer.Select(false);
    }
}
