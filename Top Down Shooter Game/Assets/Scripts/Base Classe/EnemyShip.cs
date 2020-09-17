using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShip : Ship
{
    [Header("AI variables")]
    public AIPath aiPath;
    public AIDestinationSetter aiDestination;

    public void TargetAINull()
    {
        if (aiDestination.target == null)
        {
            aiDestination.target = GameInstances.GetPlayer().transform;
        }
    }
}
