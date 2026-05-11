using UnityEngine;

public class SensorScript : MonoBehaviour
{
    public GameObject Quiz;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(){
        Quiz.SetActive(true);
    }

    void OnTriggerExit(){
        Quiz.SetActive(false);
    }
}
