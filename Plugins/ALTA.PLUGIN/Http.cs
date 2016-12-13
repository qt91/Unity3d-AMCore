using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Http : MonoBehaviour
{

    public WWW GET(string url, RequsetSuccess function, RequestErr ErrFunction = null)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, function, ErrFunction));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post, RequsetSuccess function, RequestErr ErrFunction = null)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, function, ErrFunction));
        return www;
    }
    public void UploadFile(byte[] localFile, string uploadURL, RequsetSuccess function, RequestErr ErrFunction = null)
    {
        //WWW localFile = new WWW("file:///" + localFileName);
        //yield return localFile;
        //if (localFile.error == null)
        //    Debug.Log("Loaded file successfully");
        //else
        //{
        //    Debug.Log("Open file error: " + localFile.error);
        //    yield break; // stop the coroutine here
        //}
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        postForm.AddBinaryData("file", localFile, "photo_" + DateTime.Now.Ticks + ".jpg", "image/png");
        WWW upload = new WWW(uploadURL, postForm);
        StartCoroutine(WaitForRequest(upload, function, ErrFunction));
    }

    public delegate void RequsetSuccess(WWW www);
    public delegate void RequestErr(WWW ww);

    private IEnumerator WaitForRequest(WWW www, RequsetSuccess function, RequestErr ErrFunction)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            if (function != null)
                function(www);
//            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            if (ErrFunction != null)
                ErrFunction(www);
            //Debug.Log("WWW Error: " + www.error);
        }
    }
}
