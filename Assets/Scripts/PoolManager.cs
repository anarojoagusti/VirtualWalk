using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    public void Load(string key, GameObject prefab, int quantity)
    {
        if (!pool.ContainsKey(key))
        {
            pool.Add(key, new List<GameObject>());
        }

        for (int i = 0; i < quantity; i++)
        {
            var prefab_ = Instantiate(prefab);
            prefab_.name = prefab.name;
            prefab_.SetActive(false);
            pool[key].Add(prefab_);
        }
    }

    public void Spawn (string key, GameObject prefab, int element, Vector3 spawnPosition)
    {
        //GameManager.instance.SetSmashedText(false);
        if (!pool.ContainsKey(key))
        {
            Load(key, prefab, 1);
        }

        //int i = -1;
        foreach(GameObject goInList in pool[key])
        {
            //i++;
            if (goInList.name == pool[key][element].name)
            {
                var thisPrefab = pool[key][element];
                thisPrefab.SetActive(true);
                thisPrefab.transform.position = spawnPosition;
            }
        }
    }

    public void Despawn (GameObject prefab)
    {
        prefab.SetActive(false);
    }
}
