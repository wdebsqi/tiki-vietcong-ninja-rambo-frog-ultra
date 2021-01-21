using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    // Function - Health - kill on trigger
    #region OnTriggerEnter2D(Collider2D other)
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealthManager enemy = other.GetComponent<EnemyHealthManager>();
        if(enemy != null)
        {
            enemy.PitDie();
        }

    }
    #endregion
}
