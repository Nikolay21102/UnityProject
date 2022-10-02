using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;
    float angle;
    float currentVelocity;
    public float maxTurnSpeed = 90;
    public float smoothTime = 0.3f;
    private Animator anim;
    private HealthBar health;
    private GameObject deathscreen;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GetComponent<HealthBar>();
        deathscreen = GameObject.Find("Death Screen");
        deathscreen.SetActive(false);
    }
    private States State
    { get { return (States)anim.GetInteger("state");  }
      set { anim.SetInteger("state", (int)value); }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);
        if(HealthBar.currentValue > 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
                State = States.Run;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
                State = States.Run;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                State = States.Run;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                State = States.Run;
            }
            else
            {
                State = States.idle;
            }
        }
        if(HealthBar.currentValue <= 0)
        {
            State = States.die;
            deathscreen.SetActive(true);
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    enum States 
    {
        idle,
        Run,
        die
    }

}
