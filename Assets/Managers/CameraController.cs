using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isLock = false;

    public float PanSpeed = 30f;
    public float PanBorderTick = 10f;
    public float ScrollSpeed = 5f;
    public float MinY = 10f;
    public float MaxY = 80f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isLock = !isLock;

        if (isLock) return;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - PanBorderTick)
            transform.Translate(Vector3.up * PanSpeed * Time.deltaTime);

        else if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= PanBorderTick)
            transform.Translate(Vector3.down * PanSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - PanBorderTick)
            transform.Translate(Vector3.right * PanSpeed * Time.deltaTime);

        else if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= PanBorderTick)
            transform.Translate(Vector3.left * PanSpeed * Time.deltaTime);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Vector3 vec = transform.position;
            vec.y -= scroll * 1000f * ScrollSpeed * Time.deltaTime;

            //  min = 0, max = 10;
            //  vec.y = 5
            //  vec.y 11 -> max -> vec.y = 10
            //  해당 값을 보정해주는 함수
            vec.y = Mathf.Clamp(vec.y, MinY, MaxY);
            transform.position = vec;
        }
    }


}
