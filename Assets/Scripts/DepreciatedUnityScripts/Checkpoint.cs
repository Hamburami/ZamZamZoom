using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startTime;
    private float lastTime;

    public TMP_Text timeText;

    void Start() {
        startTime = -1;
        lastTime = 0;
    }

    // Update is called once per frame
    void Update() {
        timeText.text = lastTime.ToString();
    }


    void OnTriggerEnter(Collider collision) {
        //Debug.Log("Start: " + startTime + "  Last: " + lastTime + "Time: " + Time.time);
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Checkpoint trigger entered");
            if (startTime > 0) {
                lastTime = Time.time - startTime;
            }
            startTime = Time.time;
        }
    }
}