using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    public GameObject boostPlatformPrefab;
    public float boostPlatformChance = 0.1f;
    public GameObject disappearingPlatformPrefab;
    public float disappearingPlatformChance = 0.1f;

    public float platformSpacingY = 2.0f;
    public int initialPlatforms = 20;

    private float despawnYThreshold = -5f;
    private float nextPlatformCheck = 0.0f;

    public GameObject coinPrefab;
    public float coinSpawnChance = 0.3f;

    private void Start()
    {
        float initialY = 0;
        for (int i = 0; i < initialPlatforms; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1.7f, 1.7f), initialY, 0);
            initialY += platformSpacingY;
            SpawnPlatform(spawnPosition);
        }
        nextPlatformCheck = initialY;
    }

    private void Update()
    {
        CheckAndSpawnPlatforms();
        DespawnOffscreenPlatforms();
    }

    private void CheckAndSpawnPlatforms()
    {
        if (PlayerController.instance != null &&
            PlayerController.instance.transform.position.y > nextPlatformCheck - (initialPlatforms * platformSpacingY / 2))
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1.7f, 1.7f), nextPlatformCheck, 0);
            SpawnPlatform(spawnPosition);
            nextPlatformCheck += platformSpacingY;
        }
    }

    private void DespawnOffscreenPlatforms()
    {
        if (PlayerController.instance != null)
        {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            float playerY = PlayerController.instance.transform.position.y;

            foreach (GameObject platform in platforms)
            {
                if (platform.transform.position.y < playerY + despawnYThreshold)
                {
                    Destroy(platform);
                }
            }

            foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
            {
                if (coin.transform.position.y < playerY + despawnYThreshold)
                {
                    Destroy(coin);
                }
            }

            if (PlayerController.instance != null && PlayerController.instance.transform.position.y < despawnYThreshold)
            {
                PlayerController.instance.HandleGameOver();
            }
        }

    }


    void SpawnPlatform(Vector3 position)
    {
        GameObject prefabToSpawn = ChoosePlatformPrefab();
        Instantiate(prefabToSpawn, position, Quaternion.identity);
        SpawnCoin(position);
    }

    private GameObject ChoosePlatformPrefab()
    {
        if (Random.value < boostPlatformChance)
        {
            return boostPlatformPrefab;
        }

        if (Random.value < disappearingPlatformChance)
        {
            return disappearingPlatformPrefab;
        }
        return platformPrefabs[Random.Range(0, platformPrefabs.Length)];
    }

    private void SpawnCoin(Vector3 position)
    {
        if (Random.value < coinSpawnChance)
        {
            Vector3 coinPosition = position + new Vector3(0, 0.5f, 0);
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }

    }
}
