using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    private Rigidbody _rigidbody;
    private float _speed;

	// Use this for initialization
	void Start () {
        _speed = 0;
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            _speed -= 3000 * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            _speed += 1000 * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0f, 1.5f, 0f);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0f, -1.5f, 0f);
     
        transform.position = new Vector3(transform.position.x, 1.75f, transform.position.z);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
	}

    void FixedUpdate() {
        _speed *= 0.98f;
        _rigidbody.velocity = transform.forward * _speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "wall") {
            _speed *= .5f;
        }
    }
}
