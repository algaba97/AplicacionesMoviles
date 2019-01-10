using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSink : MonoBehaviour {

    float position;// posaicion x , la y siempre será igual
    public TextMesh label;
    int contador;
    LevelManager LM;
    public void Init(LevelManager aux)
    {
        LM = aux;
        position = 0.0f;
    }
 
    public void Hide()
    {
        label.gameObject.SetActive(false);
    }
   public void Show(float positionx,float positiony)
    {
        contador = 1;
        label.gameObject.SetActive(true);
        label.gameObject.transform.position = new Vector3(positionx, positiony + 0.5f, label.gameObject.transform.position.z);
    }
    public void SetPosition(float pos)
    {
        position = pos;
    }
   public Vector3 getPosition()
    {

        return new Vector3(position, LM.GetPosition(), 0);
    }
    public void llega(BallLogic ball)
    {
        contador++;
        label.text = ""+contador;
        LM.boardManager.Ball(ball.gameObject);
        Destroy(ball.gameObject);
    }
}
