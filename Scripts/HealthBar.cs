using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public GameObject ship;
    public GameObject fillArea;

    private LifeCounter shipLife;
    private Slider healthBar;
    private int maxLife;

    // Start is called before the first frame update
    void Start() {
        shipLife = ship.GetComponent<LifeCounter>();
        healthBar = GetComponent<Slider>();

        maxLife = shipLife.lifeCounter;
    }

    // Update is called once per frame
    void Update() {

        /*
        if (ship)
            fillArea.SetActive(false);
        else {
            healthBar.value = 1.f * shipLife.lifeCounter / maxLife;
            fillArea.SetActive(false);
        }
        */
        if (ship != null) {
            healthBar.value = (float)shipLife.lifeCounter / maxLife;
            if (healthBar.value == 0) {
                fillArea.SetActive(false);
            }
        } else {
            fillArea.SetActive(false);
        }
        
    }
}
