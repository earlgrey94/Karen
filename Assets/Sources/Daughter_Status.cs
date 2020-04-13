using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daughter_Status
{
    const float MIN_NORMAL_STATUS = 0.0f;
    const float MAX_NORMAL_STATUS = 999.9f;
    const int MIN_WORLD_AFFINITY = 0;
    const int MAX_WORLD_AFFINITY = 100;

    //일반 능력치
    private float STAT1_STAMINA;
    private float STAT1_INTELLIGEN;
    private float STAT1_CHARM;
    private float STAT1_NATURE;
    private float STAT1_GRACE;
    private float STAT1_STRESS;

    //세계 호감도
    private float STAT1_INFLU_HUMAN;
    private float STAT1_INFLU_ELF;
    private float STAT1_INFLU_SPLIT;
    private float STAT1_INFLU_HEAVEN;
    private float STAT1_INFLU_HELL;

    //나이
    int age;
    int age_week;
    public int Age_Week {
        get { return age_week; }
    }

    //single tone
    private static Daughter_Status instance;
    private Daughter_Status()
    {
        //★저장한 파일 가져와야됨
        age = 10;
        age_week = 1;
    }
    public static Daughter_Status Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Daughter_Status();
            }
            return instance;
        }
    }

    public void AddAge_Week() { ++age_week; }
}
