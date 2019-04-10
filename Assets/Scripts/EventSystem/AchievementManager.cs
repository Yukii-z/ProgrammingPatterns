using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    //enemy kill achievement
    public int[] killedEnemyNum = new int[3]{0,0,0};
    private int[] _aimEnemyNum = new int[3] {5, 5, 5};
    public int killedWaveNum = -1;
    private int _aimWaveNum = 2;
    public GameObject achievePrefab;
    public GameObject achieveColumn;
    
    //wave kill achievement
    
    //be in four corner achievement
    // Start is called before the first frame update
    private void Awake()
    {
        achievePrefab = Resources.Load<GameObject>("Prefabs/Flower (1)");
        achieveColumn = GameObject.Find("Column");
    }

    void Start()
    {
        Services.Event.AddHandler<EnemyKilled>(OnEnemyKilled);
        Services.Event.AddHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
        Services.Event.AddHandler<StayEnemyAchieve>(OnStayEnemyAchieve);
        Services.Event.AddHandler<WalkEnemyAchieve>(OnWalkEnemyAchieve);
        Services.Event.AddHandler<JumpEnemyAchieve>(OnJumpEnemyAchieve);
        Services.Event.AddHandler<WaveAchieve>(OnWaveAchieve);
        Services.Event.AddHandler<StayFourCorner>(OnStayFourCorner);
        Services.Event.AddHandler<StayCornerAchieve>(OnStayCornerAchieve);
    }

    private void OnDestroy()
    {
        Services.Event.RemoveHandler<EnemyKilled>(OnEnemyKilled);
        Services.Event.RemoveHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
        Services.Event.RemoveHandler<StayEnemyAchieve>(OnStayEnemyAchieve);
        Services.Event.RemoveHandler<WalkEnemyAchieve>(OnWalkEnemyAchieve);
        Services.Event.RemoveHandler<JumpEnemyAchieve>(OnJumpEnemyAchieve);
        Services.Event.RemoveHandler<WaveAchieve>(OnWaveAchieve);
        Services.Event.RemoveHandler<StayFourCorner>(OnStayFourCorner);
        Services.Event.RemoveHandler<StayCornerAchieve>(OnStayCornerAchieve);
    }

    void OnEnemyKilled(EnemyKilled evt)
    {
        int i= (int) evt.enemyType;
        // EnemyManager.Instance.enemyRecord.TryGetValue(evt.enemyType, out i);
        if (killedEnemyNum.Length > i)
        {
            killedEnemyNum[i]++;
            if (killedEnemyNum[i] == _aimEnemyNum[i])
            {
                switch (i)
                {
                    case 0:
                        Services.Event.Fire(new StayEnemyAchieve());
                        break;
                    case 1:
                        Services.Event.Fire(new WalkEnemyAchieve());
                        break;
                    case 2:
                        Services.Event.Fire(new JumpEnemyAchieve());
                        break;
                }

            }
        }
    }

    void OnEnemyWaveKilled(EnemyWaveKilled evt)
    {
        killedWaveNum++;
        if (_aimWaveNum == killedWaveNum)
        {
            Services.Event.Fire(new WaveAchieve());
        }
    }

    void OnStayEnemyAchieve(StayEnemyAchieve evt)
    {
        Debug.Log("You killed enough stay enemy!");
        Instantiate(achievePrefab, achieveColumn.transform);
    }

    void OnJumpEnemyAchieve(JumpEnemyAchieve evt)
    {
        Debug.Log("You killed enough jump enemy!");
        Instantiate(achievePrefab, achieveColumn.transform);
    }

    void OnWalkEnemyAchieve(WalkEnemyAchieve evt)
    {
        Debug.Log("You killed enough walk enemy!");
        Instantiate(achievePrefab, achieveColumn.transform);
    }

    void OnWaveAchieve(WaveAchieve evt)
    {
        Debug.Log("You killed enough wave!");
        Instantiate(achievePrefab, achieveColumn.transform);
    }

    private int cornerNum = 0;
    void OnStayFourCorner(StayFourCorner evt)
    {
        cornerNum++;
        if (cornerNum == 4)
        {
            Services.Event.Fire(new StayCornerAchieve());
        }
    }

    void OnStayCornerAchieve(StayCornerAchieve evt)
    {
        Debug.Log("You stayed four corner!");
        Instantiate(achievePrefab, achieveColumn.transform);       
    }

    public void Clear()
    {
        killedEnemyNum = new int[3]{0,0,0};
        killedWaveNum = -1;
        foreach (Transform child in achieveColumn.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
