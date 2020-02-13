using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : MonoBehaviour
{
    public GameObject statImg;
    private static Schedule instance = null;    //singletone
    public List<ScheInfo> scheInfo_School;
    public List<ScheInfo> scheInfo_AfterSchool;
    public List<ScheInfo> scheInfo_Rest;

    public static Schedule Instance
    {
        get {
            if (instance == null)
                instance = new Schedule();
            return instance;
        }
    }

    private Schedule() { }

    public void SetVisibleStatImg()
    {
        if (statImg != null)
        {
            if (statImg.active == true)
                statImg.SetActive(false);
            else
                statImg.SetActive(true);
        }
    }
}