using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLinks : MonoBehaviour
{
    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/AlexanderTerp");
    }

    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/AlexanderTerp/unity-tetris");
    }

    public void OpenYouTube()
    {
        Application.OpenURL("https://youtube.com/watch?v=c5991E51Spk");
    }
}
