﻿using UnityEngine;
public class CoverDissolve : PostEffectsBase
{

    static readonly string shaderName = "lcl/screenEffect/MaskEffect";

    // 遮罩大小
    [Range(0.01f, 10f), Tooltip("遮罩大小")]
    public float size = 5.0f;
    // 边缘模糊程度
    [Range(0.0001f, 0.1f), Tooltip("边缘模糊程度")]
    public float edgeBlurLength = 0.05f;
    // 遮罩中心位置
    private Vector2 pos = new Vector4(0.5f, 0.5f);

    void OnEnable()
    {
        shader = Shader.Find(shaderName);
    }

    // 渲染屏幕
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material)
        {
            // 把鼠标坐标传递给shader
            material.SetVector("_Pos", pos);
            // 遮罩大小
            material.SetFloat("_Size", size);
            // 模糊程度
            material.SetFloat("_EdgeBlurLength", edgeBlurLength);
            // 渲染
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            //将mousePos转化为（0，1）区间
            pos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
        }
    }
}