using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLockButtonBehavior : MonoBehaviour
{
    LockMiniGame Lock;
    // Start is called before the first frame update
    void Start()
    {
        Lock = GameObject.FindGameObjectWithTag("LockGame").GetComponent<LockMiniGame>();
    }

    public void OnResetButtonPressed()
    {
        Lock.ResetLock();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
