using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Jump Destination;
    public float JumpTime;
    public AnimationCurve SizeScaleCurve;
    public Coroutine currentJump = null;

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Bird Player" && Destination.currentJump == null)
            currentJump = StartCoroutine(JumpRoutine(other.gameObject));   
    }

    private IEnumerator JumpRoutine(GameObject toMove)
    {
        Vector3 startPosition = toMove.transform.position;
        Vector3 startScale = toMove.transform.localScale;
        toMove.GetComponent<PlayerMovementController>().enabled = false;

        float time = 0;
        while(time < JumpTime)
        {
            time += Time.deltaTime;
            toMove.transform.position = Vector3.Lerp(startPosition, Destination.transform.position, time/JumpTime);

            float newSize = SizeScaleCurve.Evaluate(time/JumpTime);
            toMove.transform.localScale = new Vector3(newSize, newSize, newSize);
            yield return null;
        }

        toMove.transform.localScale = startScale;

        toMove.GetComponent<PlayerMovementController>().enabled = true;

        currentJump = null;
    }
}
