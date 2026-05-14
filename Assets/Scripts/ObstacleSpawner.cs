using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject laneObstaclePrefab;

    public float startSpawnInterval = 2f;
    public float minimumSpawnInterval = 0.7f;
    public float obstacleSpeed = 15f;
    public float spawnZDistance = 50f;

    private float[] lanePositions = { -4f, 0f, 4f };
    private float timer;
    private Transform player;
    private float currentSpawnInterval;

    void Start()
    {
        // Finds the player in the scene
        player = GameObject.FindWithTag("Player").transform;

        // Sets the starting spawn interval
        currentSpawnInterval = startSpawnInterval;
    }

    void Update()
    {
        // Updates the difficulty based on score
        UpdateDifficulty();

        timer += Time.deltaTime;

        // Spawns an obstacle once the timer reaches the interval
        if (timer >= currentSpawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void UpdateDifficulty()
    {
        if (ScoreManager.instance != null)
        {
            int score = ScoreManager.instance.score;

            // Makes obstacles spawn faster as the score increases
            currentSpawnInterval = startSpawnInterval - (score * 0.05f);

            // Stops the interval from becoming too fast
            if (currentSpawnInterval < minimumSpawnInterval)
            {
                currentSpawnInterval = minimumSpawnInterval;
            }
        }
    }

    void SpawnObstacle()
    {
        int obstacleType = Random.Range(0, 2);

        // Spawns a single obstacle in one random lane
        if (obstacleType == 0)
        {
            int randomLane = Random.Range(0, 3);
            SpawnSingleObstacle(lanePositions[randomLane], 0.05f);
        }
        else
        {
            // Spawns two obstacles in two different random lanes
            int firstLane = Random.Range(0, 3);
            int secondLane;

            do
            {
                secondLane = Random.Range(0, 3);
            }
            while (secondLane == firstLane);

            SpawnSingleObstacle(lanePositions[firstLane], 0.05f);
            SpawnSingleObstacle(lanePositions[secondLane], 0.05f);
        }
    }

    void SpawnSingleObstacle(float laneX, float yPos)
    {
        // Creates the obstacle ahead of the player
        Vector3 spawnPos = new Vector3(
            laneX,
            yPos,
            player.position.z + spawnZDistance
        );

        GameObject obstacle = Instantiate(laneObstaclePrefab,spawnPos,Quaternion.identity);
        obstacle.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        // Adds or finds the ObstacleMover script
        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();
        if (mover == null)
        {
            mover = obstacle.AddComponent<ObstacleMover>();
        }

        // Sets obstacle speed and player reference
        mover.speed = obstacleSpeed;
        mover.player = player;
    }
}