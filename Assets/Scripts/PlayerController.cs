using SplineMesh;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float rotProgress = 0f;
    [SerializeField]
    private Spline spline;
    [SerializeField]
    private float _rotSpeed = 4.0f;
    SplineExtrusion extr;
    //SplineMesh mesh;
    SplineNode node;
    SplineMeshTiling tiling;

    Transform _child;
    private float progress = 0f;

    private float angle2 = 0;
    float rot;
    private float rate = 0;
    Vector3 prevPos = Vector3.zero;


    Quaternion toot;
    Quaternion objRotation;
    private void Start()
    {
        _child = transform.GetChild(0);
        tiling = spline.gameObject.GetComponent<SplineMeshTiling>();
        toot = transform.rotation;
    }

    private void FixedUpdate()
    {
        progress += 2f * Time.deltaTime;
        if (progress >= spline.Length)
        {
            progress = 0f;
        }
        /*rate += Time.deltaTime / DurationInSecond;
        if (rate > spline.nodes.Count - 1)
        {
            rate -= spline.nodes.Count - 1;
        }*/
        //CurveSample sample = spline.GetSampleAtDistance(progress);

        CurveSample sample = spline.GetProjectionSample(transform.position);

        var ss = sample.Rotation;

        var senterPosition = transform.position;
        var spos = sample.location;
        spos += (transform.right * (_rotSpeed)) + transform.up *1.5f;
        //spos.x += _rotSpeed;
        //spos.y += 1.5f;
        //transform.position += spos - prevPos;
        //transform.rotation = sample.Rotation;
        RaycastHit hit;
        var upVector = Vector3.Cross(sample.tangent, 
            Vector3.Cross(Quaternion.AngleAxis(sample.roll, Vector3.forward) * transform.position, sample.tangent).normalized);
        Debug.Log(Vector3.Cross(upVector,transform.position));
        //transform.up = transform.up - upVector;
        //transform.rotation = sample.Rotation;
        
        //Ray ray = new Ray(transform.position, -transform.up);
        Ray ray = new Ray(transform.position + transform.right * (_rotSpeed - rot), -transform.up);
        transform.position += transform.right * (_rotSpeed - rot);

        /*toot = Quaternion.FromToRotation(transform.position, sample.location);
        toot.x = sample.Rotation.x;
        toot.y = sample.Rotation.y;
        transform.rotation = toot;*/


        if (Physics.Raycast(ray, out hit) && Mathf.Abs(_rotSpeed - rot) > 0)
        {

            //transform.rotation = Quaternion.LookRotation(hit.normal);
            //Vector3 asd = Vector3.Cross(transform.right, hit.normal);

            /*transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2);*/
            //transform.position = new Vector3(spos.x, hit.point.y + transform.localScale.y / 2, spos.z);
            Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, hit.normal)) * hit.normal;
            toot = Quaternion.LookRotation(proj, hit.normal);
            //toot = Quaternion.FromToRotation(transform.forward, hit.normal);
            //toot = Quaternion.FromToRotation(transform.up, hit.normal);
            //toot.x = 0;
            //toot.y = 0;
            transform.rotation = toot;
            
            //transform.up = hit.normal;
            var asda = hit.point;
            
            //transform.up = hit.normal;
            //transform.rotation = Quaternion.Slerp(transform.rotation, toot, Time.deltaTime * 2);


            /*Vector3 rotationVector = hit.normal - transform.position;
            rotationVector.x = 0;
            rotationVector.y = 0;
            transform.rotation = Quaternion.LookRotation(rotationVector);*/
            //transform.up = hit.point - transform.position;
            /*transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.FromToRotation(transform.up, hit.normal),
                Time.deltaTime * 2);*/
            //transform.up = transform.position - hit.normal;
            //transform.LookAt(sample.location);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * transform.localScale.y / 2, Color.green);
            //Debug.Log(hit.normal);
            //toot.z = tooty.z;
            //Debug.Log(toot);
            //transform.rotation = toot;
            //
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 5f, Color.red);
        }
        objRotation = sample.Rotation;
        objRotation.z += toot.z;
        //transform.rotation = objRotation;
        //
        //transform.position = spos;
        //transform.position = sample.Rotation * transform.position;
        //transform.rotation.SetLookRotation(sample.Rotation * transform.forward);

        //transform.position += 
        rot = _rotSpeed;
        prevPos = transform.position;
        /*var lookpos = sample.location - transform.position;
        toot = Quaternion.LookRotation(lookpos, Vector3.forward);
        toot.x = 0;
        toot.y = 0;
        var asd = sample.Rotation;
        asd.z = toot.z;
        transform.rotation = asd;*/
        //transform.position = spos;
        //var rot = transform.rotation;
        //transform.rotation = sample.Rotation;
        //transform.rotation = Quaternion.Slerp(rot, sample.Rotation, _rotSpeed * Time.deltaTime);

        /*float radius = 1f;
        var sPos = Vector3.zero;
        var angle1 = Mathf.Lerp(0f, 2 * Mathf.PI, _rotSpeed);
        sPos.x += radius * Mathf.Sin(angle1);
        sPos.y += radius * Mathf.Cos(angle1);
        sPos =  sample.Rotation * sPos;
        transform.position += sPos;*/ 
        //Debug.Log(Vector3.Angle(senterPosition, transform.position));
        /*transform.position += sPos;
        angle2 += Vector3.Angle(transform.position, senterPosition);*/
        //rot.z = angle2;
        //transform.rotation = rot;

        //transform.Rotate(0,0,angle2);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
        //rot.x = 0;
        //rot.y = 0;
        //_child.transform.rotation = rot;
        //_child.LookAt(new Vector3(sample.location.x, sample.location.y, 0));
        //Debug.Log(_child.transform.up);
        //_child.transform.up = senterPosition - transform.position;
        //_child.transform.LookAt(sample.location);
        //transform.up = sample.up - transform.position;
        //_child.LookAt(sample.location);
    }




}
