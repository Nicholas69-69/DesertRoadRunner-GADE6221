using UnityEngine;

public class PickUpMover : MonoBehaviour
{
    public float speed = 15f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up * 90f * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);
    }
}