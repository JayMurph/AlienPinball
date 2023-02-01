/**
 * FILE             : MainMenuController.cs
 * PROJECT          : SENG3060-A1
 * PROGRAMMER       : Joshua Murphy
 * FIRST VERSION    : February 1, 2023
 * DESCRIPTION      : Contains the MainMenuController class
 */
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides methods to be used in the main menu of the pinball game
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Switches the application to the Main Game Scene
    /// </summary>
    public void StartGame() 
    { 
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
