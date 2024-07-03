using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameDifferenceShader : MonoBehaviour
{
    [SerializeField] RawImage img;
    [SerializeField] Material material;
    [SerializeField] RenderTexture prev;
    [SerializeField] RenderTexture current;

    void Update()
    {
        img.texture = current;
        material.SetTexture("_PreviousFrame", prev);
        Graphics.CopyTexture(current, prev);
    }
}
