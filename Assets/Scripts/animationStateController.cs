using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator YbotAnim;
    int velocityHash;
    float velocity = 0.0f;
    public float acc = 2.0f;
    public float dcc = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        YbotAnim = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("a") || Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        if (forwardPressed && velocity < .2f)
        {
            velocity += Time.deltaTime * acc;
        }
        if (!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * dcc;
        }
        if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        YbotAnim.SetFloat(velocityHash, velocity);
    }
}
