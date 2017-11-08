using UnityEngine;


public class BasicAttackMovement : AbstractProjectileMovement {
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