using UnityEngine;

public class AnimarGrafica : MonoBehaviour
{

    public Renderer thisRenderer;
    [Space]
    public int materialIndex = 0;
    [Space]
    public Vector2 uvAnimationRate = new Vector2(1.0f, 1.0f);
    public Vector2 uvMaxMov = new Vector2(2.0f, 2.0f);

    [Space]
    public string textureName = "_MainTex";
    public Vector2 uvOffset = Vector2.zero;

    private void Start()
    {
        thisRenderer = this.GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);

        if(uvMaxMov.x < 0)
        {
            if (uvOffset.x < uvMaxMov.x)
            {
                uvOffset.x = 0f;
            }
        }
        else
        {
            if (uvOffset.x > uvMaxMov.x)
            {
                uvOffset.x = 0f;
            }
        }



        if (uvMaxMov.y < 0)
        {
            if (uvOffset.y < uvMaxMov.y)
            {
                uvOffset.y = 0f;
            }
        }
        else
        {
            if (uvOffset.y > uvMaxMov.y)
            {
                uvOffset.y = 0f;
            }
        }

        if (thisRenderer.enabled)
        {
            thisRenderer.material.SetTextureOffset(textureName, uvOffset);
        }
    }

}
