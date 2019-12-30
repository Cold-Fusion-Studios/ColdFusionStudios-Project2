using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CritFloatingText : MonoBehaviour
{
    public Animator animator;
    private Text damageText;
    //  private ChangeColor color;
    //  public Color32 critshot;
    //  public Color32 bodyshot;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
        //     color = animator.GetComponent<ChangeColor>();
        //    damageText.color = bodyshot;

    }
    public void SetText(string text)
    {
        damageText.text = text;
    }

    public void SetSize(float distance)
    {

        if (distance >= 25)
        {
            //    damageText.color = new Color(1, 1, 1, 1);
            damageText.fontSize = (int)(500 / distance);

        }
        else if (distance < 25 && distance > 15)
        {
            //  damageText.color = new Color(1, 1, 1, 1);
            damageText.fontSize = 20;
        }
        else if (distance <= 15 && distance > 5)
        {
            damageText.fontSize = (int)(300 / distance);
            //    damageText.color = new Color(1, 1, 1,1);

        }
        else
        {
            //  damageText.color = new Color(1, 1, 1, 1);
            damageText.fontSize = 60;
        }
     //   Debug.Log(damageText.fontSize);
    }
}
