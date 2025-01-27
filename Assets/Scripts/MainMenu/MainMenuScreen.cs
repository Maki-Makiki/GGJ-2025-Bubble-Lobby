using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("DEV_CardSys");
    }

    public void OptionsButtonPressed()
    {
        SceneManager.LoadScene("Options");
    }
}