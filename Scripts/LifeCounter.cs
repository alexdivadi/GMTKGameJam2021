using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public int lifeCounter = 1;
    public string team;

    //private bool dead = false;


    private EnemySpawnController spawner;
    private UIManager ui;
    private int maxLife;
    // Start is called before the first frame update
    void Start()
    {
        ui = (UIManager)FindObjectOfType(typeof(UIManager));
        if (gameObject.tag == "Enemy")
            spawner = (EnemySpawnController)FindObjectOfType(typeof(EnemySpawnController));
        else
            spawner = null;
        maxLife = lifeCounter;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c != null) {
            if (c.gameObject.tag == "Projectile" && ((c.gameObject.GetComponent<ProjectileController>().parentTeam != team) && team != ""))
            {
                // Destroy the laserww
                Destroy(c.gameObject);
                lifeCounter -= c.gameObject.GetComponent<ProjectileController>().damage;
                if (c.gameObject.GetComponent<SpriteRenderer>().sprite.name == "Shield_0")
                {
                    AudioManager.instance.playSound("ShieldHit");
                }
                else
                {
                    AudioManager.instance.playSound(c.gameObject.GetComponent<ProjectileController>().sound);
                }
            }
            else if (!(team == "Player" && c.gameObject.GetComponent<LifeCounter>() && c.gameObject.GetComponent<LifeCounter>().team == "Player") && c.gameObject.tag != "Walls" && team != "" && c.gameObject.tag != "Projectile")
            {
                var momentum = c.gameObject.GetComponent<Rigidbody2D>().velocity + gameObject.GetComponent<Rigidbody2D>().velocity;
                c.gameObject.GetComponent<Rigidbody2D>().velocity = momentum / 2f;
                gameObject.GetComponent<Rigidbody2D>().velocity = momentum / 2f;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (lifeCounter <= 0)
        {
            // Update with death animation
            Die();
        }
    }

    void Die()
    {
        if (team == "Player" && gameObject.tag != "Player") {
            StatTracker.losePart();
        } else if (gameObject.tag == "Enemy") {
            StatTracker.kill();
            spawner.OnEnemyDestroyed();
        } else if (gameObject.tag == "Stuck") {
            AudioManager.instance.playSound("BreakOff");
        } else if (gameObject.tag == "Player") {
            gameObject.GetComponent<Animator>().SetTrigger("Explode");
            ui.GameOver();
        }

        if (gameObject.tag == "Stuck" && Random.value >= 0.25) {
            gameObject.tag = "Unstuck";
            team = "";
            lifeCounter = maxLife;

            StickyCollision[] colliders = GetComponentsInChildren<StickyCollision>();
            foreach (StickyCollision collider in colliders) {
                collider.changeParent(transform.parent);
            }
            
            transform.parent = null;

            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        } else {
            LifeCounter[] components = GetComponentsInChildren<LifeCounter>();
            foreach (LifeCounter lifeCounter in components) {
                if (lifeCounter != this) {
                    lifeCounter.Die();
                }
            }
            
            if (gameObject.tag != "Player") {
                Destroy(gameObject);
            }
        }
    }
}
