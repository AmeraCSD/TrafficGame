using UnityEngine;

/// <summary>
/// Placing this script on the game object will make that game object pan with mouse movement.
/// </summary>

[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithAcceleration : IgnoreTimeScale
{
	public Vector2 degrees = new Vector2(5f, 3f);
	public float range = 1f;

	Transform mTrans;
	Quaternion mStart;
	Vector2 mRot = Vector2.zero;

	void Start ()
	{
		mTrans = transform;
		mStart = mTrans.localRotation;
	}

	void Update ()
	{
		Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.x;
        dir.z = Input.acceleration.y;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
		
		//dir *= Time.deltaTime;
		//transform.Translate(dir * 5f);
		Debug.Log("dir === " + dir);
		
		float delta = UpdateRealTimeDelta();
		
	//	Vector3 pos = Input.acceleration;
		
		//dir = Input.mousePosition;
		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		
		if (range < 0.1f) range = 0.1f;
		float x = Mathf.Clamp((dir.x - halfWidth) / halfWidth / range, -1f, 1f);
		float y = Mathf.Clamp((dir.y - halfHeight) / halfHeight / range, -1f, 1f);
		
		
		mRot = Vector2.Lerp(mRot, new Vector2(dir.x, dir.y), delta * 5f);

		mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * degrees.y * 1.3f, mRot.x * degrees.x * 1.3f, 0f);
		
		
	//	transform.Rotate(dir * 3f)	;
		
	}
}