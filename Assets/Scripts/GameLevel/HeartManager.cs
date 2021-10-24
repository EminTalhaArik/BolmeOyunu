using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hearts = new GameObject[3];

    public void HeartControl(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(false);
        }

        for (int i = 0; i < health; i++)
        {
            hearts[i].SetActive(true);
        }
    }
}
