using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AltaControllerInterFace : MonoBehaviour {
    public delegate void CompleteRequest(WWW www);
    public delegate void ErrorRequest(WWW www);
	// Use this for initialization
    public WWW GET(string url, CompleteRequest FunctionRequest, ErrorRequest Err = null)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, FunctionRequest, Err));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post, CompleteRequest FunctionRequest, ErrorRequest Err = null)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, FunctionRequest, Err));
        return www;
    }

    public IEnumerator WaitForRequest(WWW www, CompleteRequest FunctionRequest, ErrorRequest Err)
    {
        yield return www;
        if (www.error == null)
        {
            if (FunctionRequest != null)
                FunctionRequest(www);
        }
        else
        {
            if (Err != null)
                Err(www);
        }
    }
}
