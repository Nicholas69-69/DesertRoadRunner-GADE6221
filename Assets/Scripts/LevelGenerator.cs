using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public Transform player;

    public int startSections = 5;
    public float spawnDistanceAhead = 100f;
    public float destroyDistanceBehind = 120f;

    // Keeps track of the current road end
    private Transform currentEndPoint;

    // Used for spawning ahead
    private float nextSpawnZ = 0f;

    void Start()
    {
        // Spawn first sections
        for (int i = 0; i < startSections; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Spawn new section ahead of player
        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            SpawnSection();
        }

        // Find all road sections
        GameObject[] sections = GameObject.FindGameObjectsWithTag("RoadSection");

        foreach (GameObject section in sections)
        {
            // Destroy sections behind player
            if (section.transform.position.z < player.position.z - destroyDistanceBehind)
            {
                Destroy(section);
            }
        }
    }

    public void SpawnSection()
    {
        if (roadPrefabs == null || roadPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, roadPrefabs.Length);
        GameObject prefabToSpawn = roadPrefabs[randomIndex];

        GameObject newSection = Instantiate(prefabToSpawn);

        RoadSectionData data = newSection.GetComponent<RoadSectionData>();

        if (data != null)
        {
            // First section
            if (currentEndPoint == null)
            {
                newSection.transform.position = Vector3.zero;
            }
            else
            {
                // Align new section start to previous end
                Vector3 offset = data.startPoint.position - newSection.transform.position;

                newSection.transform.position = currentEndPoint.position - offset;
            }

            // Save endpoint for next section
            currentEndPoint = data.endPoint;

            // Update spawn position
            nextSpawnZ = currentEndPoint.position.z;
        }

        newSection.tag = "RoadSection";
    }
}