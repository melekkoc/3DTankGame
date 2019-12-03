using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBehaviour : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;
    float Local_health = 100;

    public void TakeDamage(float temp)
    {
        Local_health -= temp;
        if (Local_health > 0 )
            healthText.text = string.Format("%{0}", Local_health);

        else
            healthText.text = string.Format("%{0}", 0);

        healthBar.fillAmount = Local_health / 100f;

  
        if (Local_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

