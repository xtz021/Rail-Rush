using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform playerCart;

    Vector3 posDelta;

    // Start is called before the first frame update
    void Awake()
    {
        posDelta = new Vector3(0,5,-5);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = transform.position - playerCart.position;
        //Debug.Log("" + pos);
        transform.position = playerCart.position + posDelta;
    }
}
