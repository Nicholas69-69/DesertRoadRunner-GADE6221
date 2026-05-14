using UnityEngine;

public class RoadEndTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("RoadEndTrigger activated by: " + other.name);
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered RoadEndTrigger, spawning new section.");
            triggered = true;
            LevelGenerator generator = FindObjectOfType<LevelGenerator>();
            if (generator != null)
            {
                Debug.Log("LevelGenerator found, spawning new section.");
                generator.SpawnSection();
            }
        }
    }
}