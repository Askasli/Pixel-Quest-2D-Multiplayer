using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : IMousePosition
{
    public Vector2 mousePosition(Transform _transform) //Getting mouse position
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - _transform.position;
    }
}
