using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SCHE_TYPE { Rest, School, AfterSchool, Forced }

//각 일정(국어 수업, 전단지 알바 등) 관리
public class ScheInfo
{
    public enum MODE_INDEX { All, Normal, Charming, Bad, Sick }
    public enum STEP_TYPE { None, Basic, High }
    public enum EXPO_TYPE { Always, Unlock, Period, Disposable, Substitute }
    public enum ADJUST { None, Minus, Plus }

    int scheGroupID;
    string remarks;
    SCHE_TYPE scheType;
    int yearNum;
    int monthNum;
    int dayNum;
    MODE_INDEX modeIndex;
    STEP_TYPE stepType;
    EXPO_TYPE expoType;
    ADJUST adjust;
    int money;
    string prefabPath;
    string localIndex;
    
    private ScheInfo() { }
    public ScheInfo(string[] keys)
    {
        scheGroupID = int.Parse(keys[0]);
        remarks = keys[1];
        scheType = (SCHE_TYPE)int.Parse(keys[2]);   //
        yearNum = int.Parse(keys[3]);
        monthNum = int.Parse(keys[4]);
        dayNum = int.Parse(keys[5]);
        modeIndex = (MODE_INDEX)int.Parse(keys[6]);
        stepType = (STEP_TYPE)int.Parse(keys[7]);
        expoType = (EXPO_TYPE)int.Parse(keys[8]);
        adjust = (ADJUST)int.Parse(keys[9]);
        money = int.Parse(keys[10]);
        prefabPath = keys[11];
        localIndex = keys[12];
    }

    public string GetScheduleInfo()
    {
        string str = "";

        str = remarks;
        if (stepType == STEP_TYPE.Basic)
            str += "/초급";
        else if (stepType == STEP_TYPE.High)
            str += "/고급";

        str += "\n";

        //str += "스탯정보"
        if (adjust == ADJUST.Plus)
            str += "+";
        else if (adjust == ADJUST.Minus)
            str += "-";
        str += money;

        return str;
    }

    //Procedure
    public SCHE_TYPE ScheType
    {
        get { return this.scheType; }
    }

    public int ScheGroupID
    {
        get { return scheGroupID; }
    }

    public int Money { get { return money; } }
    public ADJUST Adjust { get { return adjust; } }
    public string Remark { get { return remarks; } }
}

public class ScheduleCGList
{
    int index;
    int age;
    Image CG;

    public ScheduleCGList(string[] keys)
    {
        string imgPath = "Assets\\";

        index = int.Parse(keys[0]);
        age = int.Parse(keys[1]);

        switch (age)
        {
            case 10:

                break;
            default:
                Debug.Log("Wrong Age");
                break;
        }
    }
}