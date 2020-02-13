using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCHE_TYPE { Rest, School, AfterSchool, Forced }

public class ScheInfo : MonoBehaviour
{
    enum MODE_INDEX { All, Normal, Charming, Bad, Sick }
    enum STEP_TYPE { None, Basic, High }
    enum EXPO_TYPE { Always, Unlock, Period, Disposable, Substitute }
    enum ADJUST { None, Minus, Plus }

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
        scheType = (SCHE_TYPE)int.Parse(keys[2]);
        yearNum = int.Parse(keys[3]);
        monthNum = int.Parse(keys[3]);
        dayNum = int.Parse(keys[4]);
        modeIndex = (MODE_INDEX)int.Parse(keys[5]);
        stepType = (STEP_TYPE)int.Parse(keys[6]);
        expoType = (EXPO_TYPE)int.Parse(keys[7]);
        adjust = (ADJUST)int.Parse(keys[8]);
        money = int.Parse(keys[9]);
        prefabPath = keys[10];
        localIndex = keys[11];
    }

    //Procedure
    public SCHE_TYPE ScheType
    {
        get {
            return this.scheType;
        }
    }
}
