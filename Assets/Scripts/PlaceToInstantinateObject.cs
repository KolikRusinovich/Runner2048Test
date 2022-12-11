using System.Linq;
using UnityEngine;

public class PlaceToInstantinateObject
{
    private IOrderedEnumerable<Vector3> _verticesAtEndOfObject;
    private const float _SEPARATION_NUMBER = 0.1f;
    private Vector3[] _worldVertices;
    private int _centerOfEndObject;

    public Vector3 FindPlaceToInstantinateNextBlock(GameObject previousBlock)
    {
        _verticesAtEndOfObject = previousBlock.GetComponent<MeshFilter>().sharedMesh.vertices.Where(v => v.y > _SEPARATION_NUMBER)
            .OrderBy(v => v.x);
        int i = 0;
        _worldVertices = new Vector3[_verticesAtEndOfObject.Count()];
        foreach (Vector3 point in _verticesAtEndOfObject)
        {
            _worldVertices[i] = previousBlock.transform.TransformPoint(point);
            i++;
        }
        _centerOfEndObject = _worldVertices.Length / 2;
        return ((_worldVertices.ElementAt(_centerOfEndObject - 1) + _worldVertices.ElementAt(_centerOfEndObject)) / 2);
    }
}
