using UnityEngine;
using System.Collections;

public  class CamController : MonoBehaviour
{

	// si despues quieren que la camara haga el movimiento al estado inicial despues de unos segundos habrá que incorporarlo acá y camcontrollerambito solo tendrá el raycast si es que sigo esa líneas
	protected Camera cam;
	protected FloatInterpolation camSizeInterpolation;
	protected Vector3InterPolation camMoveInterpolation;
	[SerializeField]
	protected float timeToTargets=1; // distancia al objetivo partido por la velocidad
	//protected float distanceToTarget;
	//[SerializeField]
	//protected float speedToTarget=17f;


	protected bool StartErp=false;



	protected static CamController _instance;
	public static CamController Instance
	{
		get
		{
			if (!_instance)
			{
				_instance = (CamController) GameObject.FindObjectOfType(typeof(CamController));
				if (!_instance)
				{
					GameObject container = new GameObject("CamController");

					_instance = container.AddComponent(typeof(CamController)) as CamController;
				}
			}

			return _instance;
		}
	}

	protected void Start ()
	{
		// cual es el estado inicial de la camara? esta un un evento o se ve todo el mapa. Cuando se ve todo el mapa?¿
		cam = GetComponent<Camera> ();
		camSizeInterpolation = new FloatInterpolation ();
		camMoveInterpolation = new Vector3InterPolation ();
		camSizeInterpolation.SwitchLerp (FloatInterpolation.LerpMode.Sinerp);
		camMoveInterpolation.SwitchLerp (Vector3InterPolation.LerpMode.Sinerp);
	
	}
	
	// Update is called once per frame
	protected void Update () 
	{
		if (StartErp) {
			if (camMoveInterpolation.CurrentLerpTime != camMoveInterpolation.LerpTime) 
			{
				camMoveInterpolation.UpdateLerp ();
				camSizeInterpolation.UpdateLerp ();
				cam.orthographicSize = camSizeInterpolation.InterpolatedValue;
				cam.transform.position = camMoveInterpolation.InterpolatedValue;
			} else {
				StartErp = false;
				//currentTimeToDefault = timeToDefault;
			}
		} //else de camcontrollerambito

	
	}

	public void GoToPosition(Vector2 position,float zoom)
	{
		//if (!onTransition) 
		//{
		Vector2 camPos=cam.transform.position;
		if (position != camPos) {
			
			//	SetTimeToTarget (position);
			camMoveInterpolation.StarPos = new Vector3 (cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
			camMoveInterpolation.EndPos = new Vector3 (position.x, position.y, cam.transform.position.z);
			camMoveInterpolation.LerpTime = timeToTargets;
			camSizeInterpolation.StarPos = cam.orthographicSize;
			camSizeInterpolation.EndPos = zoom;
			camSizeInterpolation.LerpTime = timeToTargets;
			//	if(camMoveInterpolation.CurrentLerpTime == camMoveInterpolation.LerpTime) {
			camSizeInterpolation.ResetartTime ();
			camMoveInterpolation.ResetartTime ();

			StartErp = true;

		} else 
		{
			OnSamePosition ();
		}
		//	StartCoroutine (Move ());
		//}
	}
	protected virtual void OnSamePosition()
	{
		
	}

	/*protected  void SetTimeToTarget(Vector2 target)
	{
		distanceToTarget = Vector2.Distance ( cam.transform.position, target);

		timeToTargets = distanceToTarget / speedToTarget  ;
		Debug.Log ("time to targets: "+timeToTargets);
		Debug.Log ("distance to target: "+distanceToTarget);
	}*/


}
