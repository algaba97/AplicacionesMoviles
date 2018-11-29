using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockLogic : MonoBehaviour {

    public int vida;
    private TextMesh texto;
	// Use this for initialization
	void Start () {
        texto = GetComponentInChildren<TextMesh>();
        texto.text = vida.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vida--;
            if (vida <= 0)
                this.gameObject.SetActive(false);
            
            texto.text = vida.ToString();

        }

    }
    public void setVida(int _v){
        vida = _v;
    }
}
