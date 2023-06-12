using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBotSpawnerMine : MonoBehaviour
{
    public int counter;
    public int xPos, zPos;
    public GameObject enemy;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (counter < 5)
        {
            zPos = Random.Range(-19, 16);
            xPos = Random.Range(217, 255);

            Instantiate(enemy, new Vector3(xPos, 4, zPos), Quaternion.identity);

            yield return new WaitForSeconds(1f);

            counter++;
        }
    }
}
