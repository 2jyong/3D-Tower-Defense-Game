using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Enemy target = null;
    protected float speed = 0f;
    protected int damage = 0;

    public void SetTarget(Enemy target, int damage, float speed)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;

        Vector3 normal = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(normal);
    }

    private void Update()
    {
        if (target == null) { Destroy(gameObject); return; }

        Vector3 vec = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(vec);

        float point = (speed * Time.deltaTime) * 1.5f;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(target.transform.position.x, target.transform.position.z)) <= point)
            target.OnHit(damage);

        //if (Vector3.Distance(transform.position, target.transform.position) <= point)
        //    target.OnHit(damage);
    }

}
