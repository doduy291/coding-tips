using UnityEngine;

public class ClassA : MonoBehaviour
{
    public string textA = "Yo! This is text from Class A";

    public void Initialize(string message)
    {
        Debug.Log(message);
    }
}
