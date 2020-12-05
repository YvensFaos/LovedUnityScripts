using UnityEngine;

/**
This script grants static methods, such as RemoveAllChidrenFrom one Game Object.
*/
public class GameObjectUtils
{
    public static void RemoveAllChidrenFrom(GameObject removeChildrenFrom)
    {
        int childCount = removeChildrenFrom.transform.childCount;
        while (childCount-- > 0)
        {
            GameObject.DestroyImmediate(removeChildrenFrom.transform.GetChild(0).gameObject);
        }
    }
}
