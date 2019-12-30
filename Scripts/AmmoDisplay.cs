using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoDisplay : MonoBehaviour
{
    public int ammotodisplay;
string percent;
public Text displayAmmo;
    public int wpnNumtoDisplay;

// Update is called once per frame
void Update()
{
        displayAmmo.text = "Ammo: " + ammotodisplay.ToString();
        
    }

public void changeAmmo(int ammo)
    {
        ammotodisplay =  ammo;
    }


}
