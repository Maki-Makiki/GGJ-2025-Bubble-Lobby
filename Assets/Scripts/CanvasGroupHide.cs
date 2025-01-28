using UnityEngine;

public class CanvasGroupHide : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public bool enablerInsteed;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = this.GetComponent<CanvasGroup>();
        }

        if (enablerInsteed)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else 
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
       
    }
}
