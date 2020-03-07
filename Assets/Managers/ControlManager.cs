using UnityEngine;

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
                            return;
                        }
                        else 
                            prevNode.MaterialColor = Color.white;
                    }
                    
                    n.MaterialColor = NodeManager.Get.SelectColor;
                    prevNode = n;
                }
            }
        }
    }

}
