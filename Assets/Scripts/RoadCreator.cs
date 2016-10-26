using System;
using System.Collections;

using UnityEngine;

[ExecuteInEditMode]
public class RoadCreator : MonoBehaviour
{
    /// <summary>
    /// Chunks of the road
    /// </summary>
    public RoadChunk[] roadChunks;

    public int matrixSize;
    public Vector2 initialPosition;

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
        scale = roadChunks[0].transform.localScale.x;
        mountTransform = gameObject.transform;
        mountTransform.position = new Vector3(0, 0.1f, 0);
        chunks = new RoadChunk[matrixSize, matrixSize];
        Vector2 currPosition = initialPosition;
        Vector2 dir = new Vector2(1f, 0f);
        for (int i=0; i<roadSize; i++)
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
        }

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
    private int getRandomChunkIndex()
    {
        return UnityEngine.Random.Range(0, roadChunks.Length);
    }
}
