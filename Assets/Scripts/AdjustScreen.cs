using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScreen : MonoBehaviour {
    /// <summary>
    /// Script para ajustar el canvas de la pantalla para todo tipo de resolución y Aspect Ratio,
    /// el canvas consiste en 3 partes, Arriba, medio y abajo, además, arriba y abajo tienen un panel
    /// con diferentes botones/imagenes, que tambien se escalan.
    /// </summary>
    public bool menu;
    public RectTransform CanvasA; 
    public RectTransform CanvasB; 
    public RectTransform CanvasC;
    public RectTransform PanelTop; 
    public RectTransform PanelBottom;

    public static AdjustScreen instance;


    void Start () {
        instance = this;
        float auxTam = Mathf.Min((float)Screen.width / 11, (float)Screen.height / 18);
        float margenY_ = (Screen.height - auxTam * 14) / 2;
        CanvasA.sizeDelta = new Vector2(Screen.width, margenY_);
        CanvasB.sizeDelta = new Vector2(Screen.width, margenY_);
        if (menu)
        CanvasC.sizeDelta = new Vector2(Screen.width - ((Screen.width - auxTam * 11)), Screen.height - margenY_ * 2);
        else
            CanvasC.sizeDelta = new Vector2(Screen.width , Screen.height - margenY_ * 2);
        Vector2 rd3 = PanelTop.sizeDelta;
        float tamqq = Mathf.Min((auxTam * 11) / rd3.x, margenY_ / rd3.y);
        PanelTop.localScale = new Vector2(tamqq, tamqq);
        PanelBottom.localScale = new Vector2(tamqq, tamqq);
    }

    public Vector2 getMiddleCanvasSize()
    {
        return CanvasC.sizeDelta;
    }

}
