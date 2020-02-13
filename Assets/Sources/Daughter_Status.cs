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
}
