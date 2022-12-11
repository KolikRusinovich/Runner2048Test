using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float rotProgress = 0;
    [SerializeField]
    private Spline spline;
    SplineExtrusion extr;

    private float progress = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        progress += 2f * Time.deltaTime;
        if (progress >= spline.Length)
        {
            progress = 0f;
        }
        CurveSample sample = spline.GetSampleAtDistance(progress);
        Debug.Log(sample.Rotation);
        transform.position = sample.location;
        transform.rotation = sample.Rotation;
        float radius = 1f;
        var sPos = Vector3.zero;
        var angle1 = Mathf.Lerp(0f, 2 * Mathf.PI, rotProgress);
        sPos.x += radius * Mathf.Sin(angle1);
        sPos.y += radius * Mathf.Cos(angle1);
        sPos.z += 0;
        sPos =  sample.Rotation * sPos; 



        transform.position += sPos;
    }




}
