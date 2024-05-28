using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MenuButton
{
    void Start()
    {
        onClick = () => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
