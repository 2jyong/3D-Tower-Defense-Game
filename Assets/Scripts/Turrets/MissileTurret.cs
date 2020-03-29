using UnityEngine;

public class MissileTurret : TurretStatus
{
    protected override void Shoot()
    {
        if (target == null) return;

        Bullet b = Instantiate(
            Bullet,
            BulletPoint.position,
            Quaternion.identity,
            null).GetComponent<Bullet>();

        b.SetTarget(target, Damage, BulletSpeed);
    }
}
