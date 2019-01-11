using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockLogic : MonoBehaviour {
    LevelManager LM;
    private TextMesh texto;
    public GameObject particles;
    public Vector2 position;
   
    // Use this for initialization
    void Start () {
        texto = GetComponentInChildren<TextMesh>();
        if(GetComponent<Block>().getVida() != 0)
        texto.text = GetComponent<Block>().getVida().ToString();
    }
    public void Init(LevelManager aux,int i, int j)
    {
        LM = aux;
        position = new Vector2(j,i);
    }

    public int getY()
    {
        position.y += 1.0f;
        return (int)position.y;

    }
    public bool canDestroy() // devuelve si el bloque está en la posicion mayor es decir ya ha spawnead y está desactivado
    {
        return position.y >= 0;
    }
    public bool gameOver() // Comprueba si llegas a la ultima posicion por abajo que hace que pierdas el juego
    {
        return position.y >= 12;
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GetComponent<Block>().Touch())
            {
                Destroy(Instantiate(particles,transform.position,new Quaternion(0,0,0,0)),0.5f);
                //Instantiate(particles,transform.position,new Quaternion(0,0,0,0));
                LM.bloqueRoto();
                this.gameObject.SetActive(false);
               
            }

            actualizaTexto();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LM.getBM().bolasAMeter++;
            LM.ballsink.sumaCont();
           // LM.getBM().numeroTiles--;
            Destroy(gameObject);
        }
    }
    public void setVida(int _v){
        GetComponent<Block>().setVida(_v);
    }
    public void actualizaTexto()
    {
        texto.text = GetComponent<Block>().getVida().ToString();

    }
}
