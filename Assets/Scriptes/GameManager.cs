using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform[] rect_TP_Poz; //TP - Turning Point
    [SerializeField] Enermy enermy;
    [SerializeField] PanelControl panelControl;

    [SerializeField] TextMeshProUGUI txt_Timer;
    [SerializeField] Button btn_Restart;

    [SerializeField] TextMeshProUGUI txt_EnermyCount;
    int enermy_Max_Count = 100;


    [SerializeField] public List<GameObject> list_Obj_spawnEnermy = new List<GameObject>();
    [SerializeField] public List<GameObject> list_Obj_SpawnCharaters = new List<GameObject>(); // 소환한 캐릭터
    Dictionary<int, List<int>> list_Round_Spawn = new Dictionary<int, List<int>>();

    public bool timer = false;
    float time;

    int round = 0;
    //----------------------------------------
    public static GameManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        GameManager_Init();
        Set_Dictionry_List();

        enermy.Enermy_Init();
        panelControl.PanelControl_Init();
    }

    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
            Timer(time);
            SetRound((int)time);
        }
    }

    void GameManager_Init()
    {
        GameManager_Btn_Init();
    }

    void GameManager_Btn_Init()
    {
        btn_Restart.onClick.RemoveAllListeners();

        btn_Restart.onClick.AddListener(() => {
            enermy.Clear_Enermy();
            timer = true;
            time = 0;
            round = 0;
        });
    }

    void Timer(float time)
    {
        int min = (int)time / 60;
        int second = (int)time % 60;

        txt_Timer.SetText(min + " : " + second);
    }

    public void Coroutine(IEnumerator name)
    {
        StartCoroutine(name);
    }

    public void SetEnermyCount()
    {
        if (list_Obj_spawnEnermy.Count == enermy_Max_Count)
        {
            timer = false;
            txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
            txt_EnermyCount.color = Color.red;
        }
        else if (list_Obj_spawnEnermy.Count > enermy_Max_Count * 0.6f) // 60 퍼
        {
            txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
            txt_EnermyCount.color = Color.yellow;
        }
        else
        {
            txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
            txt_EnermyCount.color = Color.black;
        }
    }

    void SetRound(int time)
    {
        switch (time)
        {
            case 10: //10Sec
                if(round == 0)
                {
                    Coroutine(Round_Spawn_Enermy());
                }
                break;
            case 60: // 1분
                if(round == 1)
                {
                    Coroutine(Round_Spawn_Enermy());
                }
                break;
            case 300: // 5min
                if(round == 2)
                {
                    Coroutine(Round_Spawn_Enermy());
                }
                break;
        }
    }

    IEnumerator Round_Spawn_Enermy()
    {
        round++;
        for (int i=0; i < list_Round_Spawn[round][1]; i++)
        {
            enermy.Spawn_Enermy(list_Round_Spawn[round][0]);
            yield return new WaitForSeconds(2f);
        }
        yield break;
    }




    void Set_Dictionry_List()
    {
        // prefabNum, Count
        list_Round_Spawn.Add(1, new List<int> { 0, 10 });
    }
}
