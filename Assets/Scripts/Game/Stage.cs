using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField, ReadOnly] GameMode mode;
    [SerializeField, ReadOnly] StageResultPanel resultPanel;
    [SerializeField, ReadOnly] Map map;
    [SerializeField] List<Map> mapList;

    public Vector3 PlayerStart => map.Start;
    public GameMode Mode => mode;

    List<string> monsters = new List<string>();
    List<(int goodsIndex, int value)> rewards = new List<(int goodsIndex, int value)>();

    public void Set(GameMode gameMode)
    {
        mode = gameMode;
        rewards.Clear();
        monsters.Clear();

        switch (gameMode)
        {
            case GameMode.Boss:
            case GameMode.Stage:
                SetStage(gameMode);
                break;
            case GameMode.GoldDungeon:
            case GameMode.GemDungeon:
                SetDungeon(gameMode);
                break;
            default:
                break;
        }
    }

    public void EndStage()
    {
        switch (mode)
        {
            case GameMode.Stage:
                map.CreateEnemy(monsters,DataManager.Instance.GetInfo.Stage);
                break;
            case GameMode.Boss:
            case GameMode.GoldDungeon:
            case GameMode.GemDungeon:
                break;
            default:
                break;
        }
    }

    public void CheckStage()
    {
        switch (mode)
        {
            case GameMode.Stage:
                break;
            case GameMode.Boss:
                break;
            case GameMode.GoldDungeon:
                break;
            case GameMode.GemDungeon:
                break;
            default:
                break;
        }
    }

    public void StageFail()
    {
        UIManager.Instance.PanelClose(typeof(CommonPanel));
        resultPanel.SetResult(false);
        UIManager.Instance.AddPanel(resultPanel);
    }

    public void StageSuccess()
    {
        UIManager.Instance.PanelClose(typeof(CommonPanel));
        for (int i = 0; i < rewards.Count; i++)
        {
            DataManager.Instance.AddGoods((Goods)rewards[i].goodsIndex, rewards[i].value);
            resultPanel.AddGoods((Goods)rewards[i].goodsIndex,rewards[i].value);
        }

        resultPanel.SetResult(true, mode);
        UIManager.Instance.AddPanel(resultPanel);
    }

    void SetStage(GameMode gameMode)
    {
        StageData data = ScriptableManager.Instance.Get<StageDataScriptable>(ScriptableType.StageData).Get(DataManager.Instance.GetInfo.Stage);
        UIManager.Instance.SetStageTitle(data.nameKey, data.index);
        
        CreateMap(data.map);
        rewards = data.Rewards();
        data.Monsters(monsters);

        switch (gameMode)
        {
            case GameMode.Stage:
                map.CreateEnemy(data.monsters,DataManager.Instance.GetInfo.Stage);
                break;
            case GameMode.Boss:
                map.CreateEnemy(data.monsters, DataManager.Instance.GetInfo.Stage);
                map.CreateBoss(data.boss, DataManager.Instance.GetInfo.Stage);
                break;
            default:
                break;
        }
    }

    void SetDungeon(GameMode gameMode)
    {
        DungeonsData dungeonsData = ScriptableManager.Instance.Get<DungeonsDataScriptable>(ScriptableType.DungeonsData).GetDungeon(gameMode);
        int level = 0;
        int StageLevel = 0;

        CreateMap(dungeonsData.mapPrefab);
        dungeonsData.Monsters(monsters);

        switch (gameMode)
        {
            case GameMode.GoldDungeon:
                level = DataManager.Instance.GetInfo.CurrentGoldDungeon;
                StageLevel = 1 + (dungeonsData.targetAddStage * level);
                map.CreateBoss(monsters[0],StageLevel);
                break;
            case GameMode.GemDungeon:
                level = DataManager.Instance.GetInfo.CurrentGemDungeon;
                StageLevel = 1 + (dungeonsData.targetAddStage * level);
                map.CreateBoss(monsters[0], StageLevel);
                break;
            default:
                break;
        }

        UIManager.Instance.SetStageTitle(dungeonsData.nameLocal, level);
        rewards = dungeonsData.GetRewards(level);

    }

    void CreateMap(string mapData)
    {
        if (map != null && map.Pool == mapData)
        {
            return;
        }
        else
        {
            if (map != null)
            {
                map.gameObject.SetActive(false);
            }

            map = mapList.Find(x => x.Pool == mapData);
            map.gameObject.SetActive(true);
        }
    }
}
