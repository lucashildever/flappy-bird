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
        bool canJump = jumpCoolDown <= 0;

        // jump
        if(canJump) {
            bool isJumping = Input.GetKey(KeyCode.Space);
            if(isJumping) {
                Jump();
            }
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
