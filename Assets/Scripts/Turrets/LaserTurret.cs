using UnityEngine;

public class LaserTurret : TurretStatus
{
    public LineRenderer Line;

    protected override void Shoot()
    {
        Line.SetPosition(0, BulletPoint.position);
        Line.SetPosition(1, target.transform.position);

        target.OnHit(Damage);
    }

    protected override void FindTarget()
    {
        base.FindTarget();

        if (target == null) Line.enabled = false;
        else Line.enabled = true;
    }

}
