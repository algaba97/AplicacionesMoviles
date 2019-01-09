using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

   public BallSink ballsink;
    
    public Spawner spawner;
    public DeadZone deadZone;
    public BoardManager boardManager;
    GameManager gameManager;
    public ReadMap readMap;
    public Slider puntos_bar;
    public Image[] estrellas;
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
        puntos_bar.value = puntos/3000.0f;
        if(puntos_bar.value >= 0.01f)
        {
            estrellas[0].enabled = true;
        }
        if (puntos_bar.value >= 0.6f)
        {
            estrellas[1].enabled = true;
        }
         if (puntos_bar.value >= 0.99f)
        {
            estrellas[2].enabled = true;

        }
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
    public void duplicateVelocity()
    {
        boardManager.duplicateVel();
    }
    public void takeDownAllBalls()
    {
        boardManager.takeDownAllBalls();

    }
    public BoardManager getBM()
    {
        return boardManager;
    }
}
