using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthManager : MonoBehaviour
{
    #region References
    [Header("Animator")]
    public Animator animator;
    #endregion

    #region Events
    public UnityEvent onDamageTaken;
    #endregion

    #region Player settings
    [Header("Player settings")]
    public int health = 3;
    public int timeForDamageCooldown = 1;
    #endregion

    #region Boolean variables
    public bool isDead = false;
    public bool isHit = false;
    #endregion

    // Function - Health - makes player take damage
    #region TakeDamage(int damage)
    public void TakeDamage(int damage)
    {
        Debug.Log("Player took damage");
        animator.SetBool("isDamaged", true);
        StartCoroutine(waitForDamageAnimation(0.175f));
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Die();
        }
        StartCoroutine(takingDamageCooldown(timeForDamageCooldown));
    }
    #endregion

    // Function - Health - makes player die when on 0 health
    #region Die()
    public void Die()
    {
        Debug.Log("Player died");
        onDamageTaken.Invoke();
        animator.SetTrigger("Dead");
        Destroy(gameObject, 0.350f);
    }
    #endregion

    // Function - Health - makes player die when entering Pit coordinates
    #region PitDie
    public void PitDie()
    {
        Debug.Log("Player died in the pit");
        health = 0;
        onDamageTaken.Invoke();
        isDead = true;
        Destroy(gameObject);
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


    // IEnumerator function - Health - makes you immortal for some time
    #region takingDamageCooldown()
    IEnumerator takingDamageCooldown(int time)
    {
        yield return new WaitForSeconds(time);
        isHit = false;
    }
    #endregion
}
