using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//스켸줄 생성, 삭제, 실행 관리
public class Schedule
{
    private static Schedule instance;    //singletone

    public List<ScheInfo> scheInfo_School;
    public List<ScheInfo> scheInfo_AfterSchool;
    public List<ScheInfo> scheInfo_Rest;

    //리스트에 표시되는 리스트 아이템 prefab
    public Transform scheItemBtnPrefab;
    //몇 개까지 스켸줄에 넣었는지 표시
    List<ScheInfo> dautherScheduleList; //근데 ScheInfo객체 리스트인지 잘 모르겠다; 앞으로 이걸 어떻게 써먹을지 모르겠음...

    void Start() { 

    }

    public static Schedule Instance
    {
        get {
            if (instance == null)
            {
                instance = new Schedule();
            }
            return instance;
        }
    }

    private Schedule()
    {
        scheInfo_School = new List<ScheInfo>();
        scheInfo_AfterSchool = new List<ScheInfo>();
        scheInfo_Rest = new List<ScheInfo>();

        dautherScheduleList = new List<ScheInfo>();
    }

    //public int DautherScheListCount { get { return instance.dautherScheduleList.Count; } }
    //public int GetScheduleDebut(int idx) { return instance.dautherScheduleList[idx].Money; }

    public int GetTotalScheduleDebut()
    {
        int total = 0;
        for (int i = 0; i < instance.dautherScheduleList.Count; ++i)
        {
            if(instance.dautherScheduleList[i].Adjust == ScheInfo.ADJUST.Minus)
                total += instance.dautherScheduleList[i].Money;
        }
        return total;
    }

    //지금 실행되는 딸의 스켸줄 관리
    public void AddDautherSchedule(ScheInfo scheInfo)
    {
        instance.dautherScheduleList.Add(scheInfo);
    }
    public void DeleteDautherSchedule()
    {
        int daughterScheListCount = instance.dautherScheduleList.Count;
        instance.dautherScheduleList.RemoveAt(daughterScheListCount - 1);
    }

    public string GetAllDaughterScheduleID()
    {
        string str = "";
        for (int i = 0; i < instance.dautherScheduleList.Count; ++i)
            str += instance.dautherScheduleList[i].ScheGroupID + " ";
        return str;
    }

    public ScheInfo GetDaughterScheduleByIndex(int idx)
    {
        if (idx >= 0 && idx < dautherScheduleList.Count)
        {
            return dautherScheduleList[idx];
        }
        else
        {
            Debug.Log("NullPointerException : wrong index( " + idx + ")");
            return null;
        }
    }

    public List<ScheInfo> DaughterSchedule
    {
        get { return dautherScheduleList; }
    }
}