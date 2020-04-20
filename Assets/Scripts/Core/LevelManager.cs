using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        LanguageManager.Instance.OnLanguageChangedEvent.Invoke();
    }

    public void EndApplication()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        Application.OpenURL(Application.persistentDataPath);
    }
}
