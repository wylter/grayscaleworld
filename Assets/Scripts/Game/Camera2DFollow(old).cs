using UnityEngine;

//namespace UnitySampleAssets._2D
//{

public class Camera2DFollow : MonoBehaviour
{
    [Header("Target To Follow")]
    [SerializeField]
    private Transform target;
        
    [Space]
    [Header("Movement Variables")]
    [SerializeField]
    private float damping = 1;
    [SerializeField]
    private float lookAheadFactor = 3;
    [SerializeField]
    private float lookAheadReturnSpeed = 0.5f;
    [SerializeField]
    private float lookAheadMoveThreshold = 0.1f;

    //Restrizione della telecamera
	private float downRestriction = -10;
    private float upRestriction = 10;
    private float leftRestriction = -10;
    private float rightRestriction = 10;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
	
	float nextTimeToSearch;

    // Use this for initialization
    private void Start()
    {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    public void setRestrictions(float downRestriction, float upRestriction, float leftRestriction, float rightRestriction) {
        Vector2 extents = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)) - transform.position;

        if ((rightRestriction - leftRestriction) < extents.x || (upRestriction - downRestriction) < extents.y) {
            Debug.LogError("Those are not good positions");
        }

        this.downRestriction = downRestriction + extents.y;
        this.upRestriction = upRestriction - extents.y;
        this.leftRestriction = leftRestriction + extents.x;
        this.rightRestriction = rightRestriction - extents.x;
    }

    // Update is called once per frame
    private void Update()
    {
	if (target == null) {
		FindPlayer ();
	}

	// only update lookahead pos if accelerating or changed direction
	float xMoveDelta = (target.position - lastTargetPosition).x;

	bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

	if (updateLookAheadTarget) {
		lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
	} else {
		lookAheadPos = Vector3.MoveTowards (lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
	}

	Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
	Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref currentVelocity, damping);

	newPos = new Vector3 (Mathf.Clamp(newPos.x, leftRestriction, rightRestriction), Mathf.Clamp (newPos.y, downRestriction, upRestriction), newPos.z);

	transform.position = newPos;

	lastTargetPosition = target.position;
		
    }
	
	void FindPlayer (){
			if (nextTimeToSearch <= Time.time) {
				target = GameObject.FindGameObjectWithTag ("Player").transform;
				nextTimeToSearch = Time.time + 0.5f;
			}
	}
    }
//}