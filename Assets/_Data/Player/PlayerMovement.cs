using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    //public Rigidbody2D rb;
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Vector2 lastInput = Vector2.down;

    private void Update()
    {
        this.PlayerMove();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;

        this.playerCtrl.Rb.MovePosition(this.playerCtrl.Rb.position + InputManager.Instance.InputPos * speed * Time.fixedDeltaTime);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAnimator();
        //this.LoadRigibody2D();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.parent.GetComponent<Animator>();
    }

    //protected virtual void LoadRigibody2D()
    //{
    //    if (this.rb != null) return;
    //    this.rb = transform.parent.GetComponent<Rigidbody2D>();
    //}

    protected virtual void PlayerMove()
    {
        Vector2 input = InputManager.Instance.InputPos;

        if (input.sqrMagnitude > 0)
        {
            this.lastInput = input;

            if (input.y > 0) animator.Play("meo_up");
            else if (input.y < 0) animator.Play("meo_down");
            else if (input.x < 0) animator.Play("meo_left");
            else if (input.x > 0) animator.Play("meo_right");
        }
        else
        {
            if (this.lastInput.y > 0) animator.Play("meo_idle_up");
            else if (this.lastInput.y < 0) animator.Play("meo_idle_down");
            else if (this.lastInput.x < 0) animator.Play("meo_idle_left");
            else if (this.lastInput.x > 0) animator.Play("meo_idle_right");
        }
    }
}
