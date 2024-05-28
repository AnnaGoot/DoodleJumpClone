using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;

public abstract class UIScreen : MonoBehaviour
{
    private Image[] allTargetGraphic;
    //private bool canAnimate = true;

    public float animationDuration = 0.3f;

    void OnEnable()
    {
        allTargetGraphic = GetComponentsInChildren<Image>();


        // if (allTargetGraphic.Count() < 1)
        //   canAnimate = false;
        //else
        //  canAnimate = true;
    }

    public abstract void SetupScreen(UIScreen previousScreen);



    public virtual void StartScreen()
    {
        gameObject.SetActive(true);

        Color baseColor = new Color(255, 255, 255, 0);


        foreach (var image in allTargetGraphic)
        {
            image.DOFade(1, animationDuration);
        }
    }

    public void CloseScreen()
    {
        CloseScreenWithAwait();
    }

    private async void CloseScreenWithAwait()
    {
        foreach (var image in allTargetGraphic)
        {
            image.DOFade(0, animationDuration);
        }

        await Task.Delay((int)(animationDuration * 1000));

        gameObject.SetActive(false);
    }


}
