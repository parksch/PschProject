using ClientEnum;
using System.Collections.Generic;
using UnityEngine;

namespace JsonClass
{
    public partial class ItemDataScriptable// This Class is a functional Class.
    {
        public Item RandomTarget()
        {
            return itemData[Random.Range(0,itemData.Count)].ItemType();
        }

        public State MainState(Item target)
        {
            return itemData.Find(x => x.ItemType() == target).MainState();
        }

        public string Atlas(Item target)
        {
            return itemData.Find(x => x.ItemType() == target).atlas;
        }

        public BaseItem GetItem(Item target,Grade grade)
        {
            ItemData typeData = itemData.Find(x => x.ItemType() == target);
            GradeItem gradeItem = typeData.GetGradeItem(grade);
            BaseItem baseItem = ItemFactory.Create(target);

            baseItem.Set(gradeItem);
            return baseItem;
        }

        public List<State> GetOptions(Item target)
        {
            ItemData typeData = itemData.Find(x => x.ItemType() == target);
            List<State> states = new List<State>();

            for (int i = 0; i < typeData.options.Count; i++)
            {
                states.Add((State)typeData.options[i]);
            }

            return states;
        }

        public int GetDismantle(Grade target)
        {
            DefaultValuesScriptable values = ScriptableManager.Instance.Get<DefaultValuesScriptable>(ScriptableType.DefaultValues);
            int value = (int)values.Get("Dismantle");

            switch (target)
            {
                case Grade.Uncommon:
                    value = (int)(value * 1.2f);
                    break;
                case Grade.Rare:
                    value = (int)(value * 1.5f);
                    break;
                case Grade.Epic:
                    value = (int)(value * 2f);
                    break;
                case Grade.Legendary:
                    value = (int)(value * 2.5f);
                    break;
                case Grade.Mythical:
                    value = (int)(value * 3f);
                    break;
                case Grade.Max:
                    value = (int)(value * 5f);
                    break;
                default:
                    break;
            }

            return value;
        }

    }

    public partial class ItemData
    {
        public Item ItemType()
        {
            return (Item)itemType;
        }

        public State MainState()
        {
            return (State)mainState;
        }

        public List<State> TargetStates()
        {
            List<State> states = new List<State>();

            //for (int i = 0; i < randomTarget.Count; i++)
            //{
            //    states.Add((ClientEnum.State)randomTarget[i]);
            //}

            return states;
        }

        public GradeItem GetGradeItem(Grade grade)
        {
            GradeItem gradeItem = gradeItems.Find(x => x.Grade() == grade);
            return gradeItem;
        }
    }

    public partial class GradeItem
    {
        public Grade Grade()
        {
            return (Grade)grade;
        }

        public ResourcesItem GetRandom()
        {
            return resourcesItems[Random.Range(0, resourcesItems.Count)];
        }
    }

    public partial class ResourcesItem
    {
        public string GetLocal()
        {
            return ScriptableManager.Instance.Get<LocalizationScriptable>(ScriptableType.Localization).Get(local);
        }

        //public ClientEnum.State MainState()
        //{
        //    return (ClientEnum.State)mainState;
        //}

    }

}
