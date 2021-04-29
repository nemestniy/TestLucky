using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolObjects<T>
    where T : MonoBehaviour
{
    public List<T> Objects;

    public PoolObjects()
    {
        Objects = new List<T>();
    }

    public void AddObject(T item)
    {
        if(!Objects.Contains(item))
            Objects.Add(item);
    }

    public T GetFreeObject()
    {
        var item = Objects.FirstOrDefault(h => !h.gameObject.activeSelf);

        if(item != null)
            item.gameObject.SetActive(true);

        return item;
    }
}
