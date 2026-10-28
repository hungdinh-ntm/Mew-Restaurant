using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : LoadMonoBehaviour
{
    private static OrderManager instance;
    public static OrderManager Instance => instance;

    [SerializeField] protected List<DishSO> listMainMenu = new();
    [SerializeField] protected List<DishSO> listDessertMenu = new();

    protected override void Awake()
    {
        base.Awake();
        if (OrderManager.instance != null) Debug.LogError("Only 1 OrderManager allow to exist");
        OrderManager.instance = this;

        this.LoadListMenu(LevelManager.Instance.CurrentLevel);
        Debug.Log("Current Level: " + LevelManager.Instance.CurrentLevel.name);
    }

    //public virtual void LoadListMenu(List<DishSO> listMenuLevel)
    //{
    //    this.listMenu = listMenuLevel.ToList();
    //}

    public virtual void LoadListMenu(LevelSO levelSO)
    {
        this.listMainMenu = levelSO.mainDishMenu.ToList();
        this.listDessertMenu = levelSO.dessertMenu.ToList();
    }

    //public virtual List<DishSO> Order()
    //{
    //    List<DishSO> orders = new();
    //    int orderCount = this.GetNumberOfDishes();

    //    while (orders.Count < orderCount)
    //    {
    //        int index = Random.Range(0, this.listMenu.Count);
    //        DishSO dish = this.listMenu[index];
    //        if (orders.Contains(dish)) continue;
    //        orders.Add(dish);
    //    }
    //    return orders;
    //}

    public virtual List<DishSO> Order()
    {
        List<DishSO> orders = new();
        int orderCount = this.GetNumberOfDishes();

        bool hasMainDish = this.listMainMenu.Count > 0;
        bool hasDessert = this.listDessertMenu.Count > 0;

        if (hasMainDish && !hasDessert)
        {
            orders.Add(GetRandomDish(this.listMainMenu));
            return orders;
        }

        if (!hasMainDish && hasDessert)
        {
            orders.Add(GetRandomDish(this.listDessertMenu));
            return orders;
        }

        DishSO mainDish = GetRandomDish(this.listMainMenu);
        DishSO dessertDish = GetRandomDish(this.listDessertMenu);

        if (orderCount == 1)
        {
            if (Random.Range(0, 2) == 0) orders.Add(mainDish);
            else orders.Add(dessertDish);
            return orders;
        }

        orders.Add(mainDish);
        orders.Add(dessertDish);

        return orders;
    }

    protected virtual int GetNumberOfDishes()
    {
        float time = GameTime.Instance.CurrentTime;
        float levelTime = GameTime.Instance.LevelTime;

        if (this.listMainMenu.Count == 0 || this.listDessertMenu.Count == 0)
        {
            return 1;
        }

        return (time > levelTime * 0.5f) ? 1 : Random.Range(1, 3);
    }

    protected virtual DishSO GetRandomDish(List<DishSO> list)
    {
        if (list.Count == 0) return null;

        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
