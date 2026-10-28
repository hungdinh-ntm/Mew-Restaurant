using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : LoadMonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    [SerializeField] protected Vector3 mousePos;
    public Vector3 MousePos => mousePos;

    [SerializeField] protected Vector2 inputPos;
    public Vector2 InputPos => inputPos;

    protected override void Awake()
    {
        base.Awake();
        if (InputManager.instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager.instance = this;
    }

    protected virtual void Update()
    {
        this.GetMousePos();
        this.GetInput();
    }

    protected virtual void GetMousePos()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;

        this.mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void GetInput()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameOver) return;

        this.inputPos.x = Input.GetAxisRaw("Horizontal");
        this.inputPos.y = Input.GetAxisRaw("Vertical");

    }

    public virtual void LockInput()
    {
        this.enabled = false;
    }

    public virtual void UnlockInput()
    {
        this.enabled = true;
    }
}
