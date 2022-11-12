using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAim : MonoBehaviour
{
    enum Direction{
        Right,
        Left,
        Up,
        Down
    }
    // Start is called before the first frame update
    void Start()
    {
        frogToungeVertices = new Vector3[frogTongue.positionCount];
        frogTongue.GetPositions(frogToungeVertices);
    }

    public GameObject frogGraphics;
    public Animator animator;
    [Range(0,90)]public float upAnimationAngle = 45;
    [Range(0,-90)]public float downAnimationAngle = -45;
    public Transform tongueSource;
    public Vector3 tongueRightPoint;
    public Vector3 tongueTopPoint;
    public Vector3 tongueBottomPoint;
    private Vector3[] frogToungeVertices;
    private float tongueLength = 0;
    public float maxTongueLength = 1;
    public float launchTime = 1;
    // Update is called once per frame
    void Update()
    {
        //Calculates the angle to the mouse
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float angleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float angleRadTongue = Mathf.Atan2(lookAt.y - tongueSource.position.y, lookAt.x - this.transform.position.x);

        float angleDeg = (180 / Mathf.PI) * angleRad;
        Debug.Log(angleDeg);

        Direction dir;
        if(Mathf.Abs(angleDeg) > 90){
            frogGraphics.transform.localScale = new Vector3(-1,1,1);
            dir = Direction.Left;
        }
        else{
            frogGraphics.transform.localScale = new Vector3(1,1,1);
            dir = Direction.Right;
        }
        if(angleDeg > upAnimationAngle && angleDeg < 180-upAnimationAngle){
            //animator.SetInteger("Direction",(int)Direction.Up);
            dir = Direction.Up;
        }else if(angleDeg < downAnimationAngle && angleDeg > -180 - downAnimationAngle){
            //animator.SetInteger("Direction",(int)Direction.Down);
            dir = Direction.Down;
        }else{
            //animator.SetInteger("Direction",(int)Direction.Right);
        }
        animator.SetInteger("Direction",(int)dir);
        animator.SetFloat("AnimTime",Time.time*1.5f);

        FrogTongue(angleRadTongue, dir);
    }
public LineRenderer frogTongue;
private bool launching = false;
private bool retracting = false;       
    void FrogTongue(float angleRad, Direction dir){
        Vector3[] toungePositions = new Vector3[6];
        if(dir == Direction.Right){
            tongueSource.localPosition = tongueRightPoint;
        }
        if(dir == Direction.Left){
            tongueSource.localPosition = new Vector3(-tongueRightPoint.x,tongueRightPoint.y,tongueRightPoint.z);
        }
        if(dir == Direction.Up){
            tongueSource.localPosition = tongueTopPoint;
        }
        if(dir == Direction.Down){
            tongueSource.localPosition = tongueBottomPoint;
        }

        if(Input.GetButton("Fire1") && !launching && !retracting){
            launching = true;
        }
        if(launching && tongueLength == maxTongueLength){
            launching = false;
            retracting = true;
        }
        if(retracting && tongueLength == 0){
            retracting = false;
        }
        LaunchTongue(launching,maxTongueLength);
        if(launching || retracting){
            for (int i = 0; i < frogToungeVertices.Length; i++) {
                float toungeMagnitude = new Vector2(frogToungeVertices[i].x,frogToungeVertices[i].y).magnitude;
                toungePositions[i] = new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad),0) * toungeMagnitude * tongueLength;
            }
            frogTongue.SetPositions(toungePositions);
            animator.SetFloat("MouthOpen",1f);

        }
        else{
            animator.SetFloat("MouthOpen",0f);
            frogTongue.SetPositions(new Vector3[frogTongue.positionCount]);
        }
    }
    void LaunchTongue(bool outwards, float maxLength){
        if(outwards){
            tongueLength = Mathf.Clamp(tongueLength + Time.deltaTime / launchTime*maxLength,0,maxLength);
        }
        else{
            tongueLength = Mathf.Clamp(tongueLength - Time.deltaTime / launchTime*maxLength,0,maxLength);
        }
    }
}
