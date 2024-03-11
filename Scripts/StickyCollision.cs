using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyCollision : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void changeParent(Transform parent) {
        transform.parent = parent;
    }

    private void OnCollisionEnter2D(Collision2D other) {

        // suggestion: if you're going to use this check again, make it into a function
        // if other is unstuck, a player, or an enemy, and gameObj is unstuck
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Stuck") && gameObject.tag == "Unstuck") {
            transform.parent = other.transform;
            gameObject.tag = "Stuck";

            if (GetComponent<LifeCounter>() != null) {
                GetComponent<LifeCounter>().team = other.gameObject.GetComponent<LifeCounter>().team;
            }

            GetComponent<Rigidbody2D>().isKinematic = true;

            if (other.gameObject.tag == "Player") {
                StatTracker.mergePart();
                AudioManager.instance.playSound("JoinPart");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.tag == "Unstuck" && other.gameObject.tag == "Player") {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
