using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Enemy
{
    private Rigidbody2D myRigidbody;

    public float chaseRadius;
    public float attackRadius;

    public Transform homePosition;
    public Transform target;

    public Animator anim;

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        health = maxHealth.initialValue;
        this.gameObject.transform.position = new Vector3(homePosition.position.x, homePosition.position.y, homePosition.position.z);
        currentState = EnemyState.idle;
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    private void Update()
    {
        if (isHasDrop)
        {
            drop.transform.position = new Vector3(enemyTransform.position.x, enemyTransform.position.y, enemyTransform.position.z);
        }
        else
        {
            return;
        }
    }
    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position,
            transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk
                && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) 
        {
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y > 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newstate)
    {
        if (currentState != newstate)
        {
            currentState = newstate;
        }
    }
}
