using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent(typeof(Text)), ExecuteInEditMode]
public class Font : MonoBehaviour {
    private Text txt;
    public string Text;
    public static FontIcons Fontdata;
    void Awake()
    {
        txt = GetComponent<Text>();
        Fontdata = ExCss.ReadFile(System.IO.Path.Combine(Application.streamingAssetsPath, "font-awesome.min.css"));


    }
	// Use this for initialization
	void Start () 
    {        
        if (txt != null)
        {
            string tmp = Fontdata[txt.text.Trim()].Code;
            if (!string.IsNullOrEmpty(tmp))
            {
                this.Text = txt.text;
                txt.text = tmp;             
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
#if UNITY_EDITOR
        if (txt != null && Fontdata!=null)
        {
            string tmp = Fontdata[txt.text.Trim()].Code;
            if (!string.IsNullOrEmpty(tmp))
            {
                this.Text = txt.text;
            }           
        }
#endif
	}
}
