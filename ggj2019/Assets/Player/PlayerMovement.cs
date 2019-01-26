using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float maxSpeed = 5f;
    [SerializeField]
    protected float accelFactor = 25f;
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

        cameraEuler.x = Mathf.Clamp(cameraEuler.x - sensitivityFactor * Input.GetAxisRaw("Mouse Y"), -80, 80);
        cameraEuler.y += sensitivityFactor * Input.GetAxisRaw("Mouse X");
        cameraHolder.localEulerAngles = cameraEuler;
        Vector3 input = Input.GetAxisRaw("Horizontal") * cameraHolder.right + Input.GetAxisRaw("Vertical") * Vector3.ProjectOnPlane(cameraHolder.forward, Vector3.up).normalized;
        Vector2 floorInput = maxSpeed * new Vector2(input.x, input.z);
        Vector2 floorVelocity = Vector2.MoveTowards(new Vector2(rigid.velocity.x, rigid.velocity.z), floorInput, accelFactor * Time.deltaTime);
        rigid.velocity = new Vector3(floorVelocity.x, rigid.velocity.y, floorVelocity.y);
    }
}
