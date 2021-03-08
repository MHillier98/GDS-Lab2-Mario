using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public bool Movement;
    public Vector2 StartPos;
    public Vector2 EndPos;

    // Start is called before the first frame update
    void Start()
    {
        Movement = false;
        StartPos = gameObject.transform.position;
        //Vector2 EndPos = transform.position;
        EndPos = new Vector2(StartPos.x, StartPos.y + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {


        if (Movement)
        {
            //TimeStarting = Time.time;

            /*            Vector2 StartPos = transform.position;
                        //Vector2 EndPos = new Vector2(StartPos.x, StartPos.y + 0.5f);
                        Vector2 EndPos = transform.position;
                        EndPos.y = StartPos.y + 0.5f;
                        //collider.gameObject.GetComponent<Animator>().SetBool("SmallMario", true);
                        //StartCoroutine(Lerping(StartPos, EndPos));

                        transform.position = BoxMoving(StartPos, EndPos, TimeStarting, LerpingTime);*/



            StartCoroutine(Lerping(StartPos, EndPos, 0.5f));
        }
    }

    IEnumerator Lerping(Vector2 Start, Vector2 End, float Duration)
    {
        Debug.Log("Should be lerping");
        float time = 0;
        while (time < Duration)
        {
            transform.position = Vector2.Lerp(Start, End, time / Duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = End;
        Movement = false;
    }

/*    public Vector3 BoxMoving(Vector3 Start, Vector3 End, float TimeStarted, float LerpTime = 1)
    {
        float TimeSinceStarting = Time.time - TimeStarted;

        float PercentageComplete = TimeSinceStarting / LerpTime;

        var EndResult = Vector3.Lerp(Start, End, PercentageComplete);

        return EndResult;
    }*/
}
