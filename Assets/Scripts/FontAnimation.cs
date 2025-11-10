using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontAnimation : MonoBehaviour
{
    public GameObject play;
    public GameObject quit;
    
    public void StartAnimateIn()
    {
        LeanTween.scale(play, play.transform.localScale + new Vector3(0.005f, 0.005f, 0.005f), 0.1f);
    }

    public void StartAnimateOut()
    {
        LeanTween.scale(play, play.transform.localScale - new Vector3(0.005f, 0.005f, 0.005f), 0.1f);
    }

    public void QuitAnimateIn()
    {
        LeanTween.scale(quit, quit.transform.localScale + new Vector3(0.005f, 0.005f, 0.005f), 0.1f);
    }

    public void QuitAnimateOut()
    {
        LeanTween.scale(quit, quit.transform.localScale - new Vector3(0.005f, 0.005f, 0.005f), 0.1f);
    }
}
