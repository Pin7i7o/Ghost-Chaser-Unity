using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    bool launchGame = false;

    private void Update()
    {
        if (launchGame == false && Input.GetKeyDown(KeyCode.Space))
        {
            launchGame = true;
            SceneManager.LoadScene("Graveyard");
        }
    }
}
