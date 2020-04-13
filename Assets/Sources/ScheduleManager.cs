using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    Schedule schedule;
    //GameObject scheWindow;
    int scheduleCount = 0;
    const int MAX_SCHEDULE_COUNT = 7;

    // [ dummy img ]
    //빈 버튼 BackGroupd sprite
    Sprite[] emptyBGSprite;
    enum ICON_ARRAY { School, Afer_School, Rest }
    Sprite[] iconArray;

    void Start()
    {
        schedule = Schedule.Instance;
        //scheWindow = this.transform.Find("Schedule").gameObject;
    }

    public void SetVisibleStatImg()
    {
        GameObject statImg = this.transform.Find("statImg").gameObject;


        if (statImg != null)
        {
            if (statImg.active == true)
                statImg.SetActive(false);
            else
                statImg.SetActive(true);
        }
    }

    //일정리스트를 스크롤리스트에 띄움
    //[변수]scheInfoList : 스크롤 리스트에 띄워야 하는 리스트
    //     - scheInfo_School
    //     - scheInfo_AfterSchool
    //     - scheInfo_Rest
    public void ViewScheduleInfos(List<ScheInfo> scheInfoList)
    {
        Text schInfoTxt = GameObject.Find("schInfoTxt").GetComponent<Text>();
        Text schIDTxt = GameObject.Find("schID").GetComponent<Text>();

        schInfoTxt.text = scheInfoList[0].GetScheduleInfo();
        schIDTxt.text = scheInfoList[0].ScheGroupID + "";
    }

    public void ViewScheduleInfo(string schInfo)
    {
        switch (schInfo)
        {
            case "school":
                if (scheduleCount < 4)
                    ViewScheduleInfos(schedule.scheInfo_School);
                else
                    ViewScheduleInfos(schedule.scheInfo_AfterSchool);
                break;
            case "rest":
                ViewScheduleInfos(schedule.scheInfo_Rest);
                break;
            default:
                Debug.Log("Invalid list call");
                break;
        }
    }

    public ScheInfo GetScheInfoByScheID(int schID)
    {
        //학교, 방과후, 휴식 차례대로 검색
        //학교 검색
        for (int i = 0; i < schedule.scheInfo_School.Count; ++i)
        {
            if (schID.Equals(schedule.scheInfo_School[i].ScheGroupID) == true)
                return schedule.scheInfo_School[i];
        }
        //방과후 검색
        for (int i = 0; i < schedule.scheInfo_AfterSchool.Count; ++i)
        {
            if (schID.Equals(schedule.scheInfo_AfterSchool[i].ScheGroupID) == true)
                return schedule.scheInfo_AfterSchool[i];
        }
        //휴식 검색
        for (int i = 0; i < schedule.scheInfo_Rest.Count; ++i)
        {
            if (schID.Equals(schedule.scheInfo_Rest[i].ScheGroupID) == true)
                return schedule.scheInfo_Rest[i];
        }

        return null;
    }

    //스켸줄 등록
    public void AddSchedule(int schID)
    {
        ScheInfo tmpSchInfo = GetScheInfoByScheID(schID);
        if (tmpSchInfo == null)
        {
            Debug.Log(schID + " : schedule을 찾지 못했습니다...");
            return;
        }
        schedule.AddDautherSchedule(tmpSchInfo);

        //[dummy source] 아이콘을 임시로 바꾸기 위함
        iconArray = new Sprite[3];
        iconArray[(int)ICON_ARRAY.School] = Resources.Load<Sprite>("Sche_School") as Sprite;
        iconArray[(int)ICON_ARRAY.Afer_School] = Resources.Load<Sprite>("Sche_AfterSchool") as Sprite;

        //임시로 이미지만 바꿈
        string imageName = "scheBtn" + (int)(scheduleCount + 1);
        Image currentScheBtnImg = GameObject.Find(imageName).GetComponent<Image>();

        if (scheduleCount < 4)
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = iconArray[(int)ICON_ARRAY.School];
        }
        else
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = iconArray[(int)ICON_ARRAY.Afer_School];
        }

        ++scheduleCount;


        if (scheduleCount == 4)
        {
            //리스트 내용을 방과후로 전환
            ViewScheduleInfos(schedule.scheInfo_AfterSchool);
        }
        else if (scheduleCount == MAX_SCHEDULE_COUNT)
        {
            //스켸줄 실행
            GameObject StartScheduleWindow = this.transform.Find("StartScheduleBG").gameObject;
            StartScheduleWindow.SetActive(true);

            Text schInfoTxt = GameObject.Find("sch_inform2").GetComponent<Text>();

            //생활비 구하기
            int totalBudget = schedule.GetTotalScheduleDebut();
            schInfoTxt.text = string.Format("총 {0}원이 따님 생활비로 사용 예상됩니다.", totalBudget);
        }
        Timer(0.5f);
    }

    //등록된 스켸줄 삭제
    public void DeleteSchedule()
    {
        if (scheduleCount <= 0)
            return;

        schedule.DeleteDautherSchedule();

        //임시로 이미지만 지움
        string imageName = "scheBtn" + (int)(scheduleCount);
        Image currentScheBtnImg = GameObject.Find(imageName).GetComponent<Image>();
        emptyBGSprite = new Sprite[2];
        emptyBGSprite[(int)ICON_ARRAY.School] = Resources.Load<Sprite>("base3") as Sprite;
        emptyBGSprite[(int)ICON_ARRAY.Afer_School] = Resources.Load<Sprite>("base_after") as Sprite;

        if (scheduleCount < 4)
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = emptyBGSprite[(int)ICON_ARRAY.School];
        }
        else
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = emptyBGSprite[(int)ICON_ARRAY.Afer_School];
        }
        //리스트 내용을 학교로 전환
        if (scheduleCount <= 4)
        {
            ViewScheduleInfos(schedule.scheInfo_School);
        }

        --scheduleCount;
    }

    //스켸줄을 모두 등록했을 때 "예"버튼 listner
    public void Sch_OKBtnListner()
    {
        GameObject startScheduleBG = GameObject.Find("StartScheduleBG").gameObject;
        GameObject eventObj = GameObject.Find("Canvas").transform.Find("Event").gameObject;
        
        
        WriteFileCurrentSchedule(); //실행한 스켸줄을 파일에 저장
        startScheduleBG.SetActive(false);   //큐브 화면 끔

        //이벤트에 딸 스켸줄 리스트 전달해주고 첫 이벤트 화면 켬
        eventObj.SetActive(true);           //이벤트 화면 켬
        eventObj.GetComponent<EventManager>().GetDaughterSchedule(schedule.DaughterSchedule);
        eventObj.GetComponent<EventManager>().NormalScheduleEvent();

        ResetSchedule();    //현재 등록되어있는 스켸줄 reset
        this.gameObject.SetActive(false); //자기자신 끔
    }

    //스켸줄을 모두 등록했을 때 "아니"버튼 listner
    public void Sch_cancelBtnListner()
    {
        GameObject startScheduleBG = GameObject.Find("StartScheduleBG").gameObject;

        startScheduleBG.SetActive(false);
        DeleteSchedule();
    }

    //스켸줄을 모두 등록한 후 실행했을 때 해당 스켸줄 내용 파일에 저장
    void WriteFileCurrentSchedule()
    {
        FileStream fs = new FileStream("Assets\\ScheduleHistory.txt", FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
        Daughter_Status daughter_stat = Daughter_Status.Instance;
        string str = "";

        str += daughter_stat.Age_Week + " ";   //★1은 더미. 회차가 들어가야 함
        str += schedule.GetAllDaughterScheduleID();
        sw.WriteLine(str);

        sw.Flush();
        sw.Close();
        fs.Close();
    }

    void ResetSchedule()
    {
        for (int i = 0; i < MAX_SCHEDULE_COUNT; ++i)
            DeleteSchedule();
    }

    //실질적으로 스켸줄을 실행하는 함수
    //일단 더미 이미지만 불러온다
    public void StartSchedule()
    {
        for (int i = 0; i < MAX_SCHEDULE_COUNT; ++i)
        {
            
        }
    }

    IEnumerator Timer(float delta_time)
    {
        yield return new WaitForSeconds(delta_time);
    }
}
