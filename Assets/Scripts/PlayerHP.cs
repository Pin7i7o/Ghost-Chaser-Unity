using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    //stats
    public int hp;

    //GameOverScene
    public string sceneName;

    //HealthUi
    Health currentHp;

    private void Awake()
    {
        currentHp = GameObject.Find("Health").GetComponent<Health>();
    }

    private void Update()
    {
        currentHp.GetHp(hp);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            GameOver();
        }
    }
    public void RegenHealth(int regen)
    {
        hp += regen;
    }
    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void GameOver()
    {
        changeScene();
    }
}
