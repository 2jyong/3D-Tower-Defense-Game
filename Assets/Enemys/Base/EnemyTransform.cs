using UnityEngine;

public class EnemyTransform : MonoBehaviour
{
    public float Speed;

    private Transform checkPoint;
    private int index = 1;

    public void Start()
    {
        transform.position = WaypointManager.Get.GetWaypoint(0).position;
        checkPoint = WaypointManager.Get.GetWaypoint(index);

        SetDirection();
    }

    //  오브젝트의 이동이나 물리 연산의 경우 여기에 처리한다
    private void FixedUpdate()
    {
        if (checkPoint == null) return;

        //  Translate ? 이동시키는 함수
        float point = (Speed * Time.deltaTime) * 1.5f;
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, checkPoint.position) <= point)
            NextPoint();
    }

    private void SetDirection()
    {
        //  내 위치와 상대 위치를 뺀 값을 정규화(단위벡터) 시켜준다
        //  방향을 구하기 위해 사용. norvalVec = Vector / Vector Scale
        Vector3 vec = (checkPoint.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(vec);
    }

    private void NextPoint()
    {
        transform.position = checkPoint.position;
        checkPoint = WaypointManager.Get.GetWaypoint(++index);

        if (checkPoint == null)
        {
            //  Minus Life ..
        }
        else SetDirection();
    }

}
