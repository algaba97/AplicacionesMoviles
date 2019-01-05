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
    public void setVida(int _v){
        GetComponent<Block>().setVida(_v);
    }
}
