using UnityEngine;

/**
This script opens URLs.
*/
public class OpenURL : MonoBehaviour
{
    public void OpenURLCall(string URL)
    {
        Application.OpenURL(URL);
    }
}
