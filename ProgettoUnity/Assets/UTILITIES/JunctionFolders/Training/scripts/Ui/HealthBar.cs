using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image currentHealth;
    public Text ratioHp;

    private float maxHp = 150;
    private float currentHp = 150;

    public void UpdateHealthBar()
    {
        float ratio = currentHp / maxHp;
        currentHealth.rectTransform.localScale = new Vector3(ratio,1, 1);
        ratioHp.text = (ratio*100).ToString("0")+"%";
    }


}
