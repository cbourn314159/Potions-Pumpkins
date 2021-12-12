using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpiceUI : MonoBehaviour
{
    public GameObject spiceCharacter;
    public GameObject spiceText;
    public TMP_Text spiceTextMeshPro;
    public Vector3 characterPositionCenter;
    public Quaternion characterRotationCenter;
    public float time;
    public bool isTyping;
    public bool nextText;
    public string typeOutText;
    public int typeOutTextIndex;
    public float typeOutTextTimer;
    public bool floatUp;
    public float spiceTextSpeed;
    public float spiceFloatBoundariesY;
    public float spiceFloatSpeedY;
    int testTextChain;
    public bool damaged;
    public float damageTimer;
    public AudioSource sound;
    public GameObject spice;

    // Start is called before the first frame update
    void Start()
    {
        spiceCharacter = this.gameObject.transform.GetChild(0).gameObject;
        spiceText = this.gameObject.transform.GetChild(1).gameObject;

        spiceTextMeshPro = spiceText.GetComponent<TMP_Text>();

        characterPositionCenter = Camera.main.WorldToViewportPoint(spiceCharacter.transform.position);
        characterRotationCenter = spiceCharacter.transform.rotation;

        time = 0;
        isTyping = false;
        nextText = true;
        floatUp = true;
        damaged = false;

        spiceTextSpeed = .025f;
        spiceFloatBoundariesY = .01f;
        spiceFloatSpeedY = .02f;
        testTextChain = 0;

        spice = GameObject.FindGameObjectWithTag("SpiceUI");
        sound = spice.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(isTyping)
        {
            TypeOutSpiceText(typeOutText);
            
        }

        if (nextText)
        {
            switch (testTextChain)
            {
                case 0: ChangeSpiceText("Heyo bozo, name's Spice. You look like need help to pass your Univesity of Witchigan-Deadborn final exam. First off, press [C] or we can just sit here in awkward silence."); break;
                case 1: ChangeSpiceText("Glad you like my charismatic company ;). Use [W][A][S][D] to walk around. That seemed pretty obvious, but can't be too sure with someone of your caliber."); break;
                case 2: ChangeSpiceText("For ULTIMATE CHRONOMANCY POWER!!!!!, press [Esc] to enter the pause menu, duh."); break;
                case 3: ChangeSpiceText("Hit [Space Bar] to throw your tiny body upwards or [Shift] to get the zoomies. If you want be rad as hell, you can run next to a wall to climb."); break;
                case 4: ChangeSpiceText("Wanna toggle your inventory and pop out some potions? Hit [E]. (Uhhhhh maybe just have fun opening the inventory, potions are pending)"); break;
                case 5: ChangeSpiceText("So, you see that friendly boi over there down the hallway. Go say hi. He's very friendly."); break;
                case 6: ChangeSpiceText("Okay, okay, ya got me. [Left-Click] or hit [Ctrl] to fire an arcane blast and smoke his ass."); break;
                case 7: ClearSpiceText(); break;
            }
            testTextChain++;
        }

        if (Input.GetKeyDown(KeyCode.C) && isTyping == false)
        {            
            nextText = true;
        }
        else
        {
            nextText = false;
        }

        if (damaged == true)
        {
            damageTimer = 0;
            damaged = false;
            GetComponent<Image>().enabled = true;
        }
        damageTimer += Time.deltaTime;
        if (damageTimer >= .5)
        {
            GetComponent<Image>().enabled = false;
        }

        SpiceFloatingMovement();
    }

    public void ClearSpiceText()
    {
        if (!isTyping)
        {
            spiceTextMeshPro.text = "";
            
        }
    }

    public void ChangeSpiceText(string newSpiceText)
    {
        if (CheckSpiceTextLength(newSpiceText))
        {
            spiceTextMeshPro.text = "Spice Text Too Long!";
        }
        else
        {
            sound.Play();
            isTyping = true;
            spiceTextMeshPro.text = "";
            typeOutText = newSpiceText;
            typeOutTextIndex = 0;
            typeOutTextTimer = time;
        }
    }

    public void TypeOutSpiceText(string newSpiceText)
    {
        if (time - typeOutTextTimer >= spiceTextSpeed)
        {
            spiceTextMeshPro.text += newSpiceText.ToCharArray()[typeOutTextIndex];
            typeOutTextIndex++;
            typeOutTextTimer = time;
        }
        if(newSpiceText.Length <= typeOutTextIndex)
        {
            isTyping = false;
            sound.Stop();
        }
        
    }

    public bool CheckSpiceTextLength(string newSpiceText)
    {
        return newSpiceText.Length > 296;
    }

    public void TranslateSpice(float xTranslation, float yTranslation)
    {
        spiceCharacter.transform.Translate(new Vector3(xTranslation, yTranslation, 0));
    }

    public void RotateSpice(float xRotation, float yRotation)
    {
        spiceCharacter.transform.Rotate(new Vector3(xRotation, yRotation, 0));
    }

    public void SpiceFloatingMovement()
    {
        Vector3 relativeCharacterPosition = Camera.main.WorldToViewportPoint(spiceCharacter.transform.position);
        float yMovement;
        if(relativeCharacterPosition.y - characterPositionCenter.y > spiceFloatBoundariesY)
        {
            floatUp = false;
        }
        if(relativeCharacterPosition.y - characterPositionCenter.y < -spiceFloatBoundariesY)
        {
            floatUp = true;
        }

        if (floatUp)
        {
            yMovement = spiceFloatSpeedY * Time.deltaTime;
        }
        else
        {
            yMovement = -spiceFloatSpeedY * Time.deltaTime;
        }

        TranslateSpice(0, yMovement);
    }
}
