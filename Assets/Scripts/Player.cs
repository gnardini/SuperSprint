using System;
using UnityEngine;

public class Player {

    public static readonly Player ONE = new Player(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
    public static readonly Player TWO = new Player(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

    private readonly KeyCode moveUp;
    private readonly KeyCode moveDown;
    private readonly KeyCode moveLeft;
    private readonly KeyCode moveRight;

    public Player(KeyCode moveUp, KeyCode moveDown, KeyCode moveLeft, KeyCode moveRight) {
        this.moveUp = moveUp;
        this.moveDown = moveDown;
        this.moveRight = moveRight;
        this.moveLeft = moveLeft;
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
}
    