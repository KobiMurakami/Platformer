using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;

    [Header("Debug Stuff")]
    public bool isGrounded;
    Rigidbody rb;
    Animator animator;

    public TextMeshProUGUI CoinUI;
    public TextMeshProUGUI ScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.velocity += Vector3.right * horizontalAmount * Time.deltaTime * acceleration;

        float horizontalSpeed = rb.velocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.velocity;
        newVelocity.x = horizontalSpeed;
        rb.velocity = newVelocity;


        float verticalSpeed = rb.velocity.y;
        verticalSpeed = Mathf.Clamp(verticalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVerticalVelocity = rb.velocity;
        newVerticalVelocity.y = verticalSpeed;
        rb.velocity = newVerticalVelocity;

        Collider collider = GetComponent<Collider>();
        float castDistance = collider.bounds.extents.y + 0.01f;
        Vector3 startPoint = transform.position;
        Debug.DrawLine(startPoint, startPoint + castDistance * Vector3.down, Color.red);

        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);
    

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        }
        else if(Input.GetKey(KeyCode.Space) && !isGrounded) {
            if(rb.velocity.y > 0) {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
        }

        if(horizontalAmount == 0f) {
            Vector3 decayedVelocity = rb.velocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.velocity = decayedVelocity;
        }
        else {
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Grounded", isGrounded);


        // Checks for block collisions
        float castDistanceBlock = collider.bounds.extents.y +1;
        Vector3 startPointBlock = transform.position;

            Ray ray = new Ray(startPointBlock, Vector3.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, castDistanceBlock)) {
                UnityEngine.Debug.Log("Hit Detected");
                GameObject block = hit.collider.gameObject;
                if (block != null) {
                    UnityEngine.Debug.Log("Successful Hit");
                    UnityEngine.Debug.Log(block.name);
                    try {
                        UnityEngine.Debug.Log("Deleting Brick");
                        block.GetComponent<BlockController>().DestroyBlock();
                        ScoreIncrease();
                    }
                    catch(Exception e) {
                        UnityEngine.Debug.Log("Deleting QBrick");
                        GameObject qBlock = hit.collider.gameObject;
                        qBlock.GetComponent<QuestionController>().DestroyQBlock();
                        CoinGet();
                    }
                }
            }
    }

    public void CoinGet() {
        int number;
        int.TryParse(CoinUI.text, out number);
        number++;
        CoinUI.text = number.ToString();
        ScoreIncrease();
    }

    public void ScoreIncrease() {
        int number;
        int.TryParse(ScoreUI.text, out number);
        number += 100;
        ScoreUI.text = number.ToString();
    }
}
