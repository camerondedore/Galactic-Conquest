using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSunWisps : MonoBehaviour
{

	float speed = 2;



	void Update()
    {
		var rot = transform.localEulerAngles;
		rot.z += speed * Time.deltaTime;

		transform.localRotation = Quaternion.Euler(rot);
    }
}
