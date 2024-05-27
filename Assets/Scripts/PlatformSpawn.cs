using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    public GameObject boostPlatformPrefab;
    public float boostPlatformChance = 0.1f;

    public float platformSpacingY = 2.0f;
    public int initialPlatforms = 20;
    private float despawnYThreshold = -5f;

    private float nextPlatformCheck = 0.0f;

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
        if (PlayerController.instance.transform.position.y > nextPlatformCheck - (initialPlatforms * platformSpacingY / 2))
        {
            SpawnPlatform(new Vector3(Random.Range(-1.7f, 1.7f), nextPlatformCheck, 0));
            nextPlatformCheck += platformSpacingY;
        }

        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (platform.transform.position.y < PlayerController.instance.transform.position.y + despawnYThreshold)
            {
                Destroy(platform);
            }
        }

    }

    void SpawnPlatform(Vector3 position)
    {
        GameObject prefabToSpawn = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        if (Random.value < boostPlatformChance)
        {
            prefabToSpawn = boostPlatformPrefab;
        }

        Instantiate(prefabToSpawn, position, Quaternion.identity);
        //int platformIndex = Random.Range(0, platformPrefabs.Length);
        //Instantiate(platformPrefabs[platformIndex], position, Quaternion.identity);
    }
}
