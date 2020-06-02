using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : PickUpParent
{
    override protected void PickedUp(Collider2D other)
    {
        ScoreManager.instance.MoneyCollected();
    }
}
