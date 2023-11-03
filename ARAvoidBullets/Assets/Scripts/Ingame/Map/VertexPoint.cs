using ARAvoid;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class VertexPoint : MonoBehaviour
{
    private Transform root;
    private Transform Root => root ??= transform.parent;

    public void UpdateHeight()
    {
        var layPoint = transform.position;
        layPoint.y = (Root ? root.position.y : transform.position.y) + Defines.MaxMapHeight;

        if(Physics.Raycast(layPoint, Vector3.down, out var hitInfo, Defines.MaxMapHeight * 2f, Defines.FieldLayer))
        {
			layPoint.y = hitInfo.point.y;
			transform.position = layPoint;
		}   
    }
}