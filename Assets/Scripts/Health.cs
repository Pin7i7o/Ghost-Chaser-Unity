using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text counterText;
    int hp;

    // Update is called once per frame
    void Update()
    {
        ShowHp();
    }

    private void ShowHp()
    {
        counterText.text = hp.ToString();
    }

    public void GetHp(int currentHP)
    {
        hp = currentHP;
    }
}
