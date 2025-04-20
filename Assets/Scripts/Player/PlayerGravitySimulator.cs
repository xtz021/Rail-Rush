using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravitySimulator : MonoBehaviour
{
    public float verticalVelocity = 0f; // Current vertical speed
    public bool isFalling = true;

    private const float gravity = -9.81f;

    private void Update()
    {
        if (isFalling)
        {
            verticalVelocity += gravity * Time.deltaTime;
            transform.position += new Vector3(0, verticalVelocity * Time.deltaTime, 0);

            // Optional: Fake ground check (adjust based on your world)
            if (transform.position.y <= 0f)
            {
                isFalling = true;
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                verticalVelocity = 0f;
            }
        }
    }


}
