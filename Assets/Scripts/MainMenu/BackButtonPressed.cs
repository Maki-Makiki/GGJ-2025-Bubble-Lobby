using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonPressed : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
