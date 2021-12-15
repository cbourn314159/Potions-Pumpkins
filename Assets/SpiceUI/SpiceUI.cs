using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SpiceUI : MonoBehaviour
{
    public GameObject spiceCharacter;
    public GameObject spiceText;
    GameObject witchHat1;
    GameObject witchHat2;
    GameObject witchHat3;
    GameObject witchHat4;
    GameObject witchHat5;
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
    public int health;
    public float damageTimer;
    public AudioSource sound;
    public AudioClip clip;
    public GameObject spice;
    public GameObject player;
    public float regenTimer;
    public bool doorUnlocked;
    public bool firstDamage;

    // Start is called before the first frame update
    void Start()
    {
        spiceCharacter = this.gameObject.transform.GetChild(0).gameObject;
        spiceText = this.gameObject.transform.GetChild(1).gameObject;

        spiceTextMeshPro = spiceText.GetComponent<TMP_Text>();

        characterPositionCenter = Camera.main.WorldToViewportPoint(spiceCharacter.transform.position);
        characterRotationCenter = spiceCharacter.transform.rotation;

        witchHat1 = GameObject.FindGameObjectWithTag("WitchHat1");
        witchHat2 = GameObject.FindGameObjectWithTag("WitchHat2");
        witchHat3 = GameObject.FindGameObjectWithTag("WitchHat3");
        witchHat4 = GameObject.FindGameObjectWithTag("WitchHat4");
        witchHat5 = GameObject.FindGameObjectWithTag("WitchHat5");

        time = 0;
        isTyping = false;
        nextText = true;
        floatUp = true;
        damaged = false;
        health = 5;
        doorUnlocked = false;
        firstDamage = false; ;

        spiceTextSpeed = .025f;
        spiceFloatBoundariesY = .01f;
        spiceFloatSpeedY = .02f;
        testTextChain = 0;

        spice = GameObject.FindGameObjectWithTag("SpiceUI");
        player = GameObject.FindGameObjectWithTag("Player");
        sound = spice.GetComponent<AudioSource>();
        clip = (AudioClip)Resources.Load("dying_player");
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
                case 3: ChangeSpiceText("Hit [Space Bar] to throw your tiny body upwards or [Shift] to get the zoomies. If you want be rad as hell, you can walk next to a wall to climb."); break;
                case 4: ChangeSpiceText("Not to mention, [Left-Click] to fire an arcane blast and smoke some enemy Decafs (those're the bad guys)."); break;
                case 5: ChangeSpiceText("Wanna toggle your inventory to gaze upon your collection and pop out some potions? Hit [E]. Gotta go pickup ingredients like that coffee bag in front of you."); break;
                case 6: ChangeSpiceText("Now, once ya got enough ingredients to brew a potion; I'll cheat and do it for ya :P. Potions will give you plenty of hella cool powers."); break;
                case 7: ChangeSpiceText("I bet yer thinkin' \"Then, what do I do?\" Well, you gotta gather up all the components in this here dungeon to make the legendary frappe!"); break;
                case 8: ChangeSpiceText("Go ahead and walk through that door to get this PARTY STARTED! I mean exam... a serious exam."); doorUnlocked = true; break;
                case 9: ClearSpiceText(); break;
            }
            if(testTextChain <= 8)
            {
                testTextChain++;
            }
        }
        //spiceUI.GetComponent<SpiceUI>().ChangeSpiceText("Wow. Surprised that you won. Congrats, I guess... The End. Go home. Seriously, you can leave now.");
        //case 6: ChangeSpiceText("So, you see that friendly boi over there down the hallway. Go say hi. He's very friendly."); break;
        
        if (Input.GetKeyDown(KeyCode.C))
        {            
            nextText = true;
        }
        else
        {
            nextText = false;
        }

        DamageScreen();

        HealthScreen();

        SpiceFloatingMovement();
    }

    public void HealthScreen()
    {
        switch (health)
        {
            case 5:
                witchHat5.SetActive(true);
                witchHat4.SetActive(true);
                witchHat3.SetActive(true);
                witchHat2.SetActive(true);
                witchHat1.SetActive(true);
                break;
            case 4:
                witchHat5.SetActive(false);
                witchHat4.SetActive(true);
                witchHat3.SetActive(true);
                witchHat2.SetActive(true);
                witchHat1.SetActive(true);
                break;
            case 3:
                witchHat5.SetActive(false);
                witchHat4.SetActive(false);
                witchHat3.SetActive(true);
                witchHat2.SetActive(true);
                witchHat1.SetActive(true);
                break;
            case 2:
                witchHat5.SetActive(false);
                witchHat4.SetActive(false);
                witchHat3.SetActive(false);
                witchHat2.SetActive(true);
                witchHat1.SetActive(true);
                break;
            case 1:
                witchHat5.SetActive(false);
                witchHat4.SetActive(false);
                witchHat3.SetActive(false);
                witchHat2.SetActive(false);
                witchHat1.SetActive(true);
                break;
            default:
                witchHat5.SetActive(false);
                witchHat4.SetActive(false);
                witchHat3.SetActive(false);
                witchHat2.SetActive(false);
                witchHat1.SetActive(false);
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
                //SceneManager.LoadScene("Main Menu");
                break;
        }
    }

    public void DamageScreen()
    {
        if (damaged == true)
        {
            if(!firstDamage)
            {
                ChangeSpiceText("WATCHOUT! Your witch's hat only has 5 protection charges before you D.I.E. bum bum bummmmm.");
                firstDamage = true;
            }
            health -= 1;
            if (clip != null && health == 0)
            {
                sound.PlayOneShot(clip, 0.5f);
            }
            damageTimer = 0;
            damaged = false;
            GetComponent<Image>().enabled = true;
        }
        damageTimer += Time.deltaTime;
        if (damageTimer >= .5)
        {
            GetComponent<Image>().enabled = false;
        }

        if(player.GetComponent<InventoryManager>().iced)
        {
            if(health < 5)
            {
                regenTimer += Time.deltaTime;
                if(regenTimer >= 15)
                {
                    health += 1;
                    regenTimer = 0;
                }
            }
            else
            {
                regenTimer = 0;
            }
        }
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
