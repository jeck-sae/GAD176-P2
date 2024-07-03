using UnityEngine;

public class Player : Unit
{
    //I don't destroy the Player script to prevent some bugs from happening
    public override void Die()
    {
        Debug.Log("DEAD");
        gameObject.SetActive(false);
    }
}
