using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Spline spline;
    SplineExtrusion extr;

    [Range(0f, 2f)]
    public float progress = 0;

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
        transform.localPosition = sample.location;
        transform.localRotation = sample.Rotation;
    }




}
