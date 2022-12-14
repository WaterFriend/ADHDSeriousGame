using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : Singleton<FishGenerator>
{
    public GameObject smallFish;
    public GameObject mediumFish;
    public GameObject largeFish;

    public Vector2 spawnInterval;
    public float leftBound;
    public float rightBound;

    private enum FishType {SMALL, MEDIUM, LARGE};

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void StartFishSpawnCoroutine()
    {
        StartCoroutine(FishSpawnCoroutine());
    }

    public void StopFishSpawnCoroutine()
    {
        StopAllCoroutines();
    }

    public Fish SpawnFishAtPos(Vector3 pos)
    {
        GameObject fish = ObjectPoolManager.Instance.Spawn(this.smallFish, pos, Quaternion.identity);
        fish.GetComponent<Fish>().Init();
        FishManager.Instance.AddFish(fish.GetComponent<Fish>());
        return fish.GetComponent<Fish>();
    }

    IEnumerator FishSpawnCoroutine()
    {
        while (true)
        {
            float spawnTime = Random.Range(spawnInterval.x, spawnInterval.y);
            float spawnPosX = Random.Range(leftBound, rightBound);
            FishType type = (FishType)Random.Range((int)FishType.SMALL, (int)FishType.LARGE+1);

            Vector3 spawnPos = transform.position;
            spawnPos.x = spawnPosX;

            GameObject fish = null; 
            switch (type)
            {
                case FishType.SMALL:
                    fish = ObjectPoolManager.Instance.Spawn(smallFish, spawnPos, Quaternion.identity);
                    break;
                case FishType.MEDIUM:
                    fish = ObjectPoolManager.Instance.Spawn(mediumFish, spawnPos, Quaternion.identity);
                    break;
                case FishType.LARGE:
                    fish = ObjectPoolManager.Instance.Spawn(largeFish, spawnPos, Quaternion.identity);
                    break;
                default:
                    fish = ObjectPoolManager.Instance.Spawn(smallFish, spawnPos, Quaternion.identity);
                    break;
            }

            fish.GetComponent<Fish>().Init();
            FishManager.Instance.AddFish(fish.GetComponent<Fish>());

            yield return new WaitForSeconds(spawnTime);
        }
    }

}
