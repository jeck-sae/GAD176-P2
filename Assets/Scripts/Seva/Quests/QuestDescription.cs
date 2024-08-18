using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [HideInInspector] public string StoredDescription;

    public void SeeTheDescription()
    {
        QuestManager.Instance.ShowDescription(StoredDescription);
    }
}
