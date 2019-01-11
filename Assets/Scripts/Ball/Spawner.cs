using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    LevelManager LM;
    // Use this for initialization

    private Vector2 forceVector;
    [Range(1.0f, 500.0f)]
    [Tooltip("ball Speed. Value between 1 and 10.")]
    [Header("Bola")]
    public float Fuerza = 5f;
    public GameObject balls;
    float pos;
    float posBola;
    GameManager gameManager;
	void Start () {
        gameManager = GameManager.getGM();
      
       

    }
  
    
   
    public void Init(LevelManager aux)
    {
        LM = aux;


    }
    // Update is called once per frame
    void Update () {

        Vector3 fin;
        if (gameManager.getState() == 1 && Input.GetMouseButtonUp(0) )
        {

            //&& LM.boardManager.roundIsEnd()
            pos = LM.GetPosition();
            fin = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y,0));
                //el screentoworldpoint, te transforma las coordenadas del raton(1920x1080) a un punto de la escena
            if (pos< fin.y && LM.boardManager.roundIsEnd())
            {
                //Lllama al levelManager para un nuevo disparo
                balls.transform.localScale = new Vector3(gameManager.getTilesize(), gameManager.getTilesize(), gameManager.getTilesize());

                LM.NewShot();
                posBola = LM.boardManager.GetPosBola();


                forceVector = new Vector2(fin.x - posBola, fin.y - pos);



                //Borramos los bloques destruidos

                SpawnBalls((uint)LM.getBM().nBolas, forceVector);


                //hacer un for con el numero actual de bolas que hay en el nivel, y llamar con el invoke o con una
                //coroutine al metodo instance balls para instanciar una bola y darle la fuerza.
            }
        }
	}

    void SpawnBalls(uint numballs,Vector2 dir) // Lo llamará posiblemente otro objeto no desde aquí dentro
    {
        StartCoroutine(instanceBalls(numballs,dir));
    }
    IEnumerator instanceBalls(uint numBalls, Vector2 dir){
        LM.canBoostGoDown = false;
        float ballsize;//El tamaño de la bola, afecta en la creación debiudo a que si no spwanea en el propio collider de la deadzone
        ballsize = balls.transform.localScale.y /2;

        for (int i = 0; i < numBalls; i++)
        {
            
            GameObject aux = Instantiate(balls, new Vector3(posBola, pos +ballsize, 0), Quaternion.identity);
            // aux.transform.localScale = new Vector2(Screen.width/10000.0f, Screen.width / 10000.0f);
            aux.GetComponent<BallLogic>().setForce(forceVector.normalized * Fuerza);
            LM.getBM().addBall(aux.GetComponent<BallLogic>());
            yield return new WaitForFixedUpdate();// //TODO mas que un fixedUpdate
            yield return new WaitForFixedUpdate();// //TODO mas que un fixedUpdate
            yield return new WaitForFixedUpdate();// //TODO mas que un fixedUpdate
        }
        LM.canBoostGoDown = true;
      

  

    }

    

}
