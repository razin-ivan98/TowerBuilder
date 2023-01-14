using UnityEngine;

public class Controls : MonoBehaviour
{
    public Crane _crane;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _crane.Release();
        }
    }
}
