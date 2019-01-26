using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float maxSpeed = 5f;
    [SerializeField]
    protected float accelFactor = 250f;
    [SerializeField]
    protected float sensitivityFactor = 4f;
    private Rigidbody rigid;
    private Transform cameraHolder;
    public Vector3 cameraEuler = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        cameraHolder = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(sensitivityFactor * Input.GetAxisRaw("Mouse X"), Vector3.up));
        cameraEuler.x = Mathf.Clamp(cameraEuler.x - sensitivityFactor * Input.GetAxisRaw("Mouse Y"), -80, 80);
        cameraHolder.localEulerAngles = cameraEuler;
        Vector3 input = Input.GetAxisRaw("Horizontal") * cameraHolder.right + Input.GetAxisRaw("Vertical") * Vector3.ProjectOnPlane(cameraHolder.forward, Vector3.up).normalized;
        Vector2 floorInput = maxSpeed * new Vector2(input.x, input.z);
        Vector3 targetVelocity = new Vector3(floorInput.x, rigid.velocity.y, floorInput.y);
        rigid.AddForce(accelFactor * Time.deltaTime * (targetVelocity - rigid.velocity), ForceMode.Acceleration);
    }
}
