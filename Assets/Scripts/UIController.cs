using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private float startingTime = 300f;
    private float currentTime = 0f;
    public Text time;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        time.text = currentTime.ToString("0");
        
        if (currentTime == 0f)
        {
            //PlayerController.lifeRemaining -= 1;
        }
    }
}
