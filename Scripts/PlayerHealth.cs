
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float health = 100;
    private void Start()
    {
        health = 100;
    }

    private void FixedUpdate()
    {
        if(health > 100)
        {
            health = 100;
        }
       // if (Input.GetButtonDown("Fire1"))
            {
           // Debug.Log("Fire");
        }


    }

    public void TakeDamage(float damage)
    { if (health > 0)
        {
            health = health - damage;
            healthBar.fillAmount = health / 100;
        }
   //     Debug.Log(health);
    }
}