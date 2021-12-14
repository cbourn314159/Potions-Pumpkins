using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinPuzzleSquare : MonoBehaviour
{
    public bool pumpkinOnSquare;
    public GameObject skullPuzzleSquare;

    // Start is called before the first frame update
    void Start()
    {
        pumpkinOnSquare = false;
        skullPuzzleSquare = GameObject.FindGameObjectWithTag("SkullPuzzleSquare");
    }

    // Update is called once per frame
    void Update()
    {
        if(skullPuzzleSquare.GetComponent<SkullPuzzleSquare>().skullOnSquare && pumpkinOnSquare)
        {
            print("Puzzle Solved!");
        }
        pumpkinOnSquare = false;
        skullPuzzleSquare.GetComponent<SkullPuzzleSquare>().skullOnSquare = false;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Pumpkin")
        {
            pumpkinOnSquare = true;
        }
    }
}
