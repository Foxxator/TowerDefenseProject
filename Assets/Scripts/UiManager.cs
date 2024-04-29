using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI remainingEnemiestxt;
    public TextMeshProUGUI remainingHPtxt;
    public TextMeshProUGUI Exptxt;

    public void UpdateUI(int remainingEnemies, int remainingHP, int Exp)
    {
        remainingEnemiestxt.text = "Ennemis restants : " + remainingEnemies;
        remainingHPtxt.text = "HP : " + remainingHP;
        Exptxt.text = "Exp : " + Exp;
    }
}
