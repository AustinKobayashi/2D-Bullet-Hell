using UnityEngine;
using UnityEngine.Networking;

public class BasicAttackMovement : AbstractProjectileMovement {
    /*
     * Can't find _wizardAttack on the client, so wizard attacks for client.
     */
        public BasicAttackMovement()
        {
            TargetTag = "Enemy";
        }

}