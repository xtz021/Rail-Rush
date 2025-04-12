using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(pos2.position - pos1.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
