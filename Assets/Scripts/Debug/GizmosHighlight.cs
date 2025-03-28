using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosHighlight : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
