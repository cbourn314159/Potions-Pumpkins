using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinPuzzleSquare : MonoBehaviour
{
    public bool pumpkinOnSquare;
    public GameObject skullPuzzleSquare;
    public GameObject itemDrop;

    public bool itemDropped;
    public float time;
    public float puzzleTimer;
    public float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        pumpkinOnSquare = false;
        itemDropped = false;
        skullPuzzleSquare = GameObject.FindGameObjectWithTag("SkullPuzzleSquare");
        time = 0;
        delayTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (skullPuzzleSquare.GetComponent<SkullPuzzleSquare>().skullOnSquare && pumpkinOnSquare)
        {
            if (time - puzzleTimer >= delayTime && !itemDropped)
            {
                itemDrop.transform.position = new Vector3(46.7108f, 21.39403f, -12.07076f);
                Instantiate(itemDrop);
                itemDropped = true;
            }
        }
        else
        {
            puzzleTimer = time;
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
