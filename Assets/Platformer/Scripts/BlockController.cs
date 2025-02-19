using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject block;
    public void DestroyBlock() {
        Destroy(block);
    }
}
