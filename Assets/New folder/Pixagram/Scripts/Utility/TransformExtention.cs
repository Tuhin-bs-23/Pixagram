using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
    public static void RemoveChildTransforms(this Transform transform)
    {
        int childcount = transform.childCount;

        for (int i = 0; i < childcount; i++)
        {
            var childobject = transform.GetChild(0).gameObject;
            Object.DestroyImmediate(childobject);
        }

    }

    public static void DestoryAllChild(this Transform transform)
    {
        int childcound = transform.childCount;
        for (int i = 0; i < childcound; i++)
        {
            Object.Destroy(transform.GetChild(0).gameObject);
        }
    }

    public static void DestoryAllChildImmediate(this Transform transform)
    {
        int childcound = transform.childCount;
        for (int i = 0; i < childcound; i++)
        {
            Object.DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
