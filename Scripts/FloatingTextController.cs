using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    private static FloatingText popupText;
    private static GameObject canvas;
    public static Vector2 screenPosition;

    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popupText)
          popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");   
    }


  public static void CreateFloatingText (string text, Transform location, float distance, bool crit)
    {
        FloatingText instance = Instantiate(popupText);

        instance.transform.SetParent(canvas.transform,false);
        instance.transform.position = screenPosition;
       // instance.SetColor(crit);
        instance.SetText(text);
        instance.SetSize(distance);
      
    }
}
