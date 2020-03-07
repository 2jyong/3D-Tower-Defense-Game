using UnityEngine;

public class NodeManager : MonoBehaviour
{
    //  기본 접근자!
    //  private
    //  나만 사용가능! 그 누구도 건드리지 못함

    //  protected
    //  나와 나를 상속받는 개체만 접근 가능

    //  public
    //  누구나 접근 가능

    //  static ? 전역화 (Singleton Pattan)
    private static NodeManager instance = null;
    public static NodeManager Get
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<NodeManager>();
            return instance;
        }
    }

    public Color SelectColor;
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Node n = transform.GetChild(i).gameObject.AddComponent<Node>();
        }
    }
}
