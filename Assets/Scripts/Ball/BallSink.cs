using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSink : MonoBehaviour {

    float position;// posaicion x , la y siempre será igual
    public  float y;
    public Text label;
    private uint _numballs;
    bool first = false;
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
    public void SetPosition(float pos)
    {
        position = pos;
    }
   public Vector3 getPosition()
    {

        return new Vector3(position, y, 0);
    }
    public void llega(BallLogic ball)
    {
        Destroy(ball.gameObject);
        Debug.Log("hE LLEGADO");
    }
}
