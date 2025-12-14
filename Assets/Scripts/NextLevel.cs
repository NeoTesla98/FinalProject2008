using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string nextLevelName;
    public int nextLevelValue;
    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelReached", nextLevelValue);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        Time.timeScale = 1;

        
    }
}
