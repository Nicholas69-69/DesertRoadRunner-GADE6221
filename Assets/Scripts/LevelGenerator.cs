using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public Transform player;

    public int startSections = 5;
    public float spawnDistanceAhead = 100f;
    public float destroyDistanceBehind = 120f;

    private float nextSpawnZ = 0f;

    void Start()
    {
        // Spawns the first few road sections at the start of the game
        for (int i = 0; i < startSections; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Spawns a new section when the player gets close to the end
        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            SpawnSection();
        }

        // Finds all spawned road sections
        GameObject[] sections = GameObject.FindGameObjectsWithTag("RoadSection");

        foreach (GameObject section in sections)
        {
            // Destroys sections that are far behind the player
            if (section.transform.position.z < player.position.z - destroyDistanceBehind)
            {
                Destroy(section);
            }
        }
    }

    void SpawnSection()
    {
        if (roadPrefabs == null || roadPrefabs.Length == 0) return;

        // Picks a random road prefab from the array
        int randomIndex = Random.Range(0, roadPrefabs.Length);
        GameObject prefabToSpawn = roadPrefabs[randomIndex];

        // Spawns the section at the next position
        GameObject newSection = Instantiate(
            prefabToSpawn,
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity
        );

        newSection.tag = "RoadSection";

        // Uses the section endpoint to know where to place the next section
        RoadSectionData data = newSection.GetComponent<RoadSectionData>();

        if (data != null && data.endPoint != null)
        {
            nextSpawnZ = data.endPoint.position.z;
        }
        else
        {
            // Default spacing if the endpoint is missing
            Debug.LogWarning("RoadSectionData or EndPoint missing on " + newSection.name);
            nextSpawnZ += 28f;
        }
    }
}