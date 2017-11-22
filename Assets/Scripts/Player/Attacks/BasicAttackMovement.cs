using UnityEngine;
using UnityEngine.Networking;

public class BasicAttackMovement : AbstractProjectileMovement {
    /*
     * Can't find _wizardAttack on the client, so wizard attacks for client.
     */
    private WizardAttack _wizardAttack;

        public BasicAttackMovement()
        {
            targetTag = "Enemy";
        }

        public override void hit(Collider2D coll) {
            if (!isServer) return;
            _wizardAttack.CmdDealDamage(coll.gameObject);
        }
        public void SetPlayerAttack(WizardAttack wizardAttack) {
            this._wizardAttack = wizardAttack;
        }
}