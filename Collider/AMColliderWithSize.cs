using UnityEngine;
using System.Collections;

public class AMColliderWithSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
