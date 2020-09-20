using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadBillboard : MonoBehaviour
{
    public void Operate()
    {
        Managers.Images.GetWedImage(OnWebImages);
    }

    private void OnWebImages(Texture2D image)
    {
        GetComponent<Renderer>().material.mainTexture = image;
    }
}
