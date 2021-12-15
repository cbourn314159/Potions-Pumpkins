using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public bool win;
    public GameObject winDoor;
    public GameObject spiceUI;

    // Start is called before the first frame update
    void Start()
    {
        win = false;
        winDoor = GameObject.FindGameObjectWithTag("WinDoor");
        spiceUI = GameObject.FindGameObjectWithTag("SpiceUI");
    }

    // Update is called once per frame
    void Update()
    {
        if(win && winDoor.transform.rotation.y >= -180)
        {
            winDoor.transform.rotation = new Quaternion(winDoor.transform.rotation.x, winDoor.transform.rotation.y - Time.deltaTime, winDoor.transform.rotation.z, winDoor.transform.rotation.w);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<InventoryManager>().frappe)
        {
            win = true;
            spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Wow. Surprised that you won. Congrats, I guess... The End. Go home. Seriously, you can leave now.");
        }
    }
}
