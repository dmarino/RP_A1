using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : PickUpParent
{
    override protected void PickedUp(Collider2D other)
    {
        other.GetComponent<ShipController>()?.PickUpPowerUp();
    }
}
