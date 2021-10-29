using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLogic : MonoBehaviour
{
    //public float FallTime = 0.8f;
    public static int height = 20;
    public static int width = 12;

    private float PreviousTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }

        if(Time.time - PreviousTime > (Input.GetKey(KeyCode.DownArrow) ? (0.8f/15.0f) : 0.8f))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
                transform.position -= new Vector3(0, -1, 0);
            PreviousTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach(Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            Debug.Log("X Rounded");
            int roundedY = Mathf.RoundToInt(child.transform.position.y);
            Debug.Log("Y Rounded");

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                Debug.Log("Checked return true");
            return true;          
        }
        Debug.Log("Checked return false");
        return false;
    }
}
