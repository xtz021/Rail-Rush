using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;
    private float swipeThreshold = 0.05f; // ~5% of screen

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        Input.multiTouchEnabled = true; // Prevent touch conflicts
        Application.targetFrameRate = 60; // Lock frame rate
#endif
    }

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        // Check for mouse or touch input
#if UNITY_EDITOR
        #region Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion
#endif
        #region Touch Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                tap = true;
                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the swipe delta
        swipeDelta = Vector2.zero;
        if(isDragging)
        {
            //if (Input.touches.Length > 0)
            //{
            //    swipeDelta = Input.touches[0].position - startTouch;
            //}
            //else if (Input.touchCount == 0 && Input.GetMouseButton(0))
            //{
            //    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            //}
            if (Input.touchCount > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
#if UNITY_EDITOR
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
#endif
        }

        // Check if the swipe is long enough
        Vector2 normalizedDelta = new Vector2
        (
            swipeDelta.x / Screen.width,
            swipeDelta.y / Screen.height
        );

        if (normalizedDelta.magnitude > swipeThreshold)
        //if (swipeDelta.magnitude > 80)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            // Determine the direction of the swipe
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            Reset();
        }

    }

    public void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
}
