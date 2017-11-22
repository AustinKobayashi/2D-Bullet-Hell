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

        public override void hit(Collider2D coll) {
            if (!isServer) return;
            
            EnemyStatsTest enemyStats = coll.gameObject.GetComponent<EnemyStatsTest> ();

            // did player kill the enemy
            bool kill = false;

            if (enemyStats != null) 
                kill = enemyStats.TakeDamage(Damage);
            if (kill && Player != null)
                Player.GetComponent<AbstractPlayerStats>().IncreaseExperience(enemyStats.GetExperienceGain());
        }

}