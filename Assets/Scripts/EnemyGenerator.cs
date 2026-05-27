using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenEnemies = 4f;

    private void Start()
    {
        StartCoroutine(GenerateEnemies());
    }

    IEnumerator GenerateEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEnemies);
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<AIControl>().SetTarget(target);
        }
    }
}
