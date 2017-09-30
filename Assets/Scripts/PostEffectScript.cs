using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectScript : MonoBehaviour {

    public Material mat;
    public Color lerpedColor;
    private Color eveningColor = new Vector4(0.275f, 0.510f, 0.706f, 1);

    private float playSpeed = 0.009f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // src is the fuller rendered scene that you would normally
        // send directly to the monitor. Intercept it and do more work
        // before passing it on
        lerpedColor = Color.Lerp(Color.white, eveningColor, Mathf.PingPong(Time.time * playSpeed, 1));
        Debug.Log(lerpedColor);

        mat.SetColor("_Color", lerpedColor);
        Graphics.Blit(src, dest, mat);
    }
}
