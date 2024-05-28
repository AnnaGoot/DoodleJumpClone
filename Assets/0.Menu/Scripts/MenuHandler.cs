using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private UIScreen mainMenu;
    [SerializeField] private UIScreen _shop;


    private void Start()
    {
        mainMenu.StartScreen();
        //_shop.StartScreen();
    }
}
