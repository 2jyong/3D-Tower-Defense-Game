using UnityEngine;

public class StandardTurret : TurretStatus
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

    //  함수 오버라이딩
    //  부모의 함수 그대로 사용하지 않고 자식에서 함수를 재구현 하는 방법
    //protected override void FindTarget()
    //{
        
    //}
}
