using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSink : MonoBehaviour {

    float position;// posaicion x , la y siempre será igual
    public Text label;
    private uint _numballs;
    void Reset( )
    {
        _numballs = 0;
    }
    void Hide()
    {
        label.gameObject.SetActive(false);
    }
    void Show()
    {
        label.gameObject.SetActive(false);
    }
    void SetPosition(float pos)
    {
        position = pos;
    }
}
