using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private float distance = 2f;
    public Vector3 startPos;
    private bool movingRight = true;
    void Start()
    {
        this.startPos = transform.position;
    }

    void Update()
    {

        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (this.movingRight)
        {
            transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                this.movingRight = false;
                this.Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                this.movingRight = true;
                this.Flip();
            }
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
