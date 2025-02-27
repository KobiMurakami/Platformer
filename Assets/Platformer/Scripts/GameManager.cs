using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI CoinUI;

    public TextMeshProUGUI TimeUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 100 - (int)Time.time;
        if(timeLeft >= 0) {
            TimeUI.text = $"Time: {timeLeft}";
        }
        if(timeLeft <= 0) {
            UnityEngine.Debug.Log("Fail: You ran out of time");
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject block = hit.collider.gameObject;
                if (block != null) {
                    UnityEngine.Debug.Log("Successful Hit");
                    UnityEngine.Debug.Log(block.name);
                    try {
                        block.GetComponent<BlockController>().DestroyBlock();
                    }
                    catch(Exception e) {
                        CoinGet();
                    }
                }
            }
        }
    }

    public void CoinGet() {
        int number;
        int.TryParse(CoinUI.text, out number);
        number++;
        CoinUI.text = number.ToString();
    }
}
