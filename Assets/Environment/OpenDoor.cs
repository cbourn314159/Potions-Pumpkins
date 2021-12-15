using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    bool touch = false;
    GameObject spiceUI;

    // Start is called before the first frame update
    void Start()
    {
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (touch && this.transform.rotation.y >= -180 && spiceUI.GetComponent<SpiceUI>().doorUnlocked)
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y - Time.deltaTime, this.transform.rotation.z, this.transform.rotation.w);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touch = true;
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("AHHH an enemy! Panic! Panic! ABORT ABORT!");
        }
    }
}
