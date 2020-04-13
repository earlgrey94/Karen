using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Lobby : MonoBehaviour
{
    Schedule schedule;
    
    // Start is called before the first frame update
    void Start()
    {
        schedule = Schedule.Instance;
        if (schedule.scheInfo_School.Count == 0)
            FileOpen("Assets\\Schedule_List.csv");
    }

    void FileOpen(string file_name)
    {
        FileStream fs = new FileStream(file_name, FileMode.Open);
        StreamReader sr = new StreamReader(fs, Encoding.UTF8, false);

        string lineStr = null;
        string[] keys = null;
        int cnt = 0;

        while ((lineStr = sr.ReadLine()) != null)
        {
            //첫 2줄은 읽지 않고 skip
            if (cnt < 2)
            {
                ++cnt;
                continue;
            }

            //빈 줄이 들어오면 return
            if (string.IsNullOrEmpty(lineStr))
                return;

            keys = lineStr.Split(',');
            ScheInfo tmpScheInfo = new ScheInfo(keys);

            //Debug.Log("ID : " + tmpScheInfo.ScheGroupID);

            //Schedule Type에 따라 다른 list에 넣어줌
            if (tmpScheInfo.ScheType == SCHE_TYPE.School)
                schedule.scheInfo_School.Add(tmpScheInfo);
            else if (tmpScheInfo.ScheType == SCHE_TYPE.AfterSchool)
                schedule.scheInfo_AfterSchool.Add(tmpScheInfo);
            else if (tmpScheInfo.ScheType == SCHE_TYPE.Rest)
                schedule.scheInfo_Rest.Add(tmpScheInfo);

            ++cnt;
        }
        Debug.Log("로딩 끗! 총 갯수 : " + cnt);
    }

    public void CreateScheduleClick()
    {
        GameObject scheWindow = this.transform.Find("Schedule").gameObject;
        GameObject SchBtn = GameObject.Find("sch_btn"); //해당 함수를 호출하는 schedule 버튼

        //Schedule 화면 on
        scheWindow.SetActive(true);

        //기본적으로 학교 일정을 리스트에 띄워줌
        scheWindow.SendMessage("ViewScheduleInfos", schedule.scheInfo_School, SendMessageOptions.DontRequireReceiver);
        //03.23 스켸줄 메니저 생성하면서 삭제
        //schedule.ViewScheduleInfos(schedule.scheInfo_School);

        //본인 숨김
        SchBtn.SetActive(false);
    }

    //로비화면을 띄운다.
    public void SetLobbyScene()
    {
        //스켸줄 버튼
        GameObject SchBtn = this.transform.Find("sch_btn").gameObject; //해당 함수를 호출하는 schedule 버튼
        SchBtn.SetActive(true);
    }
}
