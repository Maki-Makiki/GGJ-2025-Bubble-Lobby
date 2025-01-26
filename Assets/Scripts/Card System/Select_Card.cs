using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Select_Card : MonoBehaviour
{
    public Card_Renderer card_Renderer;

    private void OnMouseEnter()
    {
        card_Renderer.Select(true);
    }

    private void OnMouseExit()
    {
        card_Renderer.Select(false);
    }
}
