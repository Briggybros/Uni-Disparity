using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {

    protected SkinnedMeshRenderer sr;
    protected Image im;
    private bool sra = true;
    Texture2D mColorSwapTex;
    Color[] mSpriteColors;

    // Use this for initialization
    void Awake () {
        sr = this.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>();
        if (!sr)
        {
            Debug.Log("false");
            sra = false;
            im = GetComponent<Image>();
        }
	}

    //Colour changing stuff
    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;
        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();
        if (!sra) im.material.SetTexture("_SwapTex", colorSwapTex);
        else sr.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public void SwapColor(int rVal, Color color)
    {
        mSpriteColors[rVal] = color;
        mColorSwapTex.SetPixel(rVal, 0, color);
        mColorSwapTex.Apply();
    }
}
