using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    Rigidbody2D enemyRigidBody;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        enemyRigidBody.velocity = new Vector2(movementSpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        movementSpeed = -movementSpeed;
        FlipEnemyFacing();
    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
}
