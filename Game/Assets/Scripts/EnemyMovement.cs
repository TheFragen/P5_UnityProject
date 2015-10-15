using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Vector3 pointA;
	public Vector3 pointB;
	
	IEnumerator Start()
	{
		pointA = transform.position;
		pointB.y = pointA.y;

		while (true) {
			yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
			yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
		}
	}
	
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		float i= 0.0f;
		float rate= 1.0f/time;
		transform.LookAt(endPos);
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null; 
		}
	}
	
}
