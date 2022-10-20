using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public int damage;

   
    private bool movingToEnd; //moving to end position
    private SpriteRenderer sprite;
    private bool UpDown;

    private void Start()
    {
        startPosition = transform.position;
        movingToEnd = true;

        sprite = gameObject.transform.Find("crab-idle-1").GetComponent<SpriteRenderer>();

        //Moving to right
        if ((int)startPosition.x < (int)endPosition.x)
        {
            sprite.flipX = true;
        }
        else if (startPosition.x > endPosition.x)
        {
            sprite.flipX = false;
        }

        
    }
    private void Update()
    {
        EnemyMove();
        

    }

    void EnemyMove()
    {
        //calculate the destination in order to movingToEnd
        Vector3 targetPosition = (movingToEnd) ? endPosition : startPosition;

        //enemy movement
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            movingToEnd = !movingToEnd;
            if (!UpDown) sprite.flipX = !sprite.flipX;
        }

        
        /*
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            //when arrive to the end
            if (transform.position == endPosition)
                movingToEnd = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
                movingToEnd = true;
        }
        */

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
    
}
