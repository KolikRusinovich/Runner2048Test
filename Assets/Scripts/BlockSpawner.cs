using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private int _totalBlocs = 10;
    [SerializeField]
    private GameObject _startingBlock;
    [SerializeField]
    private List<GameObject> _nextBlocks;

    private GameObject _prevBlock;

    public List<GameObject> _currentBlocks;

    private Vector3 _placeToinst = Vector3.zero;
    private PlaceToInstantinateObject _place;

    private void Start()
    {
        _currentBlocks = new List<GameObject>();
        _place = new PlaceToInstantinateObject();
        SpawnBlock(_startingBlock.GetComponent<Block>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBlock(SelectRandomGameObjectFromBlocks(_nextBlocks).GetComponent<Block>());
        }
    }

    private void SpawnBlock(Block block)
    {
        _prevBlock = Instantiate(block.gameObject, _placeToinst, block.gameObject.transform.rotation);
        _currentBlocks.Add(_prevBlock);
        _placeToinst = _place.FindPlaceToInstantinateNextBlock(_prevBlock);
    }

    private GameObject SelectRandomGameObjectFromBlocks(List<GameObject> list)
    {
        return list.Count == 0 ? null : list[Random.Range(0, list.Count)];
    }

}
