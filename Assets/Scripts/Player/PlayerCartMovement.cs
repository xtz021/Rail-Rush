using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{

    private float _PlayerCartSpeed = 15f;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _PlayerCartSpeed, Space.World);
    }
}
