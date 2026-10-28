using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerAbstract
{
    [Header("Play Interact")]
    [SerializeField] protected IInteractable currentInteractable;
    [SerializeField] protected Vector2 lastDirection;
    [SerializeField] protected float interactDistance = 1f;
    [SerializeField] protected LayerMask interactLayer;

    protected virtual void Update()
    {
        this.HandleDirection();
        this.HandleRaycast();
        this.HandleInteract();
    }

    protected virtual void HandleDirection()
    {
        Vector2 inputDis = InputManager.Instance.InputPos;
        if (inputDis != Vector2.zero)
        {
            this.lastDirection = inputDis;
        }
    }

    protected virtual void HandleRaycast()
    {
        if (this.lastDirection == Vector2.zero) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, this.lastDirection, this.interactDistance, this.interactLayer);
        Debug.DrawRay(transform.position, this.lastDirection * this.interactDistance, Color.green);

        if (hit.collider != null)
        {
            this.currentInteractable = hit.collider.GetComponentInParent<IInteractable>();
            //Debug.Log("Đã trúng: " + this.currentCounter.name);
        }
        else
        {
            this.currentInteractable = null;
            //Debug.Log("Don't find collider");
        }
    }

    protected virtual void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (this.currentInteractable != null) this.currentInteractable.Interact(this.playerCtrl);
        }
    }
}
