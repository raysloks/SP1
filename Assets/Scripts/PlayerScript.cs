using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    new private Rigidbody2D rigidbody;

    private Vector2 velocity;
    private Vector2 size;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        size = new Vector2(1f, 2f);
    }

    private void FixedUpdate()
    {
        {
            float hor = 0f;
            if (Input.GetKey(KeyCode.A))
                hor -= 1f;
            if (Input.GetKey(KeyCode.D))
                hor += 1f;
            hor *= 4f;
            hor += velocity.x;
            hor *= Time.fixedDeltaTime;
            RaycastHit2D[] results = Physics2D.BoxCastAll(transform.position, size, 0f, new Vector2(hor, 0f), Mathf.Abs(hor));
            results = results.Where(c => Vector2.Dot(c.normal, new Vector2(hor, 0f)) < 0).ToArray();
            if (results.Length > 0)
            {
                transform.Translate(new Vector2(results[0].distance * Mathf.Sign(hor), 0f));
                velocity.x = 0f;
            }
            else
            {
                transform.Translate(new Vector2(hor, 0f));
            }
        }

        {
            float g = 10f;
            velocity.y -= g * Time.fixedDeltaTime;
            float ver = 0f;
            ver -= g * 0.5f * Time.fixedDeltaTime;
            ver += velocity.y;
            ver *= Time.fixedDeltaTime;
            RaycastHit2D[] results = Physics2D.BoxCastAll(transform.position, size, 0f, new Vector2(0f, ver), Mathf.Abs(ver));
            results = results.Where(c => Vector2.Dot(c.normal, new Vector2(0f, ver)) < 0).ToArray();
            if (results.Length > 0)
            {
                transform.Translate(new Vector2(0f, results[0].distance * Mathf.Sign(ver)));
                velocity.y = 0f;
                velocity.x *= Mathf.Exp(Mathf.Log(0.5f) * Time.fixedDeltaTime);
                if (Input.GetKey(KeyCode.Space))
                    velocity.y = 10f;
            }
            else
            {
                transform.Translate(new Vector2(0f, ver));
            }
        }
    }

}
