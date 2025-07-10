using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class DestroyObsAbility_Char3 : MonoBehaviour
{
    Collider abilityCollider;
    Animator charAnimator;
    private void Start()
    {
        abilityCollider = GetComponent<Collider>();
        if (abilityCollider == null)
        {
            Debug.LogError("Collider component is missing on " + gameObject.name);
        }
        else
        {
            abilityCollider.isTrigger = true; // Ensure the collider is set as a trigger
        }
        charAnimator = GetComponent<Animator>();
        if (charAnimator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log($"Obstacle {other.name} enter the destroy range");
            var obs = other.GetComponent<WallBlockerScript>();
            if (obs != null)
            {
                Debug.Log($"Trigger destroy ability on {other.name}");
                if (obs.GetObstacleType() == ObstacleType.Left)
                {
                    StartCoroutine(DelayDestroyObsAfterSecs(obs, 0.1f)); // Delay destroy for 0.1 seconds
                    charAnimator.SetTrigger("DestroyObs"); // Trigger the destroy animation
                }
                else
                {
                    Debug.LogWarning("Obstacle type is not Upper, cannot destroy: " + obs.GetObstacleType());
                }
            }
            else
            {
                Debug.LogWarning("WallBlockerScript component is missing on " + other.gameObject.name);
            }
        }
    }

    private IEnumerator DelayDestroyObsAfterSecs(WallBlockerScript obs, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        obs.DestroyedByPlayer(); // Destroy the obstacle without affecting player stats
    }
}
