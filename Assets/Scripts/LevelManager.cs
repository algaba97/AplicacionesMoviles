using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

   public BallSink ballsink;
    public Spawner spawner;
    //AimController aimcontroller;
    public DeadZone deadZone;
    public BoardManager boardManager;
    GameManager gameManager;
    public ReadMap readMap;

    private void Start() {
        gameManager = GameManager.getGM();
        deadZone.Init(this);
        spawner.Init(this);
        ballsink.Init(this);
        boardManager.Init(gameManager.getTilesize(),readMap);
    }

    public void NewShot()
    {
        deadZone.ResetPosition();
    }
     public Vector3 GetPosition()
    {
        return ballsink.getPosition();
    }
    public void llega(BallLogic bl)
    {
        Debug.Log("Holaaa");
    }
}
