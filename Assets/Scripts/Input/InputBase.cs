using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputBase : MonoBehaviour
{
    public abstract bool IsInput();
    public abstract Vector2 MoveDirection();
}
