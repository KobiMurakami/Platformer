using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public TextMeshProUGUI test;
void Update()
    {
        // Detect right mouse button click (button 1)
        if (Input.GetMouseButtonDown(1)) // 1 is the right mouse button
        {
            // Convert the mouse position to world space (in 3D)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            // Perform a raycast to detect objects under the mouse position
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has a BlockController script attached
                GameObject block = hit.collider.gameObject;
                if (block != null)
                {
                    Console.WriteLine("Successful Hit");
                    // If it's a block, destroy it
                    block.GetComponent<BlockController>().DestroyBlock();
                }
            }
            test.text = "Test Success";
        }
        Console.WriteLine("Test");
        Console.WriteLine(Input.GetMouseButtonDown(1));
    }
}