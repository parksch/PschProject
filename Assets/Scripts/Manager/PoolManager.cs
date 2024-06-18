using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectScriptable;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] Transform uiParent;
    [SerializeField] ObjectScriptable objectScriptable;
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
        objectScriptable = Resources.Load<ObjectScriptable>("Scriptable/Object");
    }

    public class PoolObject
    {
        public GameObject prefab;
        public Queue<GameObject> queue= new Queue<GameObject>();
        public ClientEnum.ObjectType type;
    }

    public GameObject Dequeue(string name)
    {
        if (!poolObjects.ContainsKey(name))
        {
            AddPoolObject(name);
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

        switch (poolObject.type)
        {
            case ClientEnum.ObjectType.Object:
                gameObject.transform.SetParent(transform);
                break;
            case ClientEnum.ObjectType.UI:
                gameObject.transform.SetParent(uiParent);
                break;
            default:
                break;
        }

        poolObject.queue.Enqueue(gameObject);
    }

    void AddPoolObject(string name)
    {
        ObjectPrefab objectPrefab = objectScriptable.GetObject(name);
        poolObjects[name] = new PoolObject();
        poolObjects[name].prefab = objectPrefab.GameObject;
        poolObjects[name].type = objectPrefab.objectType;
    }

    void CreatePrefab(PoolObject  poolObject)
    {
        GameObject gameObject = null;

        switch (poolObject.type)
        {
            case ClientEnum.ObjectType.Object:
                gameObject = Instantiate(poolObject.prefab,transform);
                break;
            case ClientEnum.ObjectType.UI:
                gameObject = Instantiate(poolObject.prefab, uiParent);
                break;
            default:
                break;
        }

        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;

        poolObject.queue.Enqueue(gameObject);
    }
}
