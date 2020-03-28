using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Get
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private void Awake()
    {
        lifes = new List<RectTransform>();

        //  최초 생성될 라이프 개수 * 2
        int length = 5;
        for (int i = 0; i < length; ++i)
        {
            Vector2 vec = new Vector2(0, -(i * 32));
            //  i = 0 ? Vector2(0, 0);
            //  i = 1 ? Vector2(0, -32);
            //  i = 2 ? Vector2(0, -64);

            RectTransform rt = Instantiate(lifePrefab, lifeTransform).GetComponent<RectTransform>();
            rt.localPosition = vec;
            lifes.Add(rt);

            vec.x = 32;

            rt = Instantiate(lifePrefab, lifeTransform).GetComponent<RectTransform>();
            rt.localPosition = vec;
            lifes.Add(rt);
        }

        Money = 100;
    }

    public RectTransform lifeTransform = null;
    public GameObject lifePrefab = null;
    public Text moneyText = null;

    private int money;
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            moneyText.text = "<b>Money</b>\n" + money.ToString();
        }
    }

    private List<RectTransform> lifes = null;
    public void AddLife()
    {
        if (lifes.Count == 0) return;

        RectTransform rt = Instantiate(lifePrefab, lifeTransform).GetComponent<RectTransform>();

        Vector3 lastVec = lifes[lifes.Count - 1].localPosition;
        if (lastVec.x == 0f)
        {
            rt.localPosition = new Vector3(32, lastVec.y);
        }
        else
        {
            rt.localPosition = new Vector3(0, lastVec.y - 32f);
        }
        
        lifes.Add(rt);
    }

    public void RemoveLife()
    {
        if (lifes.Count == 0) return;

        //  List 안에 life 가 두 개 들어있다고 가정
        //  첫 번째 라이프는 lifes[0] 에 들어있다, ex) 0, 1
        //  Count 는 진짜 개수를 반환하기 때문에, lifes.Count == 2 를 반환
        //  그래서 리스트의 마지막 원소를 접근하기 위해서는 lifes.Count - 1 로 접근이 가능!
        RectTransform value = lifes[lifes.Count - 1];

        //  인덱스로 삭제하는 방법
        lifes.RemoveAt(lifes.Count - 1);

        Destroy(value.gameObject);
    }

    //  public float money = 10f;
    //  money = 9.9999999999....9f
    //  money = 10f - 9f = 9.99999..f - 8.9999987f .. = 1f = 0.999999f;
    //  decimal //  소수점 5칸만!

    private bool isFoldLife = false;
    public void OnFoldLife()
    {
        //  if (isFoldLife == true)

        //if (isFoldLife) isFoldLife = false;
        //else isFoldLife = true;

        isFoldLife = !isFoldLife;
        if (isFoldLife)
        {
            lifeTransform.localPosition = new Vector3(
                lifeTransform.localPosition.x,
                -375f);
        }
        else
        {
            lifeTransform.localPosition = new Vector3(
                lifeTransform.localPosition.x,
                0f);
        }
    }



}
