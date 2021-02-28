using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
public void OnClick()
    {
        SceneManager.LoadScene("LockingPickingScene");
    }
}
