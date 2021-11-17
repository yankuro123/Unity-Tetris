using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockLogic : MonoBehaviour
{
    //public float FallTime = 0.8f;
    public static int height = 20 ;
    public static int width = 12;
    public Vector3 RotationPoint;
    public Vector3 startingPoint;
    public SpriteRenderer Endgame;
    public SpriteRenderer Flashing;
    

    private float PreviousTime;
    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(RotationPoint, startingPoint, Color.green );

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove()) {
                transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if(Time.time - PreviousTime > (Input.GetKey(KeyCode.DownArrow) ? (0.8f/15.0f) : 0.8f))
        {
            transform.position += new Vector3(0, -1, 0);
            PreviousTime = Time.time;
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnPoint>().NewBlocks();
            }
                
            PreviousTime = Time.time;
        }
    }
    
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }
    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null; 
        }
    }
    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for ( int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);

                }

            }
        }
    }
    void CheckForLines()
    {
        for(int i = height -1 ; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            Debug.Log("X Rounded");
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            Debug.Log("Y Rounded");
            /*float X = children.transform.position.x;
            float Y = children.transform.position.y;*/
            if (roundedX < 0 || roundedX >= width || roundedY < 0 ||  roundedY >= height)
            {
                Debug.Log("Checked return false");
                return false;
            }
            if(grid[roundedX, roundedY] != null){
                return false;
            }
        }
        return true;
    }
    void EndGame()
    {
        foreach (Transform children in transform)
        {
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedY >= height)
            {
                Time.timeScale = 0;
                Endgame.GetComponent<SpriteRenderer>().enabled = true;
                Flashing.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
