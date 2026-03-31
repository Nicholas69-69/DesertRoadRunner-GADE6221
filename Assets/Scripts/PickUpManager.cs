using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public GameObject healthPickUp;
    public GameObject speedPickUp;
    public GameObject slowMoPickUp;

    public float spawnInterval = 5f;
    public float pickUpSpeed = 15f;
    public float spawnZDistance = 50f;
    public float expireTime = 15f;

    private float[] lanePositions = { -4f, 0f, 4f };
    private float timer;
    private Transform player;

    void Start()
    {
        // Finds the player in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Spawns a pickup after the timer reaches the interval
        if (timer >= spawnInterval)
        {
            SpawnPickUp();
            timer = 0f;
        }
    }

    void SpawnPickUp()
    {
        int randomLane = Random.Range(0, 3);
        int randomType = Random.Range(0, 3);

        // Chooses a random pickup type
        GameObject prefab = randomType == 0 ? healthPickUp :
                            randomType == 1 ? speedPickUp : slowMoPickUp;

        // Spawns the pickup ahead of the player in a random lane
        Vector3 spawnPos = new Vector3(
            lanePositions[randomLane],
            1f,
            player.position.z + spawnZDistance
        );

        GameObject pickUp = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Adds movement to the pickup
        pickUp.AddComponent<PickUpMover>().speed = pickUpSpeed;

        // Removes the pickup if the player does not collect it in time
        Destroy(pickUp, expireTime);
    }
}