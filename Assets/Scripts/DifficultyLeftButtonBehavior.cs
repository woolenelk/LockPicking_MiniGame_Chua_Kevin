using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLeftButtonBehavior : MonoBehaviour
{
    LockMiniGame Lock;
    // Start is called before the first frame update
    void Start()
    {
        Lock = GameObject.FindGameObjectWithTag("LockGame").GetComponent<LockMiniGame>();
    }
    public void OnDifficultyDownButtonPressed()
    {
        Lock.DecreaseDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
