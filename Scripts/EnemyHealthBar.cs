using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {
    public GameObject healthBar;
    public GameObject fillBar;
    public GameObject ship;
    public GameObject thisObject;

    public float offset;

    private LifeCounter lifeCounter;
    private int maxLife;

    // Start is called before the first frame update
    void Start() {
        lifeCounter = ship.GetComponent<LifeCounter>();
        maxLife = lifeCounter.lifeCounter;
    }

    // Update is called once per frame
    void Update() {
        fillBar.transform.localScale = new Vector3(((float) lifeCounter.lifeCounter / maxLife), 1, 1);
        healthBar.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + offset, ship.transform.position.z);
        healthBar.transform.rotation = Quaternion.identity;

        if (lifeCounter.lifeCounter <= 0) {
            Destroy(gameObject);
        }
    }
}
