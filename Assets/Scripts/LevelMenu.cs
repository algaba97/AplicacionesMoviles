using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public static LevelMenu instance;
    float auxTam;
    float margenX;
    float margenY;
    public RectTransform CanvasA;
    public RectTransform CanvasB;
    public RectTransform CanvasC;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
         auxTam = Mathf.Min((float)Screen.width / 11, (float)Screen.height / 18);
       
        margenY = (Screen.height - auxTam * 14) / 2;
        margenX = (Screen.width - auxTam * 11) / 2;
        CanvasA.sizeDelta = new Vector2(Screen.width, margenY);
        CanvasB.sizeDelta = new Vector2(Screen.width, margenY);
        CanvasC.sizeDelta = new Vector2(Screen.width-margenX*2, Screen.height - margenY * 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 getScrollViewSize()
    {
        return CanvasC.sizeDelta;
    }
}
