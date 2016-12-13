using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Alta.Plugin;

[RequireComponent(typeof(Text)), ExecuteInEditMode]
public class Symbol : MonoBehaviour {
    private Text txt;
    public string symbol;
    public static FontIcons Fontdata;
    void Awake()
    {
        txt = GetComponent<Text>();
#if UNITY_EDITOR
        Fontdata = ExCss.ReadFile(System.IO.Path.Combine(Application.streamingAssetsPath, "font-awesome.min.css"));
#endif

    }
    // Use this for initialization
    void Start () 
    {        
        if (txt != null && !string.IsNullOrEmpty(this.symbol))
        {
            string tmp = Fontdata[this.symbol.Trim()].Code;
            if (!string.IsNullOrEmpty(tmp))
            {
                txt.text = tmp;             
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && txt != null && Fontdata!= null && !string.IsNullOrEmpty(this.symbol))
        {
            string tmp = Fontdata[this.symbol.Trim()].Code;
            if (!string.IsNullOrEmpty(tmp))
            {
                txt.text = tmp;
            }
        }
#endif
	}
}
