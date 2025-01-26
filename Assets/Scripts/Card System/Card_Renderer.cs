using System;
using TMPro;
using UnityEngine;

public class Card_Renderer : MonoBehaviour
{
    public card_data CardToRender;

    [Space]
    public Renderer card_renderer;
    public Renderer card_border_renderer;
    public Renderer card_select_effect_renderer;

    [Space(20f)]
    public Vector3 original_pos;


    [Space(20f)]
    public GameObject card_s_content;
    public bool bool_s = false;
    public Vector3 pos_max_s = new Vector3(0, 0.15f, 0);
    public float animationTime_s = 0.15f;
    public float actualTime_s = 0f;

    [Space(20f)]
    public GameObject card_a_content;
    public bool bool_a = false;
    public Vector3 pos_max_a = new Vector3(0,0, -0.15f);
    public float animationTime_a = 0.15f;
    public float actualTime_a = 0f;

    [Space(20f)]
    public bool actualizar;

    [Space(20f)]
    public GameObject canvas_number;
    public TMP_Text text_number;

    public void Start()
    {
        //Game_System.SetSingletone(this);
        Select(false);
        original_pos = card_s_content.transform.localPosition;
    }

    public void Update()
    {
        if (actualizar)
        {
            actualizar = false;
            update_card_renderer();
        }

        AnimarHover();
        AnimarSelected();

    }

    

    public void CheckActivable()
    {
        Game_System.instance.deck.SelectCardHand(CardToRender, this);
        //bool_a = !bool_a;
    }

    public void ActivateCard(bool activar, int number)
    {
        bool_a = activar;
        canvas_number.SetActive(activar);
        text_number.text = number.ToString();
    }

    private void AnimarSelected()
    {
        if (bool_a)
        {
            if (actualTime_a < animationTime_a)
            {
                actualTime_a += Time.deltaTime;
            }
            else
            {
                if (actualTime_a > animationTime_a)
                {
                    actualTime_a = animationTime_a;
                }
            }
        }
        else
        {
            if (actualTime_a > 0)
            {
                actualTime_a -= Time.deltaTime;
            }
            else
            {
                if (actualTime_a < 0)
                {
                    actualTime_a = 0;
                }
            }
        }

        float porcentaje_a = actualTime_a / animationTime_a;
        Vector3 content_final_a_pos = new Vector3(
            original_pos.x + pos_max_a.x,
            original_pos.y + pos_max_a.y,
            original_pos.z + pos_max_a.z);

        card_a_content.transform.localPosition = Vector3.Lerp(
            original_pos, content_final_a_pos, porcentaje_a);
    }

    private void AnimarHover()
    {
        if (bool_s)
        {
            if (actualTime_s < animationTime_s) 
            {
                actualTime_s += Time.deltaTime;
            }
            else
            {
                if (actualTime_s > animationTime_s)
                {
                    actualTime_s = animationTime_s;
                }
            }
        }
        else
        {
            if (actualTime_s > 0)
            {
                actualTime_s -= Time.deltaTime;
            }
            else
            {
                if (actualTime_s < 0)
                {
                    actualTime_s = 0;
                }
            }
        }

        float porcentaje_s = actualTime_s / animationTime_s;
        Vector3 content_final_s_pos = new Vector3(
            original_pos.x + pos_max_s.x, 
            original_pos.y + pos_max_s.y, 
            original_pos.z + pos_max_s.z);

        card_s_content.transform.localPosition = Vector3.Lerp(
            original_pos, content_final_s_pos, porcentaje_s);
    }

    public void update_card_renderer()
    {
        card_renderer.material.SetTexture("_Carta", CardToRender.card_Image);
        card_border_renderer.enabled = CardToRender.cardType == CardType.quick_effect;
    }

    public void Select(bool v)
    {
        //Debug.Log(" Select(" + v + ")");
        card_select_effect_renderer.enabled = v;
        bool_s = v;
        if(v == false)
        {
            if(Game_System.instance.hand.hoveredCardRenderer == this)
            {
                Game_System.instance.hand.hoveredCardRenderer = null;
            }
        }
        else
        {
            Game_System.instance.hand.hoveredCardRenderer = this;
        }
       
    }
}
