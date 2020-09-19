using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShip : Ship
{
    [Header("AI variables")]
    public AIPath aiPath;
    public AIDestinationSetter aiDestination;

    public void DisableFindingPlayer()
    {
        if (state == ShipState.DISABLED || GameInstances.GetPlayer().state == ShipState.DISABLED || Spawner.spawnerInstance.gameFinish == true)
        {
            aiDestination.enabled = false;
            aiPath.enabled = false;
        }
    }

    public void TargetAINull()
    {
        if (aiDestination.target == null)
        {
            aiDestination.target = GameInstances.GetPlayer().transform;
        }
    }
}
