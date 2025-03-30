using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosHighlight : MonoBehaviour
{
    public float radius;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform t in transform)
        {
            Gizmos.DrawSphere(t.position, radius);
        }
    }
}
