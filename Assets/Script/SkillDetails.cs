using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDetails : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public float rotationDuration = 2.0f;

    private Quaternion startRotation;
    private Quaternion endRotation;
   private void OnEnable() {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 180, 0));
        StartCoroutine(Rotate());
   }
    IEnumerator Rotate()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / rotationDuration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
        t = 0.0f;
        StartCoroutine(Rotate());
    }
    private void OnDisable() {
        transform.rotation=startRotation;
    }

}
