using UnityEngine;

public class Interactable2D : Interactable
{
    [SerializeField]
    private int sortOrder;

    protected sealed override bool CollisionIs2D => true;

    public int SortOrder => sortOrder + SortOrderAdjustment;

    public int SortOrderAdjustment { get; set; }

    public virtual int CompareInteractionSortOrder(Interactable2D other)
    {
        return other.SortOrder - SortOrder;
    }
}
