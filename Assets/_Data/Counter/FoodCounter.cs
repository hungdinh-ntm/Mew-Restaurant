
using UnityEngine;

public class FoodCounter : CounterAbstract
{
    [SerializeField] protected string itemName;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemName();
    }

    protected virtual void LoadItemName()
    {
        itemName = transform.name.Replace("Box", "");
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        //string itemName = transform.name.Replace("Counter", "");
        PlayerHold playerHold = playerCtrl.PlayerHold;
        if (playerHold.HasItem()) return;

        Vector2 pos = playerHold.HoldPoint.position;
        Quaternion ros = playerHold.HoldPoint.rotation;

        Transform newObj = ItemSpawner.Instance.Spawn(this.itemName, pos, ros);

        KitchenObjCtrl item = newObj.GetComponent<KitchenObjCtrl>();
        playerHold.HoldItem(item);
    }
}
