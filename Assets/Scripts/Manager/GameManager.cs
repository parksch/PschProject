using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField,ReadOnly] Stage stage;
    [SerializeField,ReadOnly] PlayerCharacter player;
    [SerializeField,ReadOnly] List<EnemyCharacter> enemies;

    public PlayerCharacter Player => player;

    public void AddEnemy(EnemyCharacter enemy) => enemies.Add(enemy);
    public void RemoveEnemy(EnemyCharacter enemy) 
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            stage.CreateEnemy();
        }
    }

    public List<EnemyCharacter> Enemies => enemies;

    protected override void Awake()
    {
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        player.Init();
        stage.StartStage();
        UIManager.Instance.Init();
    }

    public BaseCharacter GetTarget(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Player:
                float dist = float.MaxValue;
                BaseCharacter character = null;

                for (int i = 0; i < enemies.Count; i++)
                {
                    float enemyDist = Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z), new Vector3(enemies[i].transform.position.x, 0, enemies[i].transform.position.z));
                    if (dist > enemyDist)
                    {
                        dist = enemyDist;
                        character = enemies[i];
                    }
                }

                return character;
            case CharacterType.Enemy:
                return player.IsDeath ? null : player;
        }

        return null;
    }

    public void AddGold()
    {
        long gold = (long)(ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).startGold * (1 + (DataManager.Instance.GetInfo.Stage * ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).multiplyPerGold)));
        DataManager.Instance.AddGold(gold);
    }

    public void AddExp()
    {
        long exp = (long)(ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).startExp * (1 + (DataManager.Instance.GetInfo.Stage * ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).multiplyperStageExp)));
        DataManager.Instance.AddExp(exp);
    }

    public void AddScrap()
    {
        if (Random.Range(0f,1f) < ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).scrapProbability)
        {
            int scrapMin = ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).scrapMin;
            int scrapMax = ScriptableManager.Instance.Get<StageScriptable>(ScriptableType.Stage).scrapMax;
            long scrap = Random.Range(scrapMin, scrapMax);
            DataManager.Instance.AddScrap(scrap);
        }
    }
}
