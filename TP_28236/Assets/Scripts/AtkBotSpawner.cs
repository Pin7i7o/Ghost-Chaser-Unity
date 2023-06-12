using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBotSpawner : MonoBehaviour
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
            zPos = Random.Range(23, -18);
            xPos = Random.Range(-20, 17);

            Instantiate(enemy, new Vector3(xPos, 2, zPos), Quaternion.identity);

            yield return new WaitForSeconds(1f);

            counter++;
        }
    }
}
