using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
namespace Alta.Plugin
{
    [Serializable]
    public class ColorXml
    {
        [XmlAttribute("R")]
        public byte r;
        [XmlAttribute("G")]
        public byte g;
        [XmlAttribute("B")]
        public byte b;
        [XmlAttribute("A")]
        public byte a;
        [XmlAttribute("S")]
        public float Sensibilidad=0.3f;
        [XmlAttribute("Recote")]
        public float Recorte = 0.2f;
        public Color32 toColor32()
        {
            return new Color32(this.r,this.g,this.b,this.a);
        }
        public ColorXml()
        {

        }
        public ColorXml(Color32 c)
        {
            this.r = c.r;
            this.g = c.g;
            this.b = c.b;
            this.a = c.a;
        }

    }

    public static class ColorExtension 
    {

    }
}