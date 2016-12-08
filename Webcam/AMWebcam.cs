using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RawImage))]
public class AMWebcam : MonoBehaviour {
    public static AMWebcam Static;
    private WebCamTexture webCamTexture;
    void Awake()
    {
        Static = this;
    }
    void Start () {
        //webCamTexture = new WebCamTexture();
        ////gameObject.GetComponent<RawImage>().texture = webCamTexture;
        //gameObject.GetComponent<RawImage>().material.mainTexture = webCamTexture;
        //webCamTexture.Play();
        StartCoroutine(PlayCam());
    }
    private IEnumerator PlayCam()
    {
        yield return null;
        yield return new WaitForEndOfFrame();
        //cameraFeed = new WebCamTexture(512, 512);
        webCamTexture = new WebCamTexture(1920,1080);
        gameObject.GetComponent<RawImage>().texture = webCamTexture;

        webCamTexture.Play(); // Second time this is called - Crash/Freeze
    }


    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    CloseWebCam();
        //}
	}

    public void CloseWebCam()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
            GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 1.0f));
        }
    }
}
