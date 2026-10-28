using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIItemSpawner : Spawner
{
    public static string UIItem = "UIItem";

    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.parent.Find("Content");
    }

    public virtual void SpawnUIItem(List<DishSO> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2 pos = Vector2.zero;
            Quaternion rot = Quaternion.identity;

            Transform uiItem = this.Spawn(this.prefabs[0], pos, rot);
            uiItem.localScale = new Vector3(1, 1, 1);

            UIItem UIItem = uiItem.GetComponent<UIItem>();
            UIItem.ShowItem(list[i]);
        }
    }

    public virtual void RemoveUIItem(DishSO dishSO)
    {
        foreach (Transform uiItem in this.holder)
        {
            UIItem x = uiItem.GetComponent<UIItem>();

            if (!uiItem.gameObject.activeSelf) continue;
            if (x.DishSO == null) continue;
            if (x.DishSO != dishSO) continue;

            x.ResetItem();
            this.Despawn(uiItem);

            return;
        }
    }

    public void ClearUIItem()
    {
        foreach (Transform uiItem in this.holder)
        {
            UIItem x = uiItem.GetComponent<UIItem>();

            if (!uiItem.gameObject.activeSelf) continue;
            if (x.DishSO == null) continue;

            x.ResetItem();
            this.Despawn(uiItem);
        }
    }
}
