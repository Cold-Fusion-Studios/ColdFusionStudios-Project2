using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveDisplay : MonoBehaviour
{
    public Text ObjectiveToDisplay;
    public Text Obj;

    public WeaponPickup wpn;
    public WeaponPickup wpn1;
    public WeaponPickup wpn2;

    private void Start()
    {
        wpn.objdis = this.GetComponent<ObjectiveDisplay>();
        wpn1.objdis = this.GetComponent<ObjectiveDisplay>();
        wpn2.objdis = this.GetComponent<ObjectiveDisplay>();

    }

    public void showObjective(string objective, int time = 5)
    {
        Obj.CrossFadeAlpha(1.0f, 0.0f, false);
        ObjectiveToDisplay.text = objective;
        ObjectiveToDisplay.CrossFadeAlpha(1.0f, 0.0f, false);
        StartCoroutine(wait(time));
        
    }
    
    public void showUpdate(string update, int time = 5)
    {
        ObjectiveToDisplay.text = update;
        ObjectiveToDisplay.CrossFadeAlpha(1.0f, 0.0f, false);
        StartCoroutine(waitupdate(time));
    }

    IEnumerator wait(int time)
    {
        yield return new WaitForSeconds(time);
        ObjectiveToDisplay.CrossFadeAlpha(0.0f, 0.50f, false);
        Obj.CrossFadeAlpha(0.0f, 0.5f, false);
    }

    IEnumerator waitupdate(int time)
    {
        yield return new WaitForSeconds(time);
        ObjectiveToDisplay.CrossFadeAlpha(0.0f, 0.50f, false);
        
    }
}
