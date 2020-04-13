using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleBtnListner : MonoBehaviour
{
    Schedule schedule;
    GameObject scheWindow;

    void Start()
    {
        schedule = Schedule.Instance;
        this.GetComponent<Button>().onClick.AddListener(()=> { AddScheduleBtnListner(); });
        scheWindow = GameObject.Find("Schedule"); //transform.Find("Schedule").gameObject; 
    }

    //스켸줄 등록
    public void AddScheduleBtnListner()
    {
        GameObject schInfoBG = this.transform.Find("scheInfoBG").gameObject;
        Text schIDTxt = schInfoBG.gameObject.transform.Find("schID").GetComponent<Text>();
        int schID = int.Parse(schIDTxt.text);
        if (scheWindow != null)
            scheWindow.SendMessage("AddSchedule", schID);
        else
            Debug.Log("Can't not find Schedule obejct.");
        //scheWindow.GetComponent<sched>.AddSchedule(schID);
    }
}
