using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevelLeftButtonBehavior : MonoBehaviour
{
    LockMiniGame Lock;
    // Start is called before the first frame update
    void Start()
    {
        Lock = GameObject.FindGameObjectWithTag("LockGame").GetComponent<LockMiniGame>();
    }
    public void OnSkillDownButtonPressed()
    {
        Lock.DecreaseSkillLevel();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
