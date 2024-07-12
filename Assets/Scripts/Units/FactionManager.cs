using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : Singleton<FactionManager>
{
    public bool IsEnemyOf(Faction faction1, Faction faction2)
    {
        switch (faction1)
        {
            case Faction.KaiserReich:
                return faction2 == Faction.Republic
                    || faction2 == Faction.Outlaw;

            case Faction.Republic:
                return faction2 == Faction.KaiserReich
                    || faction2 == Faction.Outlaw;

            case Faction.Commonwealth:
                return faction2 == Faction.Outlaw;

            case Faction.Outlaw:
                return faction2 == Faction.KaiserReich 
                    || faction2 == Faction.Republic 
                    || faction2 == Faction.Commonwealth 
                    || faction2 == Faction.Neutral;

            case Faction.Neutral:
                return faction2 == Faction.Outlaw;

            default: return false;

        }

    }


}