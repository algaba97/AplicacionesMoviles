using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text rubies;
    public Text stars;
    public Text PUbomba;
    public Text PUhierro;
    public Slider volume;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.getGM().setRubiesListener(this.gameObject);
        updateTexts();
        volume.value = GameManager.getGM().getVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void updateTexts()
    {
        stars.text = GameManager.getGM().getStars().ToString();
        updateRubies();
        updatePowerUpsText();
    }
    void updateRubies()
    {
        rubies.text = GameManager.getGM().GetRubies().ToString();

    }
    public void updatePowerUpsText()
    {
        PUhierro.text = "x" + GameManager.getGM().getPowerUp("Hierro").ToString();
        PUbomba.text = "x" + GameManager.getGM().getPowerUp("Bomba").ToString();

    }
    public void buyPUHierro()
    {
        if(GameManager.getGM().addRubies(-100))
        GameManager.getGM().addPowerUp("Hierro", 1);
    }
    public void buyPUBomba()
    {
        if(GameManager.getGM().addRubies(-100))
        GameManager.getGM().addPowerUp("Bomba", 1);

    }
    public void setVolume()
    {
        GameManager.getGM().setVolumen(volume.value);
    }
}
