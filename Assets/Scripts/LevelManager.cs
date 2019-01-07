using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

   public BallSink ballsink;
    public Spawner spawner;
    //AimController aimcontroller;
    public DeadZone deadZone;
    public BoardManager boardManager;
    GameManager gameManager;
    public ReadMap readMap;
    public Text puntos_texto;
    int puntos = 0;
    int puntosASumar = 10;

    float position; // posicion de spawn de bolas y deadzone

    private void Start() {
        gameManager = GameManager.getGM();
        deadZone.Init(this);
        spawner.Init(this);
        ballsink.Init(this);
        boardManager.Init(gameManager.getTilesize(),readMap,this);
        readMap.Init(this);
        puntos_texto.text = "Puntos " + puntos.ToString();
    }

    public void NewShot()
    {
        puntosASumar = 10;
        deadZone.ResetPosition();
    }
    public void bloqueRoto()
    {
        puntos += puntosASumar;
        puntosASumar += 10;
        puntos_texto.text = "Puntos " + puntos.ToString();

    }
    public void nuevoNivel()
    {
        puntos = 0;
    }

    public void llega(BallLogic bl)
    {
        Debug.Log("Holaaa");
    }
    public void SetPosition(float aux)
    {
        position = aux;
    }
    public float GetPosition()
    {
        return position;
    }
    public void goToSelectLevel()
    {
        SceneManager.LoadScene(0);
    }
    public BoardManager getBM()
    {
        return boardManager;
    }
}
