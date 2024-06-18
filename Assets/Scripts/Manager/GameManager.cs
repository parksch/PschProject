using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Stage stage;
    [SerializeField] PlayerCharacter player;


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
    }

    public BaseCharacter GetTarget(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Player:
                break;
            case CharacterType.Enemy:
                return player;
        }

        return null;
    }
}
