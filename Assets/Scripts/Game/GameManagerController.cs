using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    bool gameIsOver = false;

    #region EndGame()
    // takes player to Gameover Screen
    public void EndGame(int time)
    {
        if (gameIsOver == false)
        {
            StartCoroutine(WaitForGameOverScreen(time));
        }
    }
    #endregion

    #region Restart()
    // restarts the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion

    #region Quit()
    // quits the game
    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region WaitForGameOverScreen()
    // waits some time and then loads Gameover Screen
    IEnumerator WaitForGameOverScreen(int time)
    {
        yield return new WaitForSeconds(time);
        gameIsOver = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
