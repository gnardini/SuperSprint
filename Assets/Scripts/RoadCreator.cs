using System;
using System.Collections;

using UnityEngine;

[ExecuteInEditMode]
public class RoadCreator : MonoBehaviour
{
    /// <summary>
    /// Chunks of the road
    /// </summary>
    public RoadChunk[] roadChunksStartBig;
	public RoadChunk[] roadChunksStartSmall;

	public GameObject planePrefab;

    public int matrixSize;
    public Vector2 initialPosition;
	private GameObject track;

    /// <summary>
    /// Size of the road in number of chunks
    /// </summary>
    public int roadSize = 10;
    static private RoadChunk[,] chunks;
    private Transform mountTransform;
    private float scale = 0f;

    /// <summary>
    /// Generate the level
    /// </summary>
    public void Generate()
    {
		
        scale = roadChunksStartBig[0].transform.localScale.x;
        chunks = new RoadChunk[matrixSize, matrixSize];
        Vector2 currPosition = initialPosition;
        Vector2 dir = new Vector2(1f, 0f);
		chunks[(int)currPosition.x-1, (int)currPosition.y] = roadChunksStartBig[0];
		track = new GameObject ();
		track.name = "Track";
		for (int i = 0; i < 3; i++) {
			GameObject plane = UnityEngine.Object.Instantiate (planePrefab) as GameObject;
			plane.name = String.Format ("Plane {0}", i);
			plane.transform.parent = track.transform;
			plane.transform.position = new Vector3 (-20.0f + ((i+1)%3)*20, 0.0f, 0.0f);
		}
		RoadChunk newGO = UnityEngine.Object.Instantiate (roadChunksStartBig [1]) as RoadChunk;
		setupNewRoadChunk (newGO, currPosition, dir);
		dfs (currPosition+dir, dir, roadSize, true);
/*        for (int i=0; i<roadSize; i++)
        {
            int roadChunkIdx = getRandomChunkIndex();

            RoadChunk newGO = UnityEngine.Object.Instantiate(roadChunks[roadChunkIdx]) as RoadChunk;
            newGO.name = String.Format("part-{0}", i);
            newGO.transform.parent = transform;

            setChunk(currPosition, newGO);
            newGO.transform.Rotate(new Vector3(0, getRotation(dir), 0));
            dir = getNewDirection(dir, newGO.turn);
            Debug.Log(dir);
            currPosition += dir;
        }*/
    }

	private void setMatrix<T>(T[,] matrix, Vector2 position, T value){
		matrix [(int)position.x, (int)position.y] = value;
	}

	private T getMatrix<T>(T[,] matrix, Vector2 position){
		return matrix [(int)position.x, (int)position.y];
	}


	private bool completeTrack(Vector2 position, Vector2 dir){
		Vector2 finalPosition = initialPosition-new Vector2(1.0f, 0.0f);
		setMatrix (chunks, finalPosition, null);
		Vector2[,] previusPositions = new Vector2[matrixSize, matrixSize];
		Vector2[] direction = { 
			new Vector2 (0.0f, 1.0f),
			new Vector2 (0.0f, -1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (-1.0f, 0.0f)
		};

		setMatrix (previusPositions, position, position - dir);
		Queue queue = new Queue ();
		queue.Enqueue (position);
		Vector2 currPosition = position;
		while (currPosition != finalPosition && queue.Count!=0) {
			currPosition = (Vector2)queue.Dequeue();
			for (int i = 0; i < direction.Length; i++) {
				Vector2 nextPosition = currPosition + direction [i];
				if (0 > (int)nextPosition.x || (int)nextPosition.x >= matrixSize)
					continue;
				if (0 > (int)nextPosition.y || (int)nextPosition.y >= matrixSize)
					continue;
				if(getMatrix(previusPositions, nextPosition)==Vector2.zero  && getMatrix(chunks, nextPosition)==null){
					setMatrix (previusPositions, nextPosition, currPosition);
					queue.Enqueue (nextPosition);
				}
			}
		}
		if (currPosition != finalPosition) {
			return false;
		}

		Vector2 lastPosition = position - dir;
		dir = new Vector2 (1.0f, 0.0f); //The first chunk is in this direction.
		Boolean fat = true; //The first chunk is starting fat.
		Boolean fatNeeded = getMatrix(chunks, lastPosition).endFat;
		while (currPosition != lastPosition) {
			Vector2 prevPosition = getMatrix (previusPositions, currPosition);
			Vector2 prevDir = currPosition - prevPosition;
			Direction turn = getTurnFromDirections (prevDir, dir);
			int index = getIndexFromDirection (turn);
			if (turn == Direction.STRAIGHT && fat != fatNeeded) {
				fat = !fat;
				index = 3;
			}
			RoadChunk[] roadChunks = fat ? roadChunksStartBig : roadChunksStartSmall;
			RoadChunk newGO = UnityEngine.Object.Instantiate (roadChunks [index]) as RoadChunk;
			fat = newGO.startFat;
			dir = prevDir;
			setupNewRoadChunk (newGO, currPosition, dir);
			currPosition = prevPosition;
		}
		return fat==fatNeeded;
	}

	private int getIndexFromDirection(Direction dir){
		switch (dir) {
		case Direction.LEFT:
			return 0;
		case Direction.STRAIGHT:
			return 1;
		case Direction.RIGHT:
			return 2;
		}
		return 3;
	}


	//Retorna la nueva dirección.
	private Vector2 setupNewRoadChunk(RoadChunk newGO, Vector2 position, Vector2 dir){
		newGO.name = String.Format ("part-{0}-{1}", (int)position.x, (int)position.y);
		newGO.transform.parent = track.transform;
		setChunk (position, newGO);
		newGO.transform.Rotate (new Vector3 (0, getRotation (dir), 0));
		return getNewDirection (dir, newGO.turn);
	}


	private bool dfs(Vector2 position, Vector2 dir, int length, bool fat){
		if (length == 0) {
			if (getMatrix (chunks, position) != null)
				return false;
			return completeTrack(position, dir);
		}
		if (chunks [(int)position.x,(int)position.y] == null) {
			int[] roadChunkIdx = getRandomChunkIndex (fat);
			RoadChunk[] roadChunks = fat ? roadChunksStartBig : roadChunksStartSmall;
			for (int i = 0; i < roadChunkIdx.Length; i++) {
				RoadChunk newGO = UnityEngine.Object.Instantiate (roadChunks [roadChunkIdx[i]]) as RoadChunk;
				Vector2 newDir = setupNewRoadChunk (newGO, position, dir);
				//Debug.Log (dir);
				if (dfs (position + newDir, newDir, length - 1, newGO.endFat)) {
					return true;
				}
				DestroyImmediate (newGO.gameObject);
				setChunk (position, null);
			}
		}
		return false;
	}

	//If directions are opposite this returns RIGHT (maybe we should change this)
	private Direction getTurnFromDirections(Vector2 prev, Vector2 curr){
		if (prev == curr)
			return Direction.STRAIGHT;
		if (getNewDirection (prev, Direction.LEFT) == curr) {
			return Direction.LEFT;
		}
		return Direction.RIGHT;
	}

    private Vector2 getNewDirection(Vector2 dir, Direction turn){
        switch(turn){
        case Direction.LEFT:
            if (dir.x == 0) return new Vector2(-dir.y, 0);
            return new Vector2(0, dir.x);
        case Direction.RIGHT:
            if (dir.x == 0) return new Vector2(dir.y, 0);
            return new Vector2(0, -dir.x);
        case Direction.STRAIGHT:
            return dir;
        }
        throw new Exception();
    }

    private float getRotation(Vector2 dir){
        return -Mathf.Atan2(dir.y, dir.x) * 180f / Mathf.PI;
    }

    private void setChunk(Vector2 p, RoadChunk chunk){
        setChunk((int)p.x, (int)p.y, chunk);
    }

    private void setChunk(int i, int j, RoadChunk chunk){
        chunks[i, j] = chunk;
		if (chunk == null)
			return;
        chunk.transform.localPosition = new Vector3((i - initialPosition.x)*scale, 0, (j-initialPosition.y)*scale);
    }

    public void RemoveAll()
    {
        RoadChunk[] rc = gameObject.GetComponentsInChildren<RoadChunk>();

        for (int i=0; i<rc.Length; i++)
        {
            UnityEngine.Object.DestroyImmediate(rc[i].gameObject);
        }
    }

    private int prevIdx = 0;
	private int[] getRandomChunkIndex(bool fat)
	{
		int length = fat ? roadChunksStartBig.Length : roadChunksStartSmall.Length;
		int[] ans = new int[length];
		for (int i = 0; i < length; i++) {
			ans [i] = i;
		}
		for (int i = 0; i < 100; i++) {
			int a = UnityEngine.Random.Range (0, length);	
			int b = UnityEngine.Random.Range (0, length);	
			int c = ans [a];
			ans [a] = ans [b];
			ans [b] = c;
		}
		return ans;
    }
}
