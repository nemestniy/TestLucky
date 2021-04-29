using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : InputBase
{
    private Vector2 startPosition = Vector2.zero;

    public override bool IsInput()
    {
        return Input.touchCount > 0;
    }

    public override Vector2 MoveDirection()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                var diffTouch = touch.position - startPosition;
                if (diffTouch.sqrMagnitude > 1.0f)
                    return diffTouch.normalized;

                return diffTouch;
            }
        }

        return Vector2.zero;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                startPosition = touch.position;
            else if (touch.phase == TouchPhase.Ended)
                startPosition = Vector2.zero;
        }

    }
}
