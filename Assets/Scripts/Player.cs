using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player {

    public static Player one() {
        return new Player(1, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
    }

    public static Player two() {
        return new Player(2, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
    }

    private readonly KeyCode moveUp;
    private readonly KeyCode moveDown;
    private readonly KeyCode moveLeft;
    private readonly KeyCode moveRight;
    private int _playerNumber;
	private int lapsToWin;
	private int lastCheckpoint;
	private CanvasController canvasController;
    private GameController _gameController;

    public Player(int playerNumber, KeyCode moveUp, KeyCode moveDown, KeyCode moveLeft, KeyCode moveRight) {
        this._playerNumber = playerNumber;
        this.moveUp = moveUp;
        this.moveDown = moveDown;
        this.moveRight = moveRight;
        this.moveLeft = moveLeft;
		lapsToWin = 3;
		lastCheckpoint = 0;
        _gameController = GameController.getInstance();
    }

    public KeyCode getUpKey() {
        return moveUp;
    }

    public KeyCode getDownKey() {
        return moveDown;
    }

    public KeyCode getRightKey() {
        return moveRight;
    }

    public KeyCode getLeftKey() {
        return moveLeft;
    }

	public void onCheckpoint(int index){
        var totalLaps = 3;

		// Forward
        if ((lastCheckpoint + 1) % totalLaps == index) {
			if (index == 0) {
				lapsToWin--;
				canvasController.updateLaps (lapsToWin);
                if (lapsToWin == 0) {
                    gameWon();
                }
			}
			lastCheckpoint = index;
		}

		// Backwards
        if ((lastCheckpoint - 1 + totalLaps) % totalLaps == index) {
			if (lastCheckpoint == 0) {
				lapsToWin++;
			}
			lastCheckpoint = index;
		}
	}

	public void setCanvasController(CanvasController controller){
		this.canvasController = controller;
	}

    private void gameWon() {
        _gameController.finishGame(_playerNumber, canvasController.getBestLap());
    }

}
    