using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public SpriteRenderer Txt;
    public SpriteRenderer Txt1;
    // Start is called before the first frame update
    void Start()
    {
        Txt =  gameObject.GetComponent<SpriteRenderer>();
        Txt1 = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Txt.GetComponent<SpriteRenderer>().enabled = false;
            Txt1.GetComponent<SpriteRenderer>().enabled = false;
            BlockLogic.lost = false;
        }
    }
}
