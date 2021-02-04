using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanelController : MonoBehaviour
{
    //public Sprite heart_0, heart_6;
    //public Image heart1_img, heart2_img, heart3_img;
    public GameObject heart1, heart2, heart3;
    public Animator heart1_animator, heart2_animator, heart3_animator;

    #region UpdateHearts()
    // updates hearts sprites
    public void UpdateHearts(int health)
    {

        // draw 3 full hearts
        //if (health == 3)
        //{
        //    return;
        //}

        if (health == 2)
        {
            // draw 2 full hearts
            heart3_animator.SetTrigger("TakeDamage");
            //heart2_img.sprite = heart_0;
            //heart1_img.sprite = heart_0;
            return;
        }

        if (health == 1)
        {
            // draw 1 full heart
            //heart3_img.sprite = heart_6;
            heart3_animator.SetTrigger("TakeDamage");
            heart2_animator.SetTrigger("TakeDamage");
            //heart1_img.sprite = heart_0;
            return;
        }

        if (health <= 0)
        {
            // draw 0 full hearts
            //heart3_img.sprite = heart_6;
            //heart2_img.sprite = heart_6;
            heart3_animator.SetTrigger("TakeDamage");
            heart2_animator.SetTrigger("TakeDamage");
            heart1_animator.SetTrigger("TakeDamage");
            return;
        }
    }
    #endregion
}
