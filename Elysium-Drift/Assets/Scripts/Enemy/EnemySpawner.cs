using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
