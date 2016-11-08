using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public int playerNumber;
	public CanvasController canvasController;

    protected GameController _gameController;
    private Player _player;
    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
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
		/*if (Input.GetKeyDown (KeyCode.X)) {
			Debug.Log ("new Vector3("+transform.position.x.ToString("F1")+"f,"+transform.position.y.ToString("F1")+"f, "+transform.position.z.ToString("F1")+"f),");
		}*/
	}

    protected bool checkPaused() {
        if (_gameController.isPaused()) {
            _rigidbody.velocity = Vector3.zero;
        }
        return _gameController.isPaused();
    }

	protected void rotateLeft(){
        float angle = -1.5f * 80 * Time.deltaTime;
        transform.Rotate(0f, angle, 0f);
        _rigidbody.velocity = Quaternion.Euler(0, angle * .8f, 0) * _rigidbody.velocity;
	}

	protected void rotateRight(){
        float angle = 1.5f * 80 * Time.deltaTime;
        transform.Rotate(0f, angle, 0f);
        _rigidbody.velocity = Quaternion.Euler(0, angle * .8f, 0) * _rigidbody.velocity;
	}

	protected void speedUp(){
        _rigidbody.AddForce(transform.forward * Time.deltaTime * -3000, ForceMode.Acceleration);
	}

	protected void speedDown(){
        _rigidbody.AddForce(transform.forward * Time.deltaTime * 1000, ForceMode.Acceleration);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "checkpoint") {
            if (_player != null) {
                _player.onCheckpoint (int.Parse(other.transform.name.Substring (6)));
            }
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
