using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class cameraController : MonoBehaviour
{

    public GameObject target = null; // ī�޶� ����ٴ� Ÿ��

    [SerializeField]
    float CameraSpeed = 10.0f;       // ī�޶��� �ӵ�

    Vector3 TargetPos;                      // Ÿ���� ��ġ

    // Update is called once per frame
    void LateUpdate()
    {

        if (target != null)
        {
            Vector3 dir = target.transform.position - this.transform.position;
            Vector3 moveVector = new Vector3(dir.x * CameraSpeed * Time.deltaTime, dir.y * CameraSpeed * Time.deltaTime, 0.0f);
            this.transform.Translate(moveVector);
        }
        
    }

}
