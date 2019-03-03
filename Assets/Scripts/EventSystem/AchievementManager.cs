using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    static private AchievementManager instance;
    static public AchievementManager Instance {
        get {
            if (instance == null) {
                instance = new AchievementManager();
            }
            return instance;
        }
    }
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
    void Start()
    {
        EventManager.Instance.AddHandler<EnemyKilled>(OnEnemyKilled);
        EventManager.Instance.AddHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
        EventManager.Instance.AddHandler<StayEnemyAchieve>(OnStayEnemyAchieve);
        EventManager.Instance.AddHandler<WalkEnemyAchieve>(OnWalkEnemyAchieve);
        EventManager.Instance.AddHandler<JumpEnemyAchieve>(OnJumpEnemyAchieve);
        EventManager.Instance.AddHandler<WaveAchieve>(OnWaveAchieve);
        EventManager.Instance.AddHandler<StayFourCorner>(OnStayFourCorner);
        EventManager.Instance.AddHandler<StayCornerAchieve>(OnStayCornerAchieve);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveHandler<EnemyKilled>(OnEnemyKilled);
        EventManager.Instance.RemoveHandler<EnemyWaveKilled>(OnEnemyWaveKilled);
        EventManager.Instance.RemoveHandler<StayEnemyAchieve>(OnStayEnemyAchieve);
        EventManager.Instance.RemoveHandler<WalkEnemyAchieve>(OnWalkEnemyAchieve);
        EventManager.Instance.RemoveHandler<JumpEnemyAchieve>(OnJumpEnemyAchieve);
        EventManager.Instance.RemoveHandler<WaveAchieve>(OnWaveAchieve);
        EventManager.Instance.RemoveHandler<StayFourCorner>(OnStayFourCorner);
        EventManager.Instance.RemoveHandler<StayCornerAchieve>(OnStayCornerAchieve);
    }

    void OnEnemyKilled(EnemyKilled evt)
    {
        int i= (int) evt.enemyType;
        // EnemyManager.Instance.enemyRecord.TryGetValue(evt.enemyType, out i);
        killedEnemyNum[i]++;
        if (killedEnemyNum[i] == _aimEnemyNum[i])
        {
            switch (i)
            {
                case 0:
                    EventManager.Instance.Fire(new StayEnemyAchieve());
                    break;
                case 1:
                    EventManager.Instance.Fire(new WalkEnemyAchieve());
                    break;
                case 2:
                    EventManager.Instance.Fire(new JumpEnemyAchieve());
                    break;
            }

        }
    }

    void OnEnemyWaveKilled(EnemyWaveKilled evt)
    {
        killedWaveNum++;
        if (_aimWaveNum == killedWaveNum)
        {
            EventManager.Instance.Fire(new WaveAchieve());
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
            EventManager.Instance.Fire(new StayCornerAchieve());
        }
    }

    void OnStayCornerAchieve(StayCornerAchieve evt)
    {
        Debug.Log("You stayed four corner!");
        Instantiate(achievePrefab, achieveColumn.transform);       
    }
}
