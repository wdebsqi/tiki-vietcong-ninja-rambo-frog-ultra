using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanelController : MonoBehaviour
{
    public Sprite heartFullSprite, heartEmptySprite;
    public Image heart1, heart2, heart3;

    #region UpdateHearts()
    // updates hearts sprites
    public void UpdateHearts(int health)
    {
        // draw 3 full hearts
        if (health == 3)
        {
            heart3.sprite = heartFullSprite;
            heart2.sprite = heartFullSprite;
            heart1.sprite = heartFullSprite;
            return;
        }

        if (health == 2)
        {
            // draw 2 full hearts
            heart3.sprite = heartEmptySprite;
            heart2.sprite = heartFullSprite;
            heart1.sprite = heartFullSprite;
            return;
        }

        if (health == 1)
        {
            // draw 1 full heart
            heart3.sprite = heartEmptySprite;
            heart2.sprite = heartEmptySprite;
            heart1.sprite = heartFullSprite;
            return;
        }

        if (health <= 0)
        {
            // draw 0 full hearts
            heart3.sprite = heartEmptySprite;
            heart2.sprite = heartEmptySprite;
            heart1.sprite = heartEmptySprite;
            return;
        }
    }
    #endregion
}
