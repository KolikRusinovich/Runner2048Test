using UnityEngine;

public enum BlockType
{
    FORWARD,
    LEFT,
    RIGHT,
    UP,
    DOWN
}

public class Block : MonoBehaviour
{
    [SerializeField]
    BlockType blockType;
}
