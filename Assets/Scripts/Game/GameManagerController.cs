using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    bool gameIsOver = false;

    public void EndGame(int time)
    {
        if (gameIsOver == false)
        {
            StartCoroutine(WaitForGameOverScreen(time));
        }
    }

    void Restart()
    {

    }

    void Quit()
    {

    }

    IEnumerator WaitForGameOverScreen(int time)
    {
        yield return new WaitForSeconds(time);
        gameIsOver = true;
        SceneManager.LoadScene("GameOverScene");
    }
}
