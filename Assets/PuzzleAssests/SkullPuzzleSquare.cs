using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPuzzleSquare : MonoBehaviour
{
    public bool skullOnSquare;

    // Start is called before the first frame update
    void Start()
    {
        skullOnSquare = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Skull")
        {
            skullOnSquare = true;
        }
    }
}
