using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToWorldButton : MonoBehaviour
{
public void OnButtonClick()
    {
        SceneManager.LoadScene("WorldScene");
    }
}
