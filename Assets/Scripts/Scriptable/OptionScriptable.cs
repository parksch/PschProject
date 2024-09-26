using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Option", menuName = "Scriptable/Option")]
public class OptionScriptable : BaseScriptable
{
    [SerializeField] List<Data> datas = new List<Data>();
    [SerializeField] List<Probability> addOption = new List<Probability>();

    [System.Serializable]
    public class Probability
    {
        [SerializeField] ClientEnum.Grade target;
        [SerializeField] List<Datas.Pair<int, int>> count;
        [SerializeField] List<Datas.Pair<ClientEnum.Grade, int>> gradeValue;
    }

    [System.Serializable]
    public class Data
	{
        [SerializeField] ClientEnum.State target;
        [SerializeField] float min = 0.9f, max = 1.1f;
		[SerializeField] string local;
		[SerializeField] List<Datas.Pair<ClientEnum.Grade, float>> gradeValue = new List<Datas.Pair<ClientEnum.Grade, float>>();
		
		public ClientEnum.State Target => target;
		public float Value(ClientEnum.Grade grade) => Mathf.Ceil(gradeValue.Find(x => x.key == grade).value * Random.Range(min, max) * 100f)/100f;
		public string Local => local;
    }

    [System.Serializable]
    public class ResultOption
    {
        [SerializeField] List<Data> datas = new List<Data>();

    }

	public Data GetData(ClientEnum.State target) => datas.Find(x => x.Target == target);

    //public ResultOption GetGradeOption(ClientEnum.Grade grade)
    //{

    //}
}
