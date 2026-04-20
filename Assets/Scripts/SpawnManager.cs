using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public ObstacleObjectPool pool;

    void Start()
    {
        pool = FindObjectOfType<ObstacleObjectPool>();
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        // 1.18 stop moving left when the game is over
        var player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        int randomType = Random.Range(0, 3);

        var obstacle = pool.Acquire(randomType);

        obstacle.transform.position = spawnPoint.position;
        obstacle.transform.rotation = Quaternion.identity;

        obstacle.GetComponent<obstacleType>().Type = randomType;
    }
}

