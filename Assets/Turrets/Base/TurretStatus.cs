using UnityEngine;

//  추상 클래스
//  이 클래스는 단독으로 사용할 수 없다!
//  누군가가 상속받아 구현해야 사용할 수 있다
public abstract class TurretStatus : MonoBehaviour
{
    public enum TurretType { Standard = 0, Missile, Laser }

    [Header("Attributes")]
    public TurretType turretType;
    public int Damage;
    public float Range;
    public float FireRate;
    public int Price;

    [Header("Bullet Attributes")]
    [SerializeField] protected float BulletSpeed;
    [SerializeField] protected Transform Neck;
    [SerializeField] protected GameObject Bullet;
    [SerializeField] protected Transform BulletPoint;

    protected Enemy target = null;
    private float reloadTime = 0;

    private void Update()
    {
        //  적을 찾는다 (타겟을 찾는다)
        FindTarget();

        //  타겟이 있으면 이 타워의 Neck 을 돌려 타겟과 내 포신의 위치를 맞춰준다
        if (target != null)
        {
            Vector3 normal = (target.transform.position - transform.position).normalized;

            Neck.rotation =
                Quaternion.LookRotation(new Vector3(normal.x, 0, normal.z));
        }

        if (reloadTime >= FireRate)
        {
            if (target == null) return;
            else
            {
                Shoot();
                reloadTime = 0f;
            }
        }
        else 
            reloadTime += Time.deltaTime;
    }

    //  abstract ?
    //  추상화. 이 요소를 사용하는 경우 이 클래스는 반드시 추상 클래스여야 함
    //  상속받는 클래스는 반드시 추상 함수를 구현해야함!
    protected abstract void Shoot();

    //  virtual ?
    //  가상화. 가상 함수.
    //  상속받는 클래스가 재구현 할 수도 있고 아니면 그냥 사용할 수도 있다
    protected virtual void FindTarget()
    {
        //  타워(이 컴포넌트가 들어있는 타워)가 쫒아다니는 타겟이 있다면
        if (target != null)
        {
            //  사정거리를 계산하여 사정거리 내에 있다면 그냥 종료. 계속 쫒아다녀라
            if (Vector2.Distance(target.transform.position, transform.position) <= Range)
                return;
        }

        //  사정거리를 벗어나거나 타겟이 null 이라면 타겟을 재 갱신
        target = SpawnManager.Get.GetEnemyInRange(transform.position, Range);
    }

    //  이 객체가 선택되면 해당 함수를 실행한다
    private void OnDrawGizmosSelected()
    {
        //  Gizmos ?
        //  Gizmos 이름이 붙은 함수 안에서만 사용할 수 있는 특별한 클래스
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
