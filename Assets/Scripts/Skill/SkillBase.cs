using Cinemachine;
using ClientEnum;
using JsonClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkillBase : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] GameObject effect;
    [SerializeField] PlayableDirector director;
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    [SerializeField, ReadOnly] protected List<SkillBuffs> buffs;
    [SerializeField, ReadOnly] protected BaseCharacter character;
    [SerializeField, ReadOnly] protected State state;
    [SerializeField, ReadOnly] protected float value;

    public string ID => id;

    public void UpdateValue()
    {
        DataManager.Skill skill = DataManager.Instance.Skills.Find(x => x.data.id == id);
        state = skill.data.State();
        value = skill.GetValue();
    }

    public virtual void SetSkill(BaseCharacter target)
    {
        UpdateValue();
        character = target;
        buffs = ScriptableManager.Instance.Get<SkillDataScriptable>(ScriptableType.SkillData).skillData.Find(x => x.id == id).skillBuffs;

        for (int i = 0; i < cameras.Count; i++)
        {
            if (GameManager.Instance.Player == character)
            {
                cameras[i].Follow = character.transform;
                cameras[i].gameObject.SetActive(true);
            }
            else
            {
                cameras[i].gameObject.SetActive(false);
            }
        }

        var timeline = director.playableAsset as TimelineAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name == "Animator")
            {
                director.SetGenericBinding(track, character.Ani);
            }
            else if (track.name == "Camera")
            {
                if (character == GameManager.Instance.Player)
                {
                    track.muted = false;
                    director.SetGenericBinding(track, GameManager.Instance.Brain);
                }
                else
                {
                    track.muted = true;
                }
            }
        }
    }

    public virtual void Active(Transform target)
    {
        effect.transform.position = target.position;
        effect.transform.rotation = target.rotation;
        gameObject.SetActive(true);
        director.Play();
    }

    public void Stop()
    {
        character.SetIdle();
        director.Stop();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        BaseCharacter enemy = other.GetComponentInParent<BaseCharacter>();
        float target = character.GetState(state);
        BigStats stats = character.GetBigState(state);

        switch (character.CharacterType)
        {
            case ClientEnum.CharacterType.Player:
                if (enemy.CharacterType == CharacterType.Enemy)
                {
                    if (state < State.HpRegen)
                    {
                        Attack(enemy, stats);
                    }
                    else
                    {
                        Attack(enemy, target);
                    }
                }
                break;
            case ClientEnum.CharacterType.Enemy:
                break;
            default:
                break;
        }
    }

    protected virtual void Attack(BaseCharacter enemy,float target)
    {
        BigStats bigStats = BigStats.Zero;
        bigStats += (int)target;
        bigStats *= value;

        BigStats attack = (enemy.Hit(bigStats) * character.DrainLife());
        character.AddHp(attack);
    }

    protected virtual void Attack(BaseCharacter enemy,BigStats stats)
    {
        BigStats bigStats = BigStats.Zero;
        bigStats += stats;
        bigStats *= value;

        BigStats attack = (enemy.Hit(bigStats) * character.DrainLife());
        character.AddHp(attack);
    }
}
