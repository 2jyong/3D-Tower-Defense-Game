using System;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject HitParticle = null;
    public float ExplosionRadius = 10f;

    protected override void OnHit()
    {
        GameObject p = Instantiate(HitParticle,
            transform.position,
            transform.rotation) as GameObject;

        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].CompareTag("Enemy"))
                colliders[i].GetComponent<Enemy>().OnHit(damage);
        }

        Destroy(p, 2f);
    }

}
