using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public Transform enemyTransform;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public bool isHasDrop;
    public GameObject drop;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void OnEnable()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 & isHasDrop == false)
        {
            this.gameObject.SetActive(false);
        }
        if (health <= 0 & isHasDrop == true)
        {
            this.gameObject.SetActive(false);
            drop.SetActive(true);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.GetComponent<Enemy>().currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
