using UnityEngine;

public class Player : Unit
{
    public void Update()
    {
        Vector3 mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
        LookAt(mousePos);
    }

    //I don't destroy the Player script to prevent some bugs from happening
    public override void Die()
    {
        Debug.Log("DEAD");
        gameObject.SetActive(false);
    }
}
