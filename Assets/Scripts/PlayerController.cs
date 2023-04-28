using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidbody;

    public float jumpPower = 3;
    public float jumpInterval = 0.5f;
    public float jumpCoolDown = 0;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // update jumpcooldown
        jumpCoolDown -= Time.deltaTime;
        bool isGameActive = GameManager.Instance.IsGameActive();
        bool canJump = jumpCoolDown <= 0 && isGameActive;

        // jump
        if(canJump) {
            bool isJumping = Input.GetKey(KeyCode.Space);
            if(isJumping) {
                Jump();
            }
        }

        thisRigidbody.useGravity = isGameActive;
    }

    void OnCollisionEnter(Collision other) {
        OnCustomCollisionEnter(other.gameObject);
    }
    void OnTriggerEnter(Collider other) {
        OnCustomCollisionEnter(other.gameObject);
    }

    private void OnCustomCollisionEnter(GameObject other) {

        bool isSensor = other.gameObject.CompareTag("Sensor");

        if(isSensor) {
            // pontuação aumenta
            GameManager.Instance.score++;
            Debug.Log("Score: " + GameManager.Instance.score);
        } else {
            // game over
            GameManager.Instance.EndGame();
        }
    }

    private void Jump() {
        // reset cool down
        jumpCoolDown = jumpInterval;
        // reset gravity/velocity
        thisRigidbody.velocity = Vector3.zero; /*Vector3.zero === new Vector3(0, 0, 0)*/
        // apply jump
        thisRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    }
}
