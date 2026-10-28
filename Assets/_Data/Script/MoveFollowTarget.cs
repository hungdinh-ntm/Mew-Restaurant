using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowTarget : LoadMonoBehaviour
{
    [Header("Move Target")]
    [SerializeField] protected Vector2 targetPos;
    [SerializeField] protected float moveSpeed = 4f;
    [SerializeField] protected float stopDistance = 0.3f;
    [SerializeField] protected bool isMoving = false;

    protected virtual void MoveTarget()
    {
        if (!this.isMoving) return;

        Vector2 current = transform.parent.position;

        if (Mathf.Abs(current.x - targetPos.x) > stopDistance)
        {
            float newX = Mathf.MoveTowards(current.x, targetPos.x, moveSpeed * Time.deltaTime);
            transform.parent.position = new Vector2(newX, current.y);
            return;
        }

        if (Mathf.Abs(current.y - targetPos.y) > stopDistance)
        {
            float newY = Mathf.MoveTowards(current.y, targetPos.y, moveSpeed * Time.deltaTime);
            transform.parent.position = new Vector2(current.x, newY);
            return;
        }

        this.isMoving = false;
    }

    public void StopMoving()
    {
        this.isMoving = false;
    }
}
