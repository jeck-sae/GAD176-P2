using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public interface IItemContainer
{
    public int LoadItems(Item item, int amount);
    public int ContainsItem(string id);
    public int RemoveItem(string id, int amount);

    public Unit GetOwner();
    public void SetOwner(Unit owner);
}
