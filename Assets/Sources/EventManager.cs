using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    List<ScheInfo> daughterSchedule;
    int count;              //스켸줄 순서
    GameObject LobbyScene;  //스켸줄 종료후 로비화면 출력

    Daughter_Status daughter_stat;  //딸의 능력치

    //UI
    Image eventSceneBG;     //이벤트에 들어가는 배경화면
    Image eventScheneChar;  //이벤트에 들어가는 캐릭터 컷신
    Text messageTxt;        //성공, 실패가 출력되는 메세지

    void Awake()
    {
        eventSceneBG = this.GetComponent<Image>();
        eventScheneChar = GameObject.Find("cut scene").GetComponent<Image>();
        LobbyScene = GameObject.Find("Lobby");
        count = 0;

        messageTxt = GameObject.Find("Message Text").GetComponent<Text>();

        daughter_stat = Daughter_Status.Instance;
    }

    private EventManager() { }

    //딸의 스켸줄 리스트 복사
    public void GetDaughterSchedule(List<ScheInfo> daughterScheduleOriginal)
    {
        if (daughterSchedule == null)
            daughterSchedule = new List<ScheInfo>(daughterScheduleOriginal);
        else
            Debug.Log("Already copied daughter schedule!");
    }

    //일반적으로 실행되는 스켸줄
    public void NormalScheduleEvent()
    {
        //학교/알바의 경우 확률로 성공.
        float successProb = 0.5f;   //★0.5는 더미숫자
        float eventProb;            //이번 이벤트에서의 확률

        if(count == daughterSchedule.Count)
        {
            //딸의 스켸줄 ++
            daughter_stat.AddAge_Week();
            //로비화면 보이기
            LobbyScene.SetActive(true);
            LobbyScene.GetComponent<Lobby>().SetLobbyScene();
            this.gameObject.SetActive(false);
            //count 초기화
            count = 0;
            //스켸줄 종료
            return;
        }

        DrawBG(daughterSchedule[count].ScheType);

        eventProb = Random.Range(0.0f, 1.0f);
        Debug.Log("확률 : " + eventProb);
        Debug.Log("count : " + count);
        //성공
        if (eventProb <= successProb) 
        {
            DrawCharScene(daughterSchedule[count].ScheType, true);
            messageTxt.text = daughterSchedule[count].Remark + "을 성실히 수행했습니다. 성공!";
            //★소지금에서 차감 or 획득
        }
        //실패
        else
        {
            DrawCharScene(daughterSchedule[count].ScheType, false);
            messageTxt.text = daughterSchedule[count].Remark + " 중 다소 집중하지 못했습니다. 실패!";
            //★소지금에서 차감 or 획득
        }
        ++count;
    }

    //★일단 학교/방과후/휴식으로 간단하게만 나눠놓음. 매개변수, 함수 등 언제든지 수정될 수 잇음.
    void DrawBG(SCHE_TYPE scheType)
    {
        string bgLocation = "CutScenes_BG/";

        if (scheType == SCHE_TYPE.School)
        {
            bgLocation += "school";
        }
        else if(scheType == SCHE_TYPE.AfterSchool)
        {
            bgLocation += "park";
        }
        else if(scheType == SCHE_TYPE.Rest)
        {

        }
        eventSceneBG.sprite = Resources.Load<Sprite>(bgLocation) as Sprite;
    }

    //★BG처럼 일단 간단하게만 만들어놓음. 매개변수, 함수 등 언제든지 수정될 수 잇음.
    void DrawCharScene(SCHE_TYPE scheType, bool isSuccess)
    {
        int age = 10;   //★딸의 나이를 반영해야된다. 임시변수
        string charLocation = "CutScenes/";

        switch (age)
        {
            case 10:
                charLocation += "10/";
                if (scheType == SCHE_TYPE.School)
                {
                    charLocation += "school/20_01_";
                    if (isSuccess)
                        charLocation += "01";
                    else
                        charLocation += "02";
                }
                else if (scheType == SCHE_TYPE.AfterSchool)
                {
                    charLocation += "after_school/11_01_";
                    if (isSuccess)
                        charLocation += "01";
                    else
                        charLocation += "02";
                }
                else if (scheType == SCHE_TYPE.Rest)
                {
                    charLocation += "rest/";
                }
                break;
            default:
                break;
        }
        eventScheneChar.sprite = Resources.Load<Sprite>(charLocation) as Sprite;
    }

    void ModifyBudget(ScheInfo scheInfo)
    {

    }
}
