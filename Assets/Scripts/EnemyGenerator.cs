using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenEnemies = 4f;
    [SerializeField] int maxEnemies = 3;
    [SerializeField] float activationDistance = 8f;

    int enemiesSpawned = 0;
    bool isActive = false;

    void Update()
    {
        if (isActive || target == null) return;

        //chequeamos distancia al player
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= activationDistance)
        {
            isActive = true;
            StartCoroutine(GenerateEnemies());
        }
    }

    IEnumerator GenerateEnemies()
    {
        while (enemiesSpawned < maxEnemies)
        {
            yield return new WaitForSeconds(timeBetweenEnemies);
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.GetComponent<AIControl>().SetTarget(target);
            enemiesSpawned++;
        }
    }
}
