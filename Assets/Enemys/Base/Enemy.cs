using UnityEngine;

public class Enemy : EnemyTransform
{
    public int MaxHP;
    public int Prize;
    protected int currentHp;

    public void OnHit(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
            SpawnManager.Get.DestroyEnemy(this);
    }

}
