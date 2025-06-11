using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectionUI : MonoBehaviour
{
    [SerializeField] List<GameObject> gemCollection;

    public void CheckCollectionStatus()
    {
        for (int i = 0; i < gemCollection.Count; i++)
        {
            if (gemCollection[i] != null && GemsCollectionInventory.Instance.gemDatas[i].collectedCount > 0)
            {
                gemCollection[i].SetActive(true);
            }
            else
            {
                gemCollection[i].SetActive(false);
            }
        }
    }
}
