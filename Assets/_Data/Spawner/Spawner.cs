using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : LoadMonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] protected Transform holder;
    [SerializeField] protected List<Transform> prefabs;
    //public Transform Prefab => prefab;

    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount => spawnedCount;

    [SerializeField] protected List<Transform> poolObjs;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefab();
    }

    protected virtual void HidePrefab()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        //Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.Log("Prefab not found: " + prefabName);
            return null;
        }
        return this.Spawn(prefab, spawnPos, rotation);
    }

    public virtual Transform Spawn(Transform prefab, Vector2 spawnPos, Quaternion spawnRos)
    {
        Transform newPrefab = this.GetObjFromPool(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, spawnRos);
        newPrefab.gameObject.SetActive(true);

        newPrefab.SetParent(this.holder);
        this.spawnedCount++;
        return newPrefab;
    }

    protected virtual Transform GetObjFromPool(Transform prefab)
    {
        foreach (Transform obj in poolObjs)
        {
            if (obj.name == prefab.name)
            {
                this.poolObjs.Remove(obj);
                return obj;
            }
        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }

    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;

        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        obj.localPosition = holder.localPosition;
        obj.localRotation = holder.localRotation;

        this.spawnedCount--;
    }

    public virtual Transform RandomPrefab()
    {
        int index = Random.Range(0, this.prefabs.Count);
        return this.prefabs[index];
    }
}
