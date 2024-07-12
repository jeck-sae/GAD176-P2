using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LayerMask unitsLayer;
    [SerializeField] private LayerMask levelLayer;
    
    public static LayerMask UnitsLayer => Instance.unitsLayer; 
    public static LayerMask LevelLayer => Instance.levelLayer;



}
