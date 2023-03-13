using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Event eventNextLevel;
    public Event eventPreviousLevel;

    public void onClickNextLevel()
    {
        eventNextLevel.occurred();
    }

    public void onClickPreviousLevel()
    {
        eventPreviousLevel.occurred();
    }

    public void onClickQuit()
    {
        Application.Quit();
    }
}