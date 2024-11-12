using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField,ReadOnly] Transform uiParent;
    Dictionary<string,PoolObject> poolObjects = new Dictionary<string, PoolObject>();

    protected override void Awake()
    {
        base.Awake();
        if (uiParent == null)
        {
            GameObject gameObject = new GameObject("UIParent");
            gameObject.transform.SetParent(transform);
            gameObject.AddComponent<Canvas>();
            gameObject.SetActive(false);
            uiParent = gameObject.transform;
        }
    }

    public class PoolObject
    {
        public GameObject prefab;
        public Queue<GameObject> queue= new Queue<GameObject>();
        public ClientEnum.ObjectType type;
    }

    public GameObject Dequeue(ClientEnum.ObjectType objectType,string name)
    {
        if (!poolObjects.ContainsKey(name))
        {
            AddPoolObject(objectType, name);
        }

        if (poolObjects[name].queue.Count <= 0)
        {
            CreatePrefab(poolObjects[name]);
        }

        GameObject gameObject = poolObjects[name].queue.Dequeue();

        return gameObject;
    }

    public void Enqueue(string name,GameObject gameObject)
    {
        PoolObject poolObject = poolObjects[name];
        gameObject.SetActive(false);
        gameObject.transform.localPosition = Vector3.zero;

        switch (poolObject.type)
        {
            case ClientEnum.ObjectType.UI:
                gameObject.transform.SetParent(uiParent);
                break;
            default:
                gameObject.transform.SetParent(transform);
                break;
        }

        poolObject.queue.Enqueue(gameObject);
    }

    void AddPoolObject(ClientEnum.ObjectType objectType,string name)
    {
        poolObjects[name] = new PoolObject();
        poolObjects[name].type = objectType;
        poolObjects[name].prefab = Resources.Load<GameObject>($"Prefab/{ClientEnum.EnumString<ClientEnum.ObjectType>.ToString(objectType)}/{name}");
    }

    void CreatePrefab(PoolObject  poolObject)
    {
        GameObject gameObject = null;

        switch (poolObject.type)
        {
            case ClientEnum.ObjectType.UI:
                gameObject = Instantiate(poolObject.prefab, uiParent);
                break;
            default:
                gameObject = Instantiate(poolObject.prefab, transform);
                break;
        }

        gameObject.name = poolObject.prefab.name;
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;

        poolObject.queue.Enqueue(gameObject);
    }
}
