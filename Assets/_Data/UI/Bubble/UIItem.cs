using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : LoadMonoBehaviour
{
    [Header("UI Item")]
    [SerializeField] protected Image itemImage;
    public Image ItemImage => itemImage;

    [SerializeField] protected DishSO dishSO;
    public DishSO DishSO => dishSO;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemImage();
    }

    protected virtual void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = GetComponentInChildren<Image>();
        //this.itemImage = transform.Find("ItemImage").GetComponent<Image>();
    }

    public virtual void ShowItem(DishSO dishSO)
    {
        this.dishSO = dishSO;
        this.itemImage.sprite = dishSO.sprite;
    }

    public virtual void ResetItem()
    {
        this.itemImage.sprite = null;
        this.dishSO = null;
    }
}
