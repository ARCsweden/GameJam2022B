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
    public enum TongueState{
        Launching,
        Retracting,
        Cooldown,
        Ready
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
    public float maxTongueLength = 1;
    public float launchTime = 1;
    public float tongueCooldown;
    public Collider2D tongueCol;
    public GameObject pickUp;
    public Transform pickUpSorce;
    // Update is called once per frame
    void Update()
    {
        //Calculates the angle to the mouse
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookAt = mouseScreenPosition;

        float angleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
        float angleRadTongue = Mathf.Atan2(lookAt.y - tongueSource.position.y, lookAt.x - this.transform.position.x);

        float angleDeg = (180 / Mathf.PI) * angleRad;
        //Debug.Log(angleDeg);

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
private float tongueLength = 0;
public TongueState tongueState = TongueState.Ready;
private float tongueCooldownLeft;     
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

        if(Input.GetButton("Fire1") && tongueState == TongueState.Ready){
            tongueState = TongueState.Launching;
        }
        if(tongueState == TongueState.Launching && tongueLength == maxTongueLength){
            tongueState = TongueState.Retracting;
        }
        if(tongueState == TongueState.Retracting && tongueLength == 0){
            tongueState = TongueState.Cooldown;
            tongueCooldownLeft = tongueCooldown;
            if(pickUp != null){
                Destroy(pickUp);
            }
        }
        if(tongueState == TongueState.Cooldown){
            tongueCooldownLeft -= Time.deltaTime;
            if(tongueCooldownLeft <= 0){
                tongueState = TongueState.Ready;
            }
        }
        LaunchTongue(tongueState == TongueState.Launching,maxTongueLength);
        if(tongueState == TongueState.Launching || tongueState == TongueState.Retracting){
            for (int i = 0; i < frogToungeVertices.Length; i++) {
                float toungeMagnitude = new Vector2(frogToungeVertices[i].x,frogToungeVertices[i].y).magnitude;
                toungePositions[i] = new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad),0) * toungeMagnitude * tongueLength;
            }
            pickUpSorce.position = toungePositions[frogToungeVertices.Length-1];
            frogTongue.SetPositions(toungePositions);
            animator.SetFloat("MouthOpen",1f);
            if(tongueState == TongueState.Launching){
                tongueCol.offset = new Vector2(toungePositions[frogTongue.positionCount-1].x,toungePositions[frogTongue.positionCount-1].y);
            }else{
                tongueCol.offset = Vector2.zero;
            }
            if(pickUp != null){
                pickUp.transform.SetParent(pickUpSorce,true);
            }

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
