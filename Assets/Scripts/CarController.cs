using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    public Camera camTwo2;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle; //araban�n sa�a sola d�nmesini kontrol eden a�� de�i�keni 
    private float currentbreakForce; //anl�k fren kuvveti 
    private bool isBreaking; // fren yap�yormu yapm�yormu 
    private bool cameraTwo; // 2.Kamera A��ld�s

    [SerializeField] private float motorForce; // motor kuvveti 
    [SerializeField] private float breakForce; //Fren Kuvveti 
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;  //Tekerlerin colliderlar�
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private WheelCollider steelCollider; // direksyonun collidar� 

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;    //Tekerlerin transformlar�
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Transform steelTransform; // direksyonun transformu 


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        //frontLeftWheelCollider.motorTorque=0.17f*motorForce

    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        cameraTwo = Input.GetKey(KeyCode.F);


    }
    private void HandleMotor()
    {
       
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking  ? breakForce : 0f; // "?" ife benzer bir ko�uldur ":" ise de�er at�yoruz ko�ul sa�lan�yorsa 
        ApplyBreaking();
    }
    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }



    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);

    }
    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }









}           
