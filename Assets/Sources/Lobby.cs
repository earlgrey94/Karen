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
        FileOpen("Schedule_List.csv");  //스켸줄 정보 파일에서 읽음
    }

    // Update is called once per frame
    void Update()
    {
        
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
                continue;

            //빈 줄이 들어오면 return
            if (string.IsNullOrEmpty(lineStr))
                return;

            keys = lineStr.Split(',');
            ScheInfo tmpScheInfo = new ScheInfo(keys);

            //Schedule Type에 따라 다른 list에 넣어줌
            if (tmpScheInfo.ScheType == SCHE_TYPE.School)
                schedule.scheInfo_School.Add(tmpScheInfo);
            else if (tmpScheInfo.ScheType == SCHE_TYPE.AfterSchool)
                schedule.scheInfo_AfterSchool.Add(tmpScheInfo);
            else if (tmpScheInfo.ScheType == SCHE_TYPE.Rest)
                schedule.scheInfo_Rest.Add(tmpScheInfo);

            ++cnt;
        }
    }

    public void CreateScheduleClick()
    { }
}
