using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : UIScreen
{
    [SerializeField] private Button back;

    [SerializeField] private UIScreen menuScreen;

    public override void SetupScreen(UIScreen previousScreen)
    {

        base.StartScreen();
        back.AddListener(LeavingShop);
    }

    void LeavingShop()
    {
        CloseScreen();
        menuScreen.StartScreen();
    }
}
