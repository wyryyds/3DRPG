using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public EventVector3 onmouseclicked;
    RaycastHit hitInfo;

    private void Update()
    {
        SetMouseTexture();
        MouseControl();
    }
    /// <summary>
    /// 切换鼠标样式
    /// </summary>
    void SetMouseTexture()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hitInfo))
        {
            //
        }
    }
    /// <summary>
    /// 鼠标移动
    /// </summary>
    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.tag == "Ground") onmouseclicked?.Invoke(hitInfo.point);
        }
    }

}
