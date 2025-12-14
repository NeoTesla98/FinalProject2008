using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    void Start()
    {
        Button button = GetComponent<Button>();
if(PlayerPrefs.GetInt("LevelReached") !>= level)
        {
            button.interactable=false;
        }

    }
}
