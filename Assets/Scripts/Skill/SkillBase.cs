using Cinemachine;
using ClientEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.TextCore.Text;
using UnityEngine.Timeline;

public class SkillBase : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] GameObject effect;
    [SerializeField] PlayableDirector director;
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    [SerializeField, ReadOnly] BaseCharacter character;
    [SerializeField, ReadOnly] State state;
    [SerializeField, ReadOnly] float value;

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

    public void Active(Transform target)
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

        switch (character.CharacterType)
        {
            case ClientEnum.CharacterType.Player:
                if (enemy.CharacterType == CharacterType.Enemy)
                {
                    enemy.Hit((long)(value * target));
                }
                break;
            case ClientEnum.CharacterType.Enemy:
                break;
            default:
                break;
        }
    }
}
