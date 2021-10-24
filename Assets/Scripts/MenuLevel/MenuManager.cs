using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject startButton, exitButton;

    void Start()
    {
        FadeOut();
    }

    private void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1f, 0.7f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1f, 0.7f).SetDelay(0.6f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
