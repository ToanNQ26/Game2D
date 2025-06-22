using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    protected GameManager gameManager;
    protected GameObject posDead;
    protected AudioManager audioManager;

    private bool isDead;

    public float killOffset = 0.5f;

    protected TutorialDialogue tutorialDialogue;

    void Awake()
    {
        this.isDead = false;
        this.posDead = GameObject.Find("PosDead");
        this.tutorialDialogue = FindAnyObjectByType<TutorialDialogue>();
        this.gameManager = FindAnyObjectByType<GameManager>();
        this.audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Update()
    {
        if (!this.isDead && this.CheckDead())
        {
            isDead = true;
            this.CheckOverGame();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            audioManager.PlayCoinSound();
            this.gameManager.AddScore(1);
        }
        else if (collision.CompareTag("trap"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            gameManager.GameWin();
        }
        else if (collision.CompareTag("Intruction"))
        {
            this.tutorialDialogue.ShowInstruction();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("hehe");
            float playerY = transform.position.y;
            float enemyY = collision.collider.transform.position.y;

            if (playerY > enemyY + killOffset)
            {
                // Player nhảy lên đầu Enemy → tiêu diệt
                collision.collider.GetComponent<Enemy>()?.Die();

                // Bật Player lên lại 1 lực nhẹ
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 10f); // lực nhảy lại
                }
            }
            else
            {
                gameManager.GameOver();
            }
        }
    }

    void CheckOverGame()
    {
        if (CheckDead()) gameManager.GameOver();
    }

    public bool CheckDead()
    {
        return this.transform.position.y < posDead.transform.position.y;
        
    }
}
