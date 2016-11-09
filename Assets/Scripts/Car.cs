using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public int playerNumber;
	public CanvasController canvasController;

    protected GameController _gameController;
    private Player _player;
    private Rigidbody _rigidbody;
    private float _speed;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        _speed = 0;
        _rigidbody = GetComponent<Rigidbody>();
        _gameController = GameController.getInstance();
        initPlayer();
	}
	
	// Update is called once per frame
	protected void Update () {
        if (checkPaused()) {
            return;
        }
		if (Input.GetKey (_player.getUpKey ()))
			speedUp ();
		if (Input.GetKey (_player.getDownKey ()))
			speedDown ();
		if (Input.GetKey (_player.getRightKey ()))
			rotateRight ();
		if (Input.GetKey (_player.getLeftKey ()))
			rotateLeft ();

			

		fixedPosition ();
	}

    protected bool checkPaused() {
        if (_gameController.isPaused()) {
            _rigidbody.velocity = Vector3.zero;
        }
        return _gameController.isPaused();
    }

	protected void rotateLeft(){
        transform.Rotate(0f, -1.5f * 80 * Time.deltaTime, 0f);
	}

	protected void rotateRight(){
        transform.Rotate(0f, 1.5f * 80 * Time.deltaTime, 0f);
	}

	protected void speedUp(){
		_speed -= 3000 * Time.deltaTime;
	}

	protected void speedDown(){
		_speed += 1000 * Time.deltaTime;
	}

	protected void fixedPosition() {
		transform.position = new Vector3(transform.position.x, 1.75f, transform.position.z);
		transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
	}

    void FixedUpdate() {
		if (_gameController.isPaused()) {
			return;
		}
		_speed *= 0.98f;
		_rigidbody.velocity = transform.forward * _speed * Time.deltaTime;
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "checkpoint") {
            if (_player != null) {
                _player.onCheckpoint (int.Parse(other.transform.name.Substring (6)));
            }
		}
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "wall") {
			Vector3 dir = (-transform.forward);
			Vector3 norm = (collision.transform.position - transform.position);
			dir /= dir.magnitude;
			norm /= norm.magnitude;
			float vectorProd = dir.x * norm.z - norm.x * dir.z;
			Debug.Log (vectorProd);
			_speed *= Mathf.Abs(vectorProd);
        }
    }

	protected virtual Player initPlayer2(){
		return Player.two();
	}

    private void initPlayer() {
        switch(playerNumber) {
		case 1:
            _player = Player.one();
            break;
		case 2:
			_player = initPlayer2 ();
            break;
        }
		_player.setCanvasController (canvasController);
    }
}
