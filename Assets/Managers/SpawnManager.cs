using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Get { get; set; } = null;

    public GameObject[] Prefabs;
    public float RoundWaitTime = 10f;   //  끝나고 대기할 시간
    public float SpawnTime = 1.5f;      //  한 마리 뽑을 때 마다 대기할 시간
    public int SpawnLimit = 10;         //  최대 몬스터 개수
    public Text TextButton;
    public Slider NextSlider;

    //  유니티와 시리얼라이즈를 하지 말라는 속성
    [NonSerialized] public int roundCount = 0;

    //  List?
    //  배열처럼 처음 선언할 때 개수에 정함이 없고, 삽입, 삭제가 용이한 클래스
    private List<Enemy> enemies = new List<Enemy>();

    public Enemy GetEnemyInRange(Vector3 position, float range)
    {
        //return enemies.Find(
        //    x => Vector3.Distance(x.transform.position, position) <= range);

        for (int i = 0; i < enemies.Count; ++i)
        {
            float distance =
                Vector3.Distance(
                    enemies[i].transform.position,
                    position);

            if (distance <= range) return enemies[i];
        }

        return null;
    }

    public void DestroyEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void Awake()
    {
        Get = this;
    }

    public void OnStart()
    {
        //  StartCoroutine ?

        StartCoroutine(UpdateSpawnTimer(SpawnTime));
    }

    //  반복자
    private IEnumerator UpdateSpawnTimer(float checkTime)
    {
        TextButton.text = "In Coming ..";

        int count = 0;
        float currTime;
        while (count++ != SpawnLimit)
        {
            currTime = Time.time;
            while (Time.time - currTime <= checkTime)
            {
                yield return null;  //  한 프레임 쉬고 다시 실행
            }

            SpawnEnemy(0);
        }

        while (enemies.Count != 0)
            yield return null;

        roundCount++;
        TextButton.text = string.Format("Next {0} Round", roundCount);
        NextSlider.value = 0;

        currTime = Time.time;
        while (Time.time - currTime <= RoundWaitTime)
        {
            NextSlider.value = (Time.time - currTime) / RoundWaitTime;
            yield return null;
        }

        StartCoroutine(UpdateSpawnTimer(SpawnTime));
        yield break;
    }

    private void SpawnEnemy(int index)
    {
        //  Instantiate
        //  복사본을 생성한다
        GameObject g = Instantiate(
            Prefabs[index],
            WaypointManager.Get.GetWaypoint(0).position, // 웨이포인트의 첫 번째 위치로 이동
            Quaternion.identity,    //  회전 값을 초기화 한다(0, 0, 0)
            null);

        enemies.Add(g.GetComponent<Enemy>());

        //  Quaternion.identity
        //  단위행렬
        //  Ex : 4x4 기준
        //  1 0 0 0
        //  0 1 0 0
        //  0 0 1 0
        //  0 0 0 1
    }

}
