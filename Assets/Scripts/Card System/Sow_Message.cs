using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sow_Message : MonoBehaviour
{
    public TMP_Text text;

    public float timer = 0;
    public float timerToTranition = 0.5f;
    public float porcentaje = 0f;

    public bool animate = false;

    public Vector3 originalPos;
    public Vector3 objetivePos = new Vector3(0,2,0);

    public Color colorOriginal;
    public Color colorFinal = new Color(1,1,1,0);

    private void Start()
    {
        originalPos = transform.position;
    }

    public void showTextAnim(string Texto)
    {
        text.text = Texto; 
        animate = true;
        
    }
    private void Update()
    {
        if (animate)
        {
            if (timer < timerToTranition)
            {
                timer += Time.deltaTime;
                porcentaje = timer / timerToTranition;

                this.transform.localPosition = Vector3.Lerp(originalPos, originalPos + objetivePos, porcentaje);
                text.color = Color.Lerp(colorOriginal, colorFinal, porcentaje);

            }
            else
            {
                timer = timerToTranition;
                Destroy(this.gameObject);
            }

        }
    }
}
