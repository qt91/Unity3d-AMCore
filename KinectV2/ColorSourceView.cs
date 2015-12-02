﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class ColorSourceView : MonoBehaviour
{
    public GameObject ColorSourceManager;
    private ColorSourceManager _ColorManager;
    
    void Start ()
    {
        //gameObject.GetComponent<RawImage>().material.SetTextureScale("_MainTex", new Vector2(-1, 1));

    }
    
    void Update()
    {
        if (ColorSourceManager == null)
        {
            return;
        }
        
        _ColorManager = ColorSourceManager.GetComponent<ColorSourceManager>();
        if (_ColorManager == null)
        {
            return;
        }
        
        gameObject.GetComponent<RawImage>().texture = _ColorManager.GetColorTexture();
    }
}
