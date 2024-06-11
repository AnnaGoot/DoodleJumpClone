using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlatformSpawn platformSpawn;

    private void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        GameObject playerInstance = Instantiate(playerPrefab);
        PlayerController playerController = playerInstance.GetComponent<PlayerController>();

        //platformSpawn.Init(playerController);
    }
}
