using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private PlatformSpawn platformSpawnGameObject;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        var playerInstance = Instantiate(playerPrefab);
        var platformSpawner = new GameObject("Spawner").AddComponent<PlatformSpawn>();

        platformSpawner.SetPlayer(playerInstance);

    }
}
