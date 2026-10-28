using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CounterAbstract : LoadMonoBehaviour, IInteractable
{
    public abstract void Interact(PlayerCtrl playerCtrl);
}
