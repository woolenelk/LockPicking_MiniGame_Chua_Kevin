using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

enum Difficulty
{
    Easy, 
    Medium, 
    Hard,
    Hell
}

public class LockMiniGame : MonoBehaviour
{
    [Header("Lock")]
    [SerializeField]
    Difficulty difficulty;
    [SerializeField]
    int SkillLevel;
    [SerializeField]
    float actualAngle;
    [SerializeField]
    float target;
    [SerializeField]
    int BufferRange;
    [SerializeField]
    int BufferSkillRange;
    [SerializeField]
    bool moving;
    [SerializeField]
    bool Attempting;
    [SerializeField]
    bool Finished;
    [SerializeField]
    bool Won;
    [SerializeField]
    float timer;

    [SerializeField]
    Quaternion originalLock;
    [SerializeField]
    Quaternion originalDoor;
    [SerializeField]
    Quaternion originalLockPick;


    [Header("Components")]
    [SerializeField]
    Transform LockPiece;
    [SerializeField]
    Transform LockPick;
    [SerializeField]
    Transform LockDoor;
    [SerializeField]
    AudioSource ClickSound;
    [SerializeField]
    TextMeshProUGUI DifficultyText;
    [SerializeField]
    TextMeshProUGUI SkillText;
    [SerializeField]
    TextMeshProUGUI TimerText;
    [SerializeField]
    float damping = 5;
    // Start is called before the first frame update
    void Start()
    {
        originalLock = LockPiece.rotation;
        originalLockPick = LockPick.rotation;
        originalDoor = LockDoor.rotation;
        actualAngle = LockPick.eulerAngles.z;
        ResetLock();
        //target = Random.Range(-90, 90);
        InvokeRepeating("SkillCheck", 0.0f, 0.5f);
        SkillText.text = SkillLevel.ToString();
        switch (difficulty)
        {
            case Difficulty.Easy:
                DifficultyText.text = "Easy";
                break;
            case Difficulty.Medium:
                DifficultyText.text = "Medium"; 
                break;
            case Difficulty.Hard:
                DifficultyText.text = "Hard"; 
                break;
            case Difficulty.Hell:
                DifficultyText.text = "Hell";
                break;
            default:
                DifficultyText.text = "Easy";
                break;
        }
    }

    void SkillCheck()
    {
        if (moving)
        {
            if (actualAngle <= target + BufferSkillRange && actualAngle >= target - BufferSkillRange)
            {
                ClickSound.volume = 0.02f + (SkillLevel * 0.005f);
                ClickSound.Play();
            }
        }
    }

    public void ResetLock()
    {
        target = Random.Range(-90, 90);
        LockDoor.rotation = originalDoor;
        LockPiece.rotation = originalLock;
        LockPick.rotation = originalLockPick;
        Finished = false;
        Won = false;
        timer = 30.0f;
        BufferRange = 5;
        switch (difficulty)
        {
            case Difficulty.Easy:
                BufferRange += 15;
                break;
            case Difficulty.Medium:
                BufferRange += 10;
                break;
            case Difficulty.Hard:
                BufferRange += 5;
                break;
            case Difficulty.Hell:
                break;
            default:
                break;
        }
        BufferSkillRange = BufferRange + (10 - SkillLevel);
    }
    // Update is called once per frame
    void Update()
    {
        actualAngle = LockPick.eulerAngles.z;
        if (actualAngle >= 180 && actualAngle <= 360)
            actualAngle -= 360;
        TimerText.text = ((int)timer).ToString();
        if (Won && !Finished)
        {
            damping = 1;
            LockDoor.rotation = Quaternion.Slerp(LockDoor.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * damping);
        }
        else if (!Won && !Finished)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, 1000);
        }
        if (timer <= 0)
        {
            Finished = true;
        }
        moving = false;
        Attempting = false;
        if (Won || Finished)
            return;
        if (Input.GetKey("space"))
        {
            Attempting = true;
            damping = 5;
            Quaternion rotation = Quaternion.AngleAxis(-5, Vector3.forward);
            rotation *= LockPiece.rotation;
            LockPiece.rotation = Quaternion.Slerp(LockPiece.rotation, rotation, Time.deltaTime * damping);
            Debug.Log(LockPiece.eulerAngles.z);
            if (!(actualAngle <= target + BufferRange && actualAngle >= target - BufferRange) && (LockPiece.eulerAngles.z-90) > 45)
            {
                LockPiece.rotation = Quaternion.Slerp(LockPiece.rotation, originalLock, Time.deltaTime * damping);
            }
            if ((LockPiece.eulerAngles.z - 90) > 80)
            {
                Won = true;
                //Finished = true;
            }
            //print("space key was pressed");
        }
        if (!Attempting && !Won)
        {
            damping = 8;
            LockPiece.rotation = Quaternion.Slerp(LockPiece.rotation, originalLock, Time.deltaTime * damping);

            if (Input.GetKey("a") && LockPick.rotation.z <= 0.78)
            {
                moving = true;
                damping = 5;
                Quaternion rotation = Quaternion.AngleAxis(5, Vector3.forward);
                rotation *= LockPick.rotation;
                LockPick.rotation = Quaternion.Slerp(LockPick.rotation, rotation, Time.deltaTime * damping);
            }
            if (Input.GetKey("d") && LockPick.rotation.z >= -0.78)
            {
                moving = true;
                damping = 5;
                Quaternion rotation = Quaternion.AngleAxis(-5, Vector3.forward);
                rotation *= LockPick.rotation;
                LockPick.rotation = Quaternion.Slerp(LockPick.rotation, rotation, Time.deltaTime * damping);
            }
        }
    }

    public void IncreaseSkillLevel()
    {
        SkillLevel = Mathf.Clamp(SkillLevel + 1, 0, 10);
        SkillText.text = SkillLevel.ToString();
    }
    public void DecreaseSkillLevel()
    {
        SkillLevel = Mathf.Clamp(SkillLevel - 1, 0, 10);
        SkillText.text = SkillLevel.ToString();
    }

    public void IncreaseDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                difficulty = Difficulty.Medium;
                DifficultyText.text = "Medium";
                break;
            case Difficulty.Medium:
                difficulty = Difficulty.Hard;
                DifficultyText.text = "Hard";
                break;
            case Difficulty.Hard:
                difficulty = Difficulty.Hell;
                DifficultyText.text = "Hell";
                break;
            case Difficulty.Hell:
                difficulty = Difficulty.Hell;
                DifficultyText.text = "Hell";
                break;
            default:
                DifficultyText.text = "Easy";
                break;
        }
    }

    public void DecreaseDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                difficulty = Difficulty.Easy;
                DifficultyText.text = "Easy";
                break;
            case Difficulty.Medium:
                difficulty = Difficulty.Easy;
                DifficultyText.text = "Easy";
                break;
            case Difficulty.Hard:
                difficulty = Difficulty.Medium;
                DifficultyText.text = "Medium";
                break;
            case Difficulty.Hell:
                difficulty = Difficulty.Hard;
                DifficultyText.text = "Hard";
                break;
            default:
                DifficultyText.text = "Easy";
                break;
        }
    }

}
