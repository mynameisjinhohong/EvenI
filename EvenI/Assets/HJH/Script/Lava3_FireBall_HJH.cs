using UnityEngine;

public class Lava3_FireBall_HJH : Object_Manager_shj
{
    Rigidbody2D rigidbody;
    public float fireBallSpeed;
    public float angle;
    public float gravity;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Vector2 moveVec = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        rigidbody.velocity = moveVec * fireBallSpeed;


    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector2.down * gravity);
        float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
    }
}
