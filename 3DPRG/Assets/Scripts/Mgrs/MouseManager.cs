using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    public static MouseManager Instance { get => instance; }
    public event Action<Vector3> onmouseclicked;

    public Texture2D pointTexture, doorwayTexture, attackTexture, targetTexture, arrowTexture;
    RaycastHit hitInfo;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
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
            switch(hitInfo.collider.tag)
            {
                case "Ground":
                    Cursor.SetCursor(targetTexture, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
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
