using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//스켸줄 생성, 삭제, 실행 관리
public class Schedule : MonoBehaviour
{
    private static Schedule instance = null;    //singletone

    public List<ScheInfo> scheInfo_School;
    public List<ScheInfo> scheInfo_AfterSchool;
    public List<ScheInfo> scheInfo_Rest;

    //리스트에 표시되는 리스트 아이템 prefab
    public Transform scheItemBtnPrefab;
    //몇 개까지 스켸줄에 넣었는지 표시
    List<ScheInfo> dautherScheduleList; //근데 ScheInfo객체 리스트인지 잘 모르겠다; 앞으로 이걸 어떻게 써먹을지 모르겠음...
    int scheduleCount = 0;
    const int MAX_SCHEDULE_COUNT = 7;
    //dummy img
    //빈 버튼 BackGroupd sprite
    Sprite[] emptyBGSprite;
    enum ICON_ARRAY { School, Afer_School, Rest }
    Sprite[] iconArray;

    public static Schedule Instance
    {
        get {
            if (instance == null)
                instance = new Schedule();
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

    public void SetVisibleStatImg()
    {
        GameObject statImg = GameObject.Find("statImg");


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
        Text schInfoTxt = GameObject.Find("schInfoTxt").GetComponent<Text>(); ;

        schInfoTxt.text = scheInfoList[0].GetScheduleInfo();
        
    }

    //각 화면의 스켸줄 info를 클릭했을 때
    public void AddSchedule()
    {
        //[dummy source] 아이콘을 임시로 바꾸기 위함
        instance.iconArray = new Sprite[3];
        instance.iconArray[(int)ICON_ARRAY.School] = Resources.Load<Sprite>("Sche_School") as Sprite;
        instance.iconArray[(int)ICON_ARRAY.Afer_School] = Resources.Load<Sprite>("Sche_AfterSchool") as Sprite;

        //임시로 이미지만 바꿈
        string imageName = "scheBtn" + (int)(scheduleCount + 1);
        Image currentScheBtnImg = GameObject.Find(imageName).GetComponent<Image>();

        if (scheduleCount < 4)
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = instance.iconArray[(int)ICON_ARRAY.School];
        }
        else
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = instance.iconArray[(int)ICON_ARRAY.Afer_School];
        }

        ++scheduleCount;


        if (scheduleCount == 4)
        {
            //리스트 내용을 방과후로 전환
            ViewScheduleInfos(instance.scheInfo_AfterSchool);
        }
        else if (scheduleCount == MAX_SCHEDULE_COUNT)
        {
            //스켸줄 실행
            GameObject StartScheduleWindow = this.transform.Find("StartScheduleBG").gameObject;
            StartScheduleWindow.SetActive(true);
        }
        Timer(0.5f);
    }

    public void DeleteSchedule()
    {
        if (scheduleCount <= 0)
            return;
        //임시로 이미지만 지움
        string imageName = "scheBtn" + (int)(scheduleCount);
        Image currentScheBtnImg = GameObject.Find(imageName).GetComponent<Image>();
        instance.emptyBGSprite = new Sprite[2];
        instance.emptyBGSprite[(int)ICON_ARRAY.School] = Resources.Load<Sprite>("base3") as Sprite;
        instance.emptyBGSprite[(int)ICON_ARRAY.Afer_School] = Resources.Load<Sprite>("base_after") as Sprite;

        if (scheduleCount < 4)
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = instance.emptyBGSprite[(int)ICON_ARRAY.School];
        }
        else
        {
            if (currentScheBtnImg != null)
                currentScheBtnImg.sprite = instance.emptyBGSprite[(int)ICON_ARRAY.Afer_School];
        }
        //리스트 내용을 학교로 전환
        if (scheduleCount <= 4)
        {
            ViewScheduleInfos(instance.scheInfo_School);
        }

        --scheduleCount;
    }
  

    IEnumerator Timer(float delta_time)
    {
        yield return new WaitForSeconds(delta_time);
    }
}