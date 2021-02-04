using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    #region References
    [Header("Animator")]
    public Animator animator;
    #endregion

    #region Events
    public UnityEvent onDamageTaken;
    #endregion

    #region Enemy settings
    [Header("Enemy settings")]
    public int health = 3;
    #endregion

    #region Boolean variables
    public bool isDead = false;
    #endregion

    // Function - Health - makes enemy take damage
    #region TakeDamage(int damage)
    public void TakeDamage(int damage)
    {
        Debug.Log("Took damage");
        animator.SetBool("isDamaged", true);
        StartCoroutine(waitForDamageAnimation(0.175f));
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Score.scoreValue += 1;
            Die();
        }
    }
    #endregion

    // Function - Health - makes enemy die when on 0 health
    #region Die()
    public void Die()
    {
        onDamageTaken.Invoke();
        animator.SetTrigger("Dead");
        Destroy(gameObject, 0.350f);
    }
    #endregion

    // Function - Health - makes enemy die when entering Pit coordinates
    #region PitDie
    public void PitDie()
    {
        if (!isDead)
        {
            onDamageTaken.Invoke();
            isDead = true;
            Score.scoreValue -= 1;
            Destroy(gameObject);
        }
    }
    #endregion


    // IEnumerator function - Animation - is waiting for end of death animation
    #region waitForAnimation()
    IEnumerator waitForDamageAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isDamaged", false);

    }
    #endregion
}

