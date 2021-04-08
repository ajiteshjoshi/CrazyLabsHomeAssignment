using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool tap, leftInput, rightInput, upInput, downInput;
    private bool dragging = false;
    private Vector2 startTouch, swipeDelta;
    public float touchInput;

    private void Update()
    {
        tap = leftInput = rightInput = upInput = downInput = false;
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                dragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                dragging = false;
                Reset();
            }

        }

        swipeDelta = Vector2.zero;

        if (dragging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
        }

        if(swipeDelta.magnitude>100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    leftInput = true;
                }
                else
                {
                    rightInput = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    downInput = true;
                }
                else
                {
                    upInput = true;
                }
            }
        }

        if (leftInput)
        {
            touchInput = -1f;
        }
        else if (rightInput)
        {
            touchInput = 1f;
        }
        else
        {
            touchInput = 0f;
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        dragging = false;
    }
}
