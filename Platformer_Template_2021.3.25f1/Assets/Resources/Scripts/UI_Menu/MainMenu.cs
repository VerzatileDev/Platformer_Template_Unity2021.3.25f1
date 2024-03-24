using UnityEngine;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <remarks>
/// Starts the Game Scene and Quits the Application.
/// </remarks>
public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
