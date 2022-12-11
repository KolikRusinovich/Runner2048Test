using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _initialPlayerSpeed = 4f;
    [SerializeField]
    private float _maximumPlayerSpeed = 30f;
    [SerializeField]
    private float _playerSpeedIncreaseRate = .1f;

    public float deviation;

    private float _playerSpeed;
    private Vector3 _movementDirection = Vector3.forward;
    private RaycastHit hit;

    private int actualWaypoint = 0;
    private int nextWaypoint => actualWaypoint + 1;

    [SerializeField]
    private BlockSpawner block;
    [HideInInspector] public Vector3 posOnPath;

    private Vector3 normal;
    Vector3 moveDirection;
    bool grounded;
    Rigidbody rb;

    private void Start()
    {
        _playerSpeed = _initialPlayerSpeed;
        transform.position = Vector3.forward;
        rb = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(posOnPath, 0.5f);
    }

    public Vector3 Project(Vector3 vector)
    {
        return Vector3.ProjectOnPlane(vector, normal);
    }

    /*private void OnSlope()
    {
        RaycastHit slopehit;
        if (Physics.Raycast(transform.position, -transform.up, out slopehit))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle;
        }
    }*/

    private void FixedUpdate()
    {
        moveDirection = transform.forward;
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out hit))
        {
            /*Vector3 desired_up = Vector3.Lerp(Vector3.up, hit.normal, Time.deltaTime * 0.1f);
            Quaternion tilt = Quaternion.FromToRotation(transform.up, desired_up);
            transform.rotation *= tilt;*/
            /*bool isValidPlane = Vector3.Dot(hit.normal, Vector3.up) > 0.1f;
            Quaternion currentRotation = transform.rotation;
            Quaternion normalRoation = Quaternion.FromToRotation(transform.transform.up, hit.normal);
            Quaternion targetRotation = isValidPlane ? normalRoation * currentRotation : currentRotation;
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 0.3f);*/
            //transform.forward = hit.normal;
            /*transform.position = Vector3.MoveTowards(transform.position, hit.normal, _playerSpeed * Time.deltaTime);
            Vector3 targetDir = hit.normal - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, _playerSpeed * Time.deltaTime * 4, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);*/
            //Vector3 segment = Vector3.ProjectOnPlane(moveDirection, hit.normal).normalized;
            Vector3 segment = block._currentBlocks[nextWaypoint].transform.position - block._currentBlocks[actualWaypoint].transform.position;
            if (Vector3.Project(transform.position - block._currentBlocks[actualWaypoint].transform.position, segment).magnitude < segment.magnitude)
            {
            }
            else
            {
                actualWaypoint++;
            }
            //transform.forward = segment;
            transform.forward = segment;
            //moveDirection += transform.forward;
            //transform.position = Vector3.Slerp(transform.position,transform.position + moveDirection.normalized * _playerSpeed, Time.deltaTime);
            rb.MovePosition(rb.position + moveDirection.normalized * _playerSpeed * Time.deltaTime);
            //
            //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);


            //rb.rotation = segment;

            //rb.rotation =  Quaternion.FromToRotation(Vector3.up, hit.normal);

            //rb.MoveRotation(transform.rotation);
            //rb.rotation = Quaternion.AngleAxis(Vector3.Angle(Vector3.up, hit.normal), hit.normal); 
            //
            //deviation = hit.distance;
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            //normal = hit.normal;
            //Vector3 segment = Project(Vector3.forward.normalized);

            //Debug.Log(transform.forward);

            //transform.forward = Vector3.Lerp(Vector3.up, segment, Time.deltaTime * 1);
            //transform.position = Vector3.Slerp(transform.position, transform.forward, Time.deltaTime * 1);
            //if too far away force it towards the down direction
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 30, Color.green);
        }
        
        
        
        /*if (nextWaypoint < block._currentBlocks.Count && block._currentBlocks.Count > 1)
        {
            Vector3 segment = block._currentBlocks[nextWaypoint].transform.position - block._currentBlocks[actualWaypoint].transform.position; //The segment between actual point and next point
            //The character's position is projected on segment, if this vector is longer than segment, it means that next point has been passed
            if (Vector3.Project(posOnPath - block._currentBlocks[actualWaypoint].transform.position, segment).magnitude < segment.magnitude)
            {
                float deviationMultiplicator = (1f - Math.Abs(deviation) / 4f); //The character is faster when he is near the center of the toboggan
                posOnPath += _playerSpeed * 1 * deviationMultiplicator * Time.deltaTime * segment.normalized;
            }
            else
            {
                actualWaypoint++;
            }

            //ORIENTATING CHARACTER
            transform.forward = Vector3.Lerp(transform.forward, segment, Time.deltaTime * 1);

            //PLACING CHARACTER
            //The character is now placed on the toboggan with a raycast, and the deviation from the initial path is added here
            //Layer mask 9 is for "Toboggan"

            if (Physics.Raycast(posOnPath + Vector3.up + transform.right * deviation * block._currentBlocks[actualWaypoint].transform.GetComponent<MeshFilter>().sharedMesh.bounds.size.x / 2f, Vector3.down, out hit, 10))
            {
                transform.position = Vector3.Slerp(transform.position, hit.point + hit.normal * transform.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y, Time.deltaTime * 1);
            }
        }*/
    }
}
