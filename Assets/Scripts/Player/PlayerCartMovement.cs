using System.Collections;
using System.Collections.Generic;
using Unity.Splines.Examples;
using UnityEngine;

public class PlayerCartMovement : MonoBehaviour
{
    // Attributes for jump calculations
    public float jumpHeight = 5f;
    private float normalY;

    [SerializeField] Transform playerCart;

    private float _PlayerCartSpeed = 15f;

    // Vectors for swipe calculations
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private void Start()
    {
        
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * _PlayerCartSpeed, Space.World);
        TouchControl();
    }


    public void TouchControl()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
		        {
                    StartCoroutine(Jump());
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
		        {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
		        {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
		        {
                    Debug.Log("right swipe");
                }
            }
        }
    }

    IEnumerator Jump()
    {
        float timer = 0f;
        float duration = 0.5f;

        while (timer < duration)
        {
            playerCart.position = new Vector3(playerCart.position.x, normalY + Mathf.Sin(timer / duration * Mathf.PI) * jumpHeight, playerCart.position.z);
            timer += Time.deltaTime;
            yield return null;
        }
        playerCart.position = new Vector3(playerCart.position.x, normalY, playerCart.position.z);
    }

}
