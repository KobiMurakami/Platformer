using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    public GameObject qBlock;
    public void DestroyQBlock() {
        Destroy(qBlock);
    }
}
