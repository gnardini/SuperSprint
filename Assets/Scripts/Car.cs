using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public int playerNumber;
	public CanvasController canvasController;

    private Player _player;
    private Rigidbody _rigidbody;
    private float _speed;

	// Use this for initialization
	void Start () {
        _speed = 0;
        _rigidbody = GetComponent<Rigidbody>();
        initPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(_player.getUpKey()))
            _speed -= 3000 * Time.deltaTime;
        if (Input.GetKey(_player.getDownKey()))
            _speed += 1000 * Time.deltaTime;
        if (Input.GetKey(_player.getRightKey()))
            transform.Rotate(0f, 1.5f, 0f);
        if (Input.GetKey(_player.getLeftKey()))
            transform.Rotate(0f, -1.5f, 0f);
     
        transform.position = new Vector3(transform.position.x, 1.75f, transform.position.z);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
	}

    void FixedUpdate() {
        _speed *= 0.98f;
        _rigidbody.velocity = transform.forward * _speed * Time.deltaTime;
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "checkpoint") {
			_player.onCheckpoint (int.Parse(other.transform.name.Substring (6)));
		}
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "wall") {
            _speed *= .5f;
        }
    }

    private void initPlayer() {
        switch(playerNumber) {
		case 1:
			_player = Player.ONE;
            break;
        case 2:
            _player = Player.TWO;
            break;
        }
		_player.setCanvasController (canvasController);
    }
}
