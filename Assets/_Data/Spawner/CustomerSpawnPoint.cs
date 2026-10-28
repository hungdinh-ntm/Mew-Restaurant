using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawnPoint : LoadMonoBehaviour
{
    [SerializeField] protected Transform pointSpawn;
    public Transform PointSpawn => pointSpawn;

    [SerializeField] protected CustomerSpawnCtrl customerSpawnCtrl;

    [SerializeField] protected int spawnLimit;
    [SerializeField] protected float spawnDelay = 1f;
    [SerializeField] protected float spawnTimer = 0f;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPointSpawn();
        this.LoadMeteoriteCtrl();
    }

    protected virtual void LoadMeteoriteCtrl()
    {
        if (this.customerSpawnCtrl != null) return;
        this.customerSpawnCtrl = GetComponent<CustomerSpawnCtrl>();
    }

    protected virtual void LoadPointSpawn()
    {
        if (this.pointSpawn != null) return;
        this.pointSpawn = transform.Find("Point");
    }

    protected virtual void FixedUpdate()
    {
        this.Spawning();
    }

    protected virtual void Spawning()
    {
        if (!GameManager.Instance.GameStart) return;
        if (!GameManager.Instance.CustomerCanSpawn) return;
        if (GameManager.Instance.GameOver) return;

        if (this.ReachLimit()) return;

        this.spawnTimer += Time.fixedDeltaTime;
        if (this.spawnTimer < this.spawnDelay) return;
        this.spawnTimer = 0f;

        Vector2 pos = this.pointSpawn.position;
        Quaternion rot = this.pointSpawn.rotation;

        Transform prefab = this.customerSpawnCtrl.CustomerSpawner.RandomPrefab();
        Transform newObj = this.customerSpawnCtrl.CustomerSpawner.Spawn(prefab, pos, rot);
        AudioManger.Instance.PlayOpen();

        CustomerCtrl customer = newObj.GetComponent<CustomerCtrl>();
        customer.AssignSeat();
    }

    protected virtual bool ReachLimit()
    {
        float time = GameTime.Instance.CurrentTime;
        float levelTime = GameTime.Instance.LevelTime;

        int spawnCount = this.customerSpawnCtrl.CustomerSpawner.SpawnedCount;
        this.spawnLimit = (time > levelTime * 0.5f) ? 1 : Random.Range(1, 4);

        return spawnCount >= this.spawnLimit;
    }
}
