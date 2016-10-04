using System;
using UnityEngine;

public class Player {

    public static readonly Player ONE = new Player(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
    public static readonly Player TWO = new Player(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

    private readonly KeyCode moveUp;
    private readonly KeyCode moveDown;
    private readonly KeyCode moveLeft;
    private readonly KeyCode moveRight;
	private int lapsToWin;
	private int lastCheckpoint;
	private CanvasController canvasController;

    public Player(KeyCode moveUp, KeyCode moveDown, KeyCode moveLeft, KeyCode moveRight) {
        this.moveUp = moveUp;
        this.moveDown = moveDown;
        this.moveRight = moveRight;
        this.moveLeft = moveLeft;
		lapsToWin = 2;
		lastCheckpoint = 0;
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

		// Forward
		if ((lastCheckpoint + 1) % 3 == index) {
			if (index == 0) {
				lapsToWin--;
				canvasController.updateLaps (lapsToWin);
			}
			lastCheckpoint = index;
		}

		// Backwards
		if ((lastCheckpoint - 1 + 3) % 3 == index) {
			if (lastCheckpoint == 0) {
				lapsToWin++;
			}
			lastCheckpoint = index;
		}
	}

	public void setCanvasController(CanvasController controller){
		this.canvasController = controller;
	}
}
    