using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LayerMask unitsLayer;
    [SerializeField] private LayerMask levelLayer;
    [SerializeField] private LayerMask shootThroughLayer;
    [SerializeField] private LayerMask inventoryLayer;

    public static LayerMask UnitsLayer => Instance.unitsLayer; 
    public static LayerMask LevelLayer => Instance.levelLayer;
    public static LayerMask ShootThroughLayer => Instance.shootThroughLayer;
    public static LayerMask InventoryLayer => Instance.inventoryLayer;

}
