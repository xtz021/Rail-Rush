using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPointFollowsPlayer : MonoBehaviour
{
    public Vector3 distantFromPlayer = new Vector3(0,2.5f,0);
    public Transform player;


    void Update()
    {
        transform.position = player.position + distantFromPlayer;
    }
}
