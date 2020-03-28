
public class Enemy : EnemyTransform
{
    public int Prize;
    public int Hp;

    public void OnHit(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            SpawnManager.Get.DestroyEnemy(this);
            GameManager.Get.Money += Prize;
        }
    }

}
