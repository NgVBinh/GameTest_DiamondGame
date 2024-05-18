using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public static DisplayManager instance;

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    [SerializeField] private Text textCoin;

    private int coin = 0;

    private void DisplayCoin()
    {
        textCoin.text = coin.ToString();
    }
    public void IncreaseCoin()
    {
        coin++;
        DisplayCoin();
    }
}
