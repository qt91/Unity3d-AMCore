using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Alta.Plugin
{
    public static class TranformExtention
    {
        public static void ClearChildren(this Transform tr)
        {
            foreach (Transform child in tr)
            {
                GameObject.Destroy(child.gameObject);
            }
            tr.DetachChildren();
        }
        public static void Reset(this Transform tr)
        {
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
            tr.localScale = Vector3.one;
        }

        public static Component CreateChild(this Transform parent, Component child)
        {
            Component go = GameObject.Instantiate(child);
            go.transform.SetParent(parent);
            go.transform.Reset();
            return go;
        }
        public static GameObject CreateChild(this Transform parent, GameObject child)
        {
            GameObject go = GameObject.Instantiate(child);
            go.transform.SetParent(parent);
            go.transform.Reset();
            return go;
        }

    }
}
