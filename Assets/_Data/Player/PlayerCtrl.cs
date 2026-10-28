using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : LoadMonoBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;

    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] protected PlayerHold playerHold;
    public PlayerHold PlayerHold => playerHold;

    [SerializeField] protected PlayerInteract playerInteract;
    public PlayerInteract PlayerInteract => playerInteract;

    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerCtrl.instance != null) Debug.LogError("Only 1 PlayerCtrl allow to exist");
        PlayerCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerMovement();
        this.LoadPlayerHold();
        this.LoadPlayerInteract();
        this.LoadRigidbody2D();
    }

    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = GetComponentInChildren<PlayerMovement>();
    }

    protected virtual void LoadPlayerHold()
    {
        if (this.playerHold != null) return;
        this.playerHold = GetComponentInChildren<PlayerHold>();
        //Debug.LogWarning(transform.name + ": LoadPlayerHold", gameObject);
    }

    protected virtual void LoadPlayerInteract()
    {
        if (this.playerInteract != null) return;
        this.playerInteract = GetComponentInChildren<PlayerInteract>();
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }
}
