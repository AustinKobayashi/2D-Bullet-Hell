using UnityEngine;

public class BasicAttackMovement : AbstractProjectileMovement {
    /*
     * Can't find _wizardAttack on the client, so wizard attacks for client.
     * TODO: Fix _wizardAttack networking issue so the client can see that class.
     */
    private WizardAttack _wizardAttack;
        public BasicAttackMovement()
        {
            targetTag = "Enemy";
        }

        public override void hit(Collider2D coll) {
                _wizardAttack.CmdDealDamage(coll.gameObject);
        }
        public void SetPlayerAttack(WizardAttack wizardAttack){
            this._wizardAttack = wizardAttack;
        }
}