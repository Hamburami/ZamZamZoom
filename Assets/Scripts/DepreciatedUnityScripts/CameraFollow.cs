using UnityEngine;

public class CameraFollow : MonoBehaviour{
    public Transform target;
    public float transition = 0.5f;
    public bool trackRotation = true;
    public float rotTransition = 100f;

    private void FixedUpdate() {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, transition * Time.fixedDeltaTime);
        if (trackRotation) {
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.rotation, rotTransition * Time.fixedDeltaTime);
        }
   }

}
