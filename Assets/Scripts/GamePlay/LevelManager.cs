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
    public Text textoRubies;
    int estrellasConseguidas = 0;
    int puntos = 0;
    int puntosASumar = 10;
   public  bool canBoostGoDown = true;

    float position; // posicion de spawn de bolas y deadzone

    private void Start() {
        gameManager = GameManager.getGM();
        gameManager.setRubiesListener(this.gameObject);
        deadZone.Init(this,ballsink);
        spawner.Init(this);
        ballsink.Init(this);
        boardManager.Init(gameManager.getTilesize(),readMap,this);
        readMap.Init(this);
        deadZone.FirstFakeBall();
        updateRubies();
    }

    public void NewShot()
    {
        puntosASumar = 10;
        deadZone.DeleteFirstFakeBall();
        deadZone.ResetPosition();
    }
    public void bloqueRoto()
    {
        puntos += puntosASumar;
        puntosASumar += 10;
        puntos_bar.value = puntos/3000.0f;
        if(puntos_bar.value >= 0.01f && estrellasConseguidas<1)
        {
            estrellas[0].enabled = true;
            estrellasConseguidas = 1;
        }
        if (puntos_bar.value >= 0.6f && estrellasConseguidas < 2)
        {
            estrellas[1].enabled = true;
            estrellasConseguidas = 2;

        }
        if (puntos_bar.value >= 0.99f && estrellasConseguidas < 3)
        {
            estrellas[2].enabled = true;
            estrellasConseguidas = 3;
        }
    }
    public void nuevoNivel()
    {
        puntos = 0;
        puntos_bar.value = puntos;
        gameManager.setLevelRate(estrellasConseguidas);
        estrellasConseguidas = 0;
        foreach (Image st in estrellas)
        {
            st.enabled = false;
        }
       
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
        if (canBoostGoDown)
        {
            deadZone.FirstFakeBall();
            boardManager.takeDownAllBalls();
        }
    }
    public BoardManager getBM()
    {
        return boardManager;
    }
    public void updateRubies()
    {
        textoRubies.text = gameManager.GetRubies().ToString ();
    }
}
