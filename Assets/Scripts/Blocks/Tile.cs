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
        return _pendingtouches == 0;
    }
   
   protected uint _pendingtouches;
    protected bool fall = true;

}
