using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 15f;
    public Transform player;

    // Stops the obstacle from giving score more than once
    private bool scored = false;

    void Update()
    {
        // Moves the obstacle backwards down the road
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Gives the player a point once the obstacle has been passed
        if (!scored && player != null && transform.position.z < player.position.z)
        {
            scored = true;

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddPoint();
            }
        }

        // Removes the obstacle once it is far behind the player
        if (player != null && transform.position.z < player.position.z - 20f)
        {
            Destroy(gameObject);
        }
    }
}