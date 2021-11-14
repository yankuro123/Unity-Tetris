using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] Blocks;
    // Start is called before the first frame update
    void Start()
    {
        NewBlocks();
    }


    public void NewBlocks()
    {
        Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
    }
}
