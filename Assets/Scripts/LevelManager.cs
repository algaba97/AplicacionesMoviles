using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

   public BallSink ballsink;
    public Spawner spawner;
    //AimController aimcontroller;
    public DeadZone deadZone;
    public BoardManager boardManager;
    

    private void Start()
    {
        deadZone.Init(this);
        spawner.Init(this);
    }

    public void NewShot()
    {
        deadZone.ResetPosition();
    }
     public Vector3 GetPosition()
    {
        return ballsink.getPosition();
    }
}
