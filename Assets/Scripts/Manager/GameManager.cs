using Cinemachine;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField,ReadOnly] Transform skillParent;
    [SerializeField,ReadOnly] CinemachineBrain brain;
    [SerializeField,ReadOnly] CinemachineVirtualCamera main;
    [SerializeField,ReadOnly] Stage stage;
    [SerializeField,ReadOnly] PlayerCharacter player;
    [SerializeField,ReadOnly] List<EnemyCharacter> enemies;
    [SerializeField,ReadOnly] List<EnemyCharacter> deathEnemies;

    public delegate void ChangeGameMode(ClientEnum.GameMode mode);
    public delegate void EnemyDeath(EnemyCharacter enemy);

    public EnemyDeath OnEnemyDeath;
    public ChangeGameMode OnChangeGameMode;

    int scrapMin = 0;
    int scrapMax = 0;
    float scrapProbability = 0;

    #region GetStatus
    public Vector3 PlayerStart => stage.PlayerStart;
    public Transform SkillParent => skillParent;
    public CinemachineBrain Brain => brain;
    public CinemachineVirtualCamera Main => main;
    public PlayerCharacter Player => player;
    public List<EnemyCharacter> Enemies => enemies;
    public ClientEnum.GameMode Mode => stage.Mode;
    public void AddEnemy(EnemyCharacter enemy) => enemies.Add(enemy);
    public BaseCharacter GetTarget(ClientEnum.CharacterType characterType)
    {
        switch (characterType)
        {
            case ClientEnum.CharacterType.Player:
                float dist = float.MaxValue;
                BaseCharacter character = enemies.Find(x => x.IsBoss);

                if (character != null)
                {
                    return character;
                }

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
            case ClientEnum.CharacterType.Enemy:
                return player.IsDeath ? null : player;
        }

        return null;
    }
    #endregion

    protected override void Awake()
    {
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        OnEnemyDeath = null;
        OnEnemyDeath += _ => { AddGold(); };
        OnEnemyDeath += _ => { AddExp(); };
        OnEnemyDeath += _ => { AddScrap(); };
        OnEnemyDeath += RemoveEnemy;
        OnEnemyDeath += AddDeathEnemy;

        OnChangeGameMode = null;
        OnChangeGameMode += SetGameMode;

        scrapMin = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).scrapMin;
        scrapMax = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).scrapMax;
        scrapProbability = ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).scrapProbability;

        player.Init();
        UIManager.Instance.Init();
        OnChangeGameMode(ClientEnum.GameMode.Stage);
    }

    public void StageFail()
    {
        UIManager.Instance.OffEscape();
        ResetStage();
        stage.StageFail();
        player.SetPlayerPos(PlayerStart);
    }

    public void StageSuccess()
    {
        UIManager.Instance.OffEscape();
        ResetStage();
        stage.StageSuccess();
        player.SetPlayerPos(PlayerStart);
    }

    public void RemoveDeathEnemy(EnemyCharacter enemy)
    {
        deathEnemies.Remove(enemy);
    }

    public void BossDeath()
    {
        UIManager.Instance.OffEscape();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SystemDeath();
            i--;
        }
    }

    void SetGameMode(ClientEnum.GameMode gameMode)
    {
        ResetStage();
        stage.Set(gameMode);
        player.SetPlayerPos(PlayerStart);
        main.transform.position = PlayerStart;
    }
    void AddGold()
    {
        if (Mode != ClientEnum.GameMode.Stage)
        {
            return;
        } 

        long gold = (long)(ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).startGold * (1 + (DataManager.Instance.GetInfo.Stage * ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).multiplyPerGold)));
        DataManager.Instance.AddGoods(ClientEnum.Goods.Gold, gold);
    }
    void AddExp()
    {
        if (Mode != ClientEnum.GameMode.Stage)
        {
            return;
        }

        long exp = (long)(ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).startExp * (1 + (DataManager.Instance.GetInfo.Stage * ScriptableManager.Instance.Get<StageOptionScriptable>(ScriptableType.StageOption).multiplyperStageExp)));
        DataManager.Instance.OnChangeExp(exp);
    }
    void AddScrap()
    {
        if (Mode != ClientEnum.GameMode.Stage)
        {
            return;
        }

        if (Random.Range(0f,1f) < scrapProbability)
        {
            long scrap = Random.Range(scrapMin, scrapMax);
            DataManager.Instance.AddGoods(ClientEnum.Goods.Scrap, scrap);
        }
    }
    void ResetStage()
    {
        foreach (var item in enemies)
        {
            item.ResetBuff();
            item.Enqueue();
        }

        foreach (var item in deathEnemies)
        {
            item.ResetBuff();
            item.Enqueue();
        }

        enemies.Clear();
        deathEnemies.Clear();
        player.ResetPlayer();
        UIManager.Instance.ResetSkill();
    }
    void RemoveEnemy(EnemyCharacter enemy)
    {
        enemies.Remove(enemy);
        stage.CheckStage();

        if (enemies.Count == 0)
        {
            stage.EndStage();
        }
    }
    void AddDeathEnemy(EnemyCharacter enemy)
    {
        deathEnemies.Add(enemy);
    }
}
