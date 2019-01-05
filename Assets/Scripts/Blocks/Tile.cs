using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    LevelManager lmanager;
    void Init(LevelManager LM)
    {
        lmanager = LM;
    }

    public virtual bool CanFall()
    {
        return fall;
    }
    public virtual bool Touch()
    {
        _pendingtouches--;
        return _pendingtouches <= 0;
    }
    public virtual void setVida(int v)
    {
        _pendingtouches = v;
    }
    public virtual int getVida()
    {
        return _pendingtouches;
    }
   
   protected int _pendingtouches;
    protected bool fall = true;

}
