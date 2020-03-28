using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    private static ControlManager instance = null;
    public ControlManager Get { get { return instance; } }

    //  선언되고 단 한번만 실행
    private void Awake()
    {
        instance = this;
    }

    //  비활성화 되었다가 다시 활성화 되었을 때
    //  또는 처음 활성화 되어있을 때
    //private void OnEnable()
    //{
    //}

    //  Awake, OnEnable 보다 늦게 호출됨
    //  Update 전에 호출된다
    //private void Start()
    //{
    //}

    public RectTransform ClickMenu = null;
    public GameObject BuyMenu = null;
    public GameObject OtherMenu = null;
    public Text CostText = null;

    private Node prevNode = null;

    //  매 프레임(?) 마다 실행되는 함수
    private void Update()
    {
        //  0 : 왼쪽 버튼 클릭, 1 : 오른쪽, 2 : 마우스 휠
        //  Input ? 사용자의 입력을 받는 클래스
        //  Input.GetMouseButton(0) 마우스의 키값이 들어와 있으면 true 를 반환 - 꾹 누른다
        //  Input.GetMouseButtonDown(0) 마우스의 키값이 들어와 있으면 단 한 번만 true - 클릭 한 번
        //  Input.GetMouseButtonUp(0) 해당 마우스 버튼을 눌럿다가 뗏을 때 true - 눌렀다 뗏을 때

        if (Input.GetMouseButtonDown(0))
        {
            if (ClickMenu.gameObject.activeSelf) return;

            //  마우스의 위치를 스크린 상의 포인트 좌표로 변환한다
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //  변환된 위치(ray) 를 가지고 해당 포인트 위치부터 Z 축으로 레이저를 쏴준다
            //  out, ref - 숙제
            //  out ? 값을 파라메터로 반환시켜준다 - 반환할 값이 많을 경우
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null) return;
                if (hit.collider.gameObject.GetComponent<Node>() is Node n)
                {
                    if (prevNode != null)
                    {
                        if (prevNode.Equals(n))
                        {
                            n.MaterialColor = Color.white;
                            prevNode = null;

                            ClickMenu.gameObject.SetActive(false);
                            return;
                        }
                        else
                            prevNode.MaterialColor = Color.white;
                    }

                    n.MaterialColor = NodeManager.Get.SelectColor;
                    prevNode = n;

                    ClickMenu.anchoredPosition = Input.mousePosition;
                    ShowMenu();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
            OnClose();
    }

    public void OnClose()
    {
        ClickMenu.gameObject.SetActive(false);

        if (prevNode != null)
        {
            prevNode.MaterialColor = Color.white;
            prevNode = null;
        }
    }

    private void ShowMenu()
    {
        ClickMenu.gameObject.SetActive(true);
        BuyMenu.SetActive(false);
        OtherMenu.SetActive(false);

        if (prevNode.transform.childCount == 0) BuyMenu.SetActive(true);
        else
        {
            Transform t = prevNode.transform.GetChild(0);
            TurretStatus status = t.GetComponent<TurretStatus>();
            
            switch (status.turretType)
            {
                case TurretStatus.TurretType.Standard:
                    CostText.text = CostString(status.Price);
                    break;

                case TurretStatus.TurretType.Missile:
                    CostText.text = CostString(status.Price + 1);
                    break;

                case TurretStatus.TurretType.Laser:
                    CostText.text = CostString(status.Price + 3);
                    break;
            }
            
            OtherMenu.SetActive(true);
        }

        string CostString(int price)
        {
            int value = (int)(price * .75f);
            return $"$ {value.ToString()}";
        }
    }

    public void OnUpgradeButton()
    {
        string[] split = CostText.text.Split('$');
        //  split[0] = '';
        //  split[1] = 해당 금액

        int cost = int.Parse(split[1]);
        if (!IsCheck(cost)) return;
        
        TurretStatus turret = prevNode.GetComponentInChildren<TurretStatus>();

        turret.Price += cost;
        turret.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

        switch (turret.turretType)
        {
            case TurretStatus.TurretType.Standard:
                turret.Damage += 2;
                turret.Range += 0.25f;
                break;

            case TurretStatus.TurretType.Missile:
                turret.Damage += 4;
                turret.Range += 0.25f;
                turret.FireRate -= 0.25f;
                break;

            case TurretStatus.TurretType.Laser:
                turret.Damage++;
                break;
        }

        OnClose();
    }

    public void OnSellButton()
    {

    }

    public void OnStandardTower()
    {
        if (!IsCheck(5)) return;

        var model = Resources.Load("Prefabs/Standard Turret");
        GameObject go = Instantiate(model, prevNode.transform) as GameObject;
        go.transform.localPosition = new Vector3(0, .5f, 0);
        go.transform.localScale = new Vector3(.5f, .5f, .5f);

        OnClose();
    }

    public void OnMissileTower()
    {
        if (!IsCheck(10)) return;

        var model = Resources.Load("Prefabs/Missile Turret");
        GameObject go = Instantiate(model, prevNode.transform) as GameObject;
        go.transform.localPosition = new Vector3(0, .5f, 0);
        go.transform.localScale = new Vector3(.5f, .5f, .5f);

        OnClose();
    }

    public void OnLaserTower()
    {
        if (!IsCheck(15)) return;

        var model = Resources.Load("Prefabs/Laser Turret");
        GameObject go = Instantiate(model, prevNode.transform) as GameObject;
        go.transform.localPosition = new Vector3(0, .5f, 0);
        go.transform.localScale = new Vector3(.5f, .5f, .5f);

        OnClose();
    }

    private bool IsCheck(int price)
    {
        if (prevNode == null) return false;
        if (GameManager.Get.Money >= price)
        {
            GameManager.Get.Money -= price;
            return true;
        }

        return false;
    }
    
}
