using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : InputBase
{
    public override bool IsInput()
    {
        return Input.anyKey;
    }

    public override Vector2 MoveDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
