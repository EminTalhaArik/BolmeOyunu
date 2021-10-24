using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AlphaPanel : MonoBehaviour
{

    [SerializeField] GameObject alphaPanel;

    private void Start()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        alphaPanel.GetComponent<CanvasGroup>().DOFade(0, 2f);
    }
}
