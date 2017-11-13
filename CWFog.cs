using UnityEngine;
using System.Collections;

public class CWFog : MonoBehaviour {

	Transform player;
	public float fadeDistanceStart = 10;
	public float fogInvisDistance = 1.0F;
	public float maxScaleMod = 0.2F;
	public float fogRotationSpeed = 10.0F;
	 
	private float myRotation = 0.0F;
	private float actualDistance = 0;
	public float wait = 7.0F;
	
	// Use this for initialization
void Start () 
{
	Invoke("DelayedStart",wait);
}


void DelayedStart ()
{
		
		if (Camera.main.gameObject!=null) {
			startNow();
		} else {
			Invoke("startNow",wait);
		}
	}
	
	void startNow () 
	{
		myRotation = Random.Range(0,355);
		float myScale = transform.localScale.x + Random.Range(0,transform.localScale.x * maxScaleMod);
		transform.localScale = new Vector3(myScale,myScale,myScale);
		float rotationFlipper = Random.Range(0.0F,1.0F);

		if (rotationFlipper > .5F) {
			fogRotationSpeed = -fogRotationSpeed;
		}
		
		GameObject go = Camera.main.gameObject;
		player = go.transform;
		int which = Random.Range(0,4);
		Vector2 whichV = new Vector2(0F,0F);
		if (which==1) {
			whichV = new Vector2 (0F, .25F);
		} else if (which==2) {
			whichV = new Vector2 (0F, .5F);
		} else if (which==3) {
			whichV = new Vector2 (0F, .75F);
		}			
		GetComponent<Renderer>().material.mainTextureOffset = whichV;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (player==null) return;
		
		if (GetComponent<Renderer>().isVisible==false) {
			return;
		}
		
		transform.LookAt(player);
		transform.Rotate(0,0,myRotation);
		actualDistance = Vector3.Distance(player.position,transform.position);
			//print("distance is " + actualDistance);
		// Color c = GetComponent<Renderer>().material.GetColor("_Color");
		if (actualDistance<fadeDistanceStart) {
			myRotation += (fogRotationSpeed * Time.deltaTime);
			// if (c!=null) {
			// 	c.a = (Mathf.Max(0,actualDistance-fogInvisDistance)/fadeDistanceStart);
			// 	GetComponent<Renderer>().material.color = c;
			// }
		} else {
			// if (c!=null) {
			// 	c.a = 1.0F;
			// 	GetComponent<Renderer>().material.color = c;
			// }
		}
	}
}
