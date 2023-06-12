using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    public Text counterText;
    int kills;

    // Update is called once per frame
    void Update()
    {
        ShowKills();

        if(SceneManager.GetActiveScene().name == "Graveyard")
        {
            if (kills == 10)
            {
                SceneManager.LoadScene("Mine");
            }
        }
        else if(SceneManager.GetActiveScene().name == "Mine")
        {
            if (kills == 10)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

    }

    private void ShowKills()
    {
        counterText.text = kills.ToString();
    }

    public void AddKill()
    {
        kills++;
    }
}
