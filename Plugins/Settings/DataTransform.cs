using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class DataRotate
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }
}

public class DataPosition
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}

public class DataScale
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}


public class DataTransform
{
    public string name;
    public string description;
    public string fileName;
    public bool autoUpdate;
    public DataRotate rotate;
    public DataPosition position;
    public DataScale scale;

    public DataTransform()
    {
        name = "";
        description = "";
        fileName = "";
        autoUpdate = true;
        rotate = new DataRotate();
        position = new DataPosition();
        scale = new DataScale();
}

    public void setPosition(Vector3 position)
    {
        this.position.x = position.x;
        this.position.y = position.y;
        this.position.z = position.z;
    }

    public void setRotation(Quaternion rotate)
    {
        this.rotate.x = rotate.x;
        this.rotate.y = rotate.y;
        this.rotate.z = rotate.z;
        this.rotate.w = rotate.w;
    }

    public void setScale(Vector3 scale)
    {
        this.scale.x = scale.x;
        this.scale.y = scale.y;
        this.scale.z = scale.z;
    }

    public Vector3 getPosition()
    {
        return new Vector3(this.position.x, this.position.y, this.position.z);
    }

    public Quaternion getRotation()
    {
        return new Quaternion(this.rotate.x, this.rotate.y, this.rotate.z, this.rotate.w);
    }

    public Vector3 getPScale()
    {
        return new Vector3(this.scale.x, this.scale.y, this.scale.z);
    }

    public void setData(Vector3 position, Quaternion rotate, Vector3 scale)
    {
        setPosition(position);
        setRotation(rotate);
        setScale(scale);
    }

    public void save()
    {
        using (StreamWriter file = File.CreateText(string.Format(Application.streamingAssetsPath + @"/{0}.json",name)))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, this);
        }
    }

    public DataTransform load()
    {
        //Check neu ton tai thi doc, khong ton tai thi tao moi va doc
        string fileName = string.Format(Application.streamingAssetsPath + @"/{0}.json", name);
        if (!File.Exists(fileName))
        {
            this.save();
        }

        using (StreamReader file = File.OpenText(fileName))
        {
            JsonSerializer serializer = new JsonSerializer();
            return JsonConvert.DeserializeObject<DataTransform>(file.ReadToEnd().ToString());
        }
    }
}

