using UnityEngine;
using System.Collections;

public class MyTransform : MonoBehaviour {

    public DataTransform dataTransform;
    public float timeReload = 1;
    public float timeCountReload = 0;
    void Start () {

        //Neu file khong ton tai thi tao va ghi file
        dataTransform = new DataTransform();
        dataTransform.name = gameObject.name;
        Transform trand = gameObject.transform;
        dataTransform.setData(trand.position, trand.rotation, trand.localScale);
        //dataTransform.save();
    }

    // Update is called once per frame
    void Update () {

        timeCountReload += Time.deltaTime;
        if(timeCountReload > timeReload)
        {
            timeCountReload = 0;

            dataTransform = dataTransform.load();
            if (dataTransform.autoUpdate)
            {
                //Debug.Log("co vao ko");
                gameObject.transform.position = dataTransform.getPosition();
                gameObject.transform.rotation = dataTransform.getRotation();
                gameObject.transform.localScale = dataTransform.getPScale();
            }
        }
	}

    
}
