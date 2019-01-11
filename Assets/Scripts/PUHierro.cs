using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PUHierro : MonoBehaviour
   {
    public BoxCollider2D bloqueH;
    GameObject[] powerUP;
    bool activo = false;
    // Start is called before the first frame update
    void Start()
    {
        powerUP = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doPowerUP()
    {
        int signo = 1;
        int offset = 0;
        if(GetComponent<BoardManager>().GetPosBola() > 0)
        {
            signo = -1;
            offset = 2;
        }
        if (!activo && GameManager.getGM().addPowerUp("Hierro",-1))
        {

            for (int i = 0; i < powerUP.Length; i++)
            {
                GameObject b = Instantiate(bloqueH.gameObject);
                float Tsize = GameManager.getGM().getTilesize();
                b.transform.localScale = new Vector2(Tsize, Tsize);
                b.transform.position = new Vector3(signo*(i+ offset ) * Tsize + Tsize/2, -6 * Tsize);
                powerUP[i] = b;

            }
            activo = true;
        }
    }
    public void removePowerUp()
    {
        foreach(GameObject b in powerUP){
            Destroy(b);
        }
        activo = false;
    }

}
