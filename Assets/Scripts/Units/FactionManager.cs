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
                    || faction2 == Faction.Outlaw 
                    || faction2 == Faction.Monster;

            case Faction.Republic:
                return faction2 == Faction.KaiserReich
                    || faction2 == Faction.Outlaw 
                    || faction2 == Faction.Monster;

            case Faction.Commonwealth:
                return faction2 == Faction.Outlaw 
                    || faction2 == Faction.Monster;

            case Faction.Outlaw:
                return faction2 == Faction.KaiserReich 
                    || faction2 == Faction.Republic 
                    || faction2 == Faction.Commonwealth 
                    || faction2 == Faction.Neutral 
                    || faction2 == Faction.Monster;

            case Faction.Neutral:
                return faction2 == Faction.Outlaw 
                    || faction2 == Faction.Monster;


            case Faction.Monster:
                return faction2 == Faction.KaiserReich
                    || faction2 == Faction.Republic
                    || faction2 == Faction.Commonwealth
                    || faction2 == Faction.Outlaw
                    || faction2 == Faction.Neutral;

            default: return false;

        }

    }


}