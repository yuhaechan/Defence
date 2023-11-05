using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

        enermy.Enermy_Init();
        panelControl.PanelControl_Init();
    }

    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
            Timer(time);
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
            enermy.Spawn_Enermy();
            //character.Find_Target();
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
        if(list_Obj_spawnEnermy.Count == enermy_Max_Count)
        {
            timer = false;
            txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
            txt_EnermyCount.color = Color.red;
        }
        else if(list_Obj_spawnEnermy.Count > list_Obj_spawnEnermy.Count* 0.6f) // 60 퍼
        {
            txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
            txt_EnermyCount.color = Color.yellow;
        }
        txt_EnermyCount.SetText("( " + list_Obj_spawnEnermy.Count + " / " + enermy_Max_Count + " )");
    }

    void SetRound()
    {

    }

    IEnumerator Round_Spawn_Enermy()
    {
        while (true)
        {

            yield return new WaitForSeconds(2f);
        }
    }
}
