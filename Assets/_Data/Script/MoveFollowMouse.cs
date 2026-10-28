using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowMouse : MoveFollowTarget
{
    private void Update()
    {
        this.GetMouseClick();
        this.MoveTarget();
    }

    private void FixedUpdate()
    {
        //this.MoveTarget();
    }

    protected virtual void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.GetMousePosition();
            this.isMoving = true;
        }
    }

    protected virtual void GetMousePosition()
    {
        Vector3 mouseP = InputManager.Instance.MousePos;
        this.targetPos = new Vector2(mouseP.x, mouseP.y);
    }
}
