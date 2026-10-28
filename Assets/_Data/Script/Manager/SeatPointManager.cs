using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatPointManager : LoadMonoBehaviour
{
    private static SeatPointManager instance;
    public static SeatPointManager Instance => instance;

    [SerializeField] protected List<SeatPoint> listSeats;

    protected override void Awake()
    {
        base.Awake();
        if (SeatPointManager.instance != null) Debug.LogError("Only 1 CustomerSeatManager allow to exist");
        SeatPointManager.instance = this;
    }

    public virtual void AddSeat(SeatPoint seat)
    {
        this.listSeats.Add(seat);
    }

    //public virtual SeatPoint FindSeat()
    //{
    //    foreach (SeatPoint seat in this.listSeats)
    //    {
    //        if (!seat.HasCustomer()) return seat;
    //    }
    //    return null;
    //}

    public virtual SeatPoint FindSeat()
    {
        List<SeatPoint> availableSeats = new List<SeatPoint>();

        foreach (SeatPoint seat in this.listSeats)
        {
            if (!seat.HasCustomer())
            {
                availableSeats.Add(seat);
            }
        }

        if (availableSeats.Count == 0) return null;

        int randomIndex = Random.Range(0, availableSeats.Count);
        return availableSeats[randomIndex];
    }
}
