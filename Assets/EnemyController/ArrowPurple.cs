using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPurple : MonoBehaviour
{
    public GameObject player;
    public GameObject spiceUI;
    public Vector3 playerPosition;
    public float arrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");

        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.LookAt(playerPosition);
        transform.Rotate(new Vector3(-90,0,0));

        arrowSpeed = 25;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * arrowSpeed;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            spiceUI.GetComponent<SpiceUI>().damaged = true;
        }
        Destroy(this.gameObject);
    }
}
