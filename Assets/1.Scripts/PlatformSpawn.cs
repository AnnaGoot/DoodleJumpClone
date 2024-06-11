using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platformPrefab;

    public GameObject boostPlatformPrefab;
    public float boostPlatformChance = 0.3f;
    public GameObject disappearingPlatformPrefab;
    public float disappearingPlatformChance = 0.3f;

    public float platformSpacingY = 2.0f;
    public int initialPlatforms = 20;

    private float despawnYThreshold = -5f;
    private float nextPlatformCheck = 0.0f;

    public GameObject coinPrefab;
    public float coinSpawnChance = 0.5f;

    private PlayerController playerController;

    //public void Init(PlayerController controller)
    //{
    //    playerController = controller;
    //}

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

        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (playerController != null)
        {
            CheckAndSpawnPlatforms();
            DespawnOffscreenPlatforms();

        }
    }

    private void CheckAndSpawnPlatforms()
    {
        if (playerController.transform.position.y > nextPlatformCheck - (initialPlatforms * platformSpacingY / 2))
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-1.7f, 1.7f), nextPlatformCheck, 0);
            SpawnPlatform(spawnPosition);
            nextPlatformCheck += platformSpacingY;
        }
    }

    private void DespawnOffscreenPlatforms()
    {
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            float playerY = playerController.transform.position.y;

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

            if (playerController.transform.position.y < despawnYThreshold)
            {
                playerController.HandleGameOver();
            }
    }


    void SpawnPlatform(Vector3 position)
    {
        GameObject prefabToSpawn = ChooosePlatformType();
        Instantiate(prefabToSpawn, position, Quaternion.identity);
        //GameObject platformInstance = Instantiate(prefabToSpawn, position, Quaternion.identity);
        //Platform platform = platformInstance.GetComponent<Platform>();
        //if (platform != null)
        //{
        //    platform.Init(playerController);
        //}

        SpawnCoin(position);
    }

    private GameObject ChooosePlatformType()
    {
        float randomValue = Random.value;
        if (randomValue < boostPlatformChance)
        {
            return boostPlatformPrefab;
        }
        else if (randomValue < boostPlatformChance + disappearingPlatformChance)
        {
            return disappearingPlatformPrefab;
        }
        else
        {
            return platformPrefab;
        }
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
