using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public string replaygame;
    public string quitgame;

    public void ReplayGame()
    {
        SceneManager.LoadScene(replaygame);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(quitgame);
    }
}
