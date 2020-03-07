using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Monobehaviour ?
//  유니티에서 컴포넌트화 할 때 필요한 개체
public class Node : MonoBehaviour
{
    public Color selectColor;
    public bool isSelect;
    private MeshRenderer tileRenderer;

    //  접근자 설정자 ? (getter, setter) Function
    public Color MaterialColor
    {
        get { return tileRenderer.material.color; }
        set { tileRenderer.material.color = value; }
    }

    //  게임을 시작할 때 단 한번만 호출됨!
    private void Awake()
    {
        //  GetComponent ?
        //  이 스크립트가 들어간 개체의 MeshRenderer 라는 컴포넌트를 가져온다
        tileRenderer = GetComponent<MeshRenderer>();
    }

}
