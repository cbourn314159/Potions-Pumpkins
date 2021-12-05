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
    public bool damaged;
    public float damageTimer;
    int testTextChain;

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
        nextText = false;
        floatUp = true;

        spiceTextSpeed = .1f;
        spiceFloatBoundariesY = .01f;
        spiceFloatSpeedY = .02f;
        testTextChain = 0;

        damaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(isTyping)
        {
            TypeOutSpiceText(typeOutText);
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
        
        if (nextText)
        {
            switch (testTextChain)
            {
                case 0: ChangeSpiceText("This is an example of Spice the Pumpkin speaking."); testTextChain++; break;
                case 1: ChangeSpiceText("Here's her saying something else."); testTextChain++; break;
                case 2: ChangeSpiceText("Last part of text chain."); testTextChain++; break;
                case 3: ClearSpiceText(); break;
            }
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
