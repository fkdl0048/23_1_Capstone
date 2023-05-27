using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class cameraController : MonoBehaviour
{

    public GameObject target = null; // 카메라가 따라다닐 타겟

    [SerializeField]
    float CameraSpeed = 10.0f;       // 카메라의 속도

    Vector3 TargetPos;                      // 타겟의 위치

    // Update is called once per frame
    void FixedUpdate()
    {

        if (target != null)
        {
            Vector3 dir = target.transform.position - this.transform.position;
            Vector3 moveVector = new Vector3(dir.x * CameraSpeed * Time.deltaTime, dir.y * CameraSpeed * Time.deltaTime, 0.0f);
            this.transform.Translate(moveVector);
        }
        
    }

}
