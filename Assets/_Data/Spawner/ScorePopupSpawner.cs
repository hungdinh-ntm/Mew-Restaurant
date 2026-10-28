using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopupSpawner : Spawner
{
    private static ScorePopupSpawner instance;
    public static ScorePopupSpawner Instance => instance;

    public static string ScorePopup = "ScorePopup";

    [SerializeField] protected int timeDespawn = 1;

    protected override void Awake()
    {
        base.Awake();
        if (ScorePopupSpawner.instance != null) Debug.LogError("Only 1 ScorePopupSpawner allow to exist");
        ScorePopupSpawner.instance = this;
    }

    public virtual Transform SpawnPopup(Vector2 pos, Quaternion rot)
    {
        Vector2 newPos = pos + new Vector2(1, 0);

        Transform newObj = this.Spawn(ScorePopupSpawner.ScorePopup, newPos, rot);
        StartCoroutine(this.DespawnRoutine(newObj));

        return newObj;
    }

    protected virtual IEnumerator DespawnRoutine(Transform obj)
    {
        yield return new WaitForSeconds(this.timeDespawn);
        this.Despawn(obj);
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        ScorePopup popup = obj.GetComponent<ScorePopup>();
        popup.ResetPopup();
    }
}
