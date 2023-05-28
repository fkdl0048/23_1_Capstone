using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNPC : MonoBehaviour
{
    [SerializeField]
    private RectTransform beFollowedRectTransform;
    [SerializeField]
    private RectTransform followerRectTransform;

    private void Update()
    {
        LateUpdate();
    }

    private void LateUpdate()
    {
        //Vector3 parentPos = beFollowedRectTransform.position;
        //Vector2 parentSize = beFollowedRectTransform.sizeDelta;
        //Vector3 localPos = canvasRectTransform.InverseTransformPoint(parentPos);
        //transform.localPosition = localPos;
        //transform.localScale = Vector3.one;
        //GetComponent<RectTransform>().sizeDelta = parentSize;
        followerRectTransform.position = beFollowedRectTransform.position + new Vector3(0, followerRectTransform.sizeDelta.y/5, 0);
    }
}
