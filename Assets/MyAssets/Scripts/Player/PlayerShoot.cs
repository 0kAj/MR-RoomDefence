using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject prefab;
    public float spawnSpeed = 10;

    void Update()
    {
        // Shoot ball from controller
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject spawnedBall = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody spawnedBallRB = spawnedBall.GetComponent<Rigidbody>();
            spawnedBallRB.linearVelocity = transform.forward * spawnSpeed;
        }
    }
}
