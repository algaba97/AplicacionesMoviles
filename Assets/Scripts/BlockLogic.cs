using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockLogic : MonoBehaviour {
    LevelManager LM;
    private TextMesh texto;
	// Use this for initialization
	void Start () {
        texto = GetComponentInChildren<TextMesh>();
        if(GetComponent<Block>().getVida() != 0)
        texto.text = GetComponent<Block>().getVida().ToString();
    }
    public void Init(LevelManager aux)
    {
        LM = aux;
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
                LM.bloqueRoto();
                this.gameObject.SetActive(false);
            }
            
            texto.text = GetComponent<Block>().getVida().ToString();

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("MasBolaspls");
    }
    public void setVida(int _v){
        GetComponent<Block>().setVida(_v);
    }
}
