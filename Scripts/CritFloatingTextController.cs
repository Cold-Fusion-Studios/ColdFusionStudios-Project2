using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritFloatingTextController : MonoBehaviour
{
    private static CritFloatingText popupText;
    private static GameObject canvas;
    public static Vector2 screenPosition;

    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText)
        {
           // Debug.Log("gotthisfar1");
            popupText = Resources.Load<CritFloatingText>("Prefabs/CritTextParent");
          //  Debug.Log("gotthisfar");
        }
    }


    public static void CreateFloatingText(string text, Transform location, float distance, bool crit)
    {
        CritFloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        // instance.SetColor(crit);
        instance.SetText(text);
        instance.SetSize(distance);

    }
}
