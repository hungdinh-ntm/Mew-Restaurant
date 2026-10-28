using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimation : LoadMonoBehaviour
{
    [SerializeField] protected Animator animator;

    protected Vector2 lastDir = Vector2.down;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.parent.GetComponent<Animator>();
    }

    public virtual void UpdateAnimation(Vector2 dir)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;

        if (dir.sqrMagnitude < 0.01f)
        {
            PlayIdle();
            return;
        }

        lastDir = dir;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
                animator.Play("cus_right");
            else
                animator.Play("cus_left");
        }
        else
        {
            if (dir.y > 0)
                animator.Play("cus_up");
            else
                animator.Play("cus_down");
        }
    }

    protected virtual void PlayIdle()
    {
        if (Mathf.Abs(lastDir.x) > Mathf.Abs(lastDir.y))
        {
            if (lastDir.x > 0)
                animator.Play("cus_idle_right");
            else
                animator.Play("cus_idle_left");
        }
        else
        {
            if (lastDir.y > 0)
                animator.Play("cus_idle_up");
            else
                animator.Play("cus_idle_down");
        }
    }

    public virtual void FaceDirection(SeatDirection direction)
    {
        switch (direction)
        {
            case SeatDirection.Up:
                lastDir = Vector2.up;
                break;

            case SeatDirection.Down:
                lastDir = Vector2.down;
                break;

            case SeatDirection.Left:
                lastDir = Vector2.left;
                break;

            case SeatDirection.Right:
                lastDir = Vector2.right;
                break;
        }

        this.PlayIdle();
    }
}
