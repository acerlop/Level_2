using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerUpsText;

    public void SetTimeTxt(int levelTime)
    {
        int minutes = (int)levelTime / 60;
        int seconds = (int)levelTime % 60;

        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void SetLivesTxt(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    public void SetPowerUpTxt(int count)
    {
        powerUpsText.text = "Power Ups: " + count.ToString();  
    }
}
