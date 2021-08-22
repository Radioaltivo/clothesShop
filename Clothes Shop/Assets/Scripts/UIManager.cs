using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI currentMoneyTMP;
    [SerializeField] TextMeshProUGUI moneyToSpendTMP;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
    }

    public void UpdateMoneyToSpend(int amount)
    {
        if (amount < 1)
        {
            moneyToSpendTMP.gameObject.SetActive(false);
        }
        else
        {
            if (!moneyToSpendTMP.isActiveAndEnabled)
            {
                moneyToSpendTMP.gameObject.SetActive(true);
            }
            moneyToSpendTMP.text = "-" + amount;
        }
    }

    public void UpdateMoney(int amount)
    {
        currentMoneyTMP.text = amount.ToString();
    } 

}