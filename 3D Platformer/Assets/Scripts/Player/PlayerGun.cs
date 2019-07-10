using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform barrelEnd;

    float bulletSpeed = 1000f;
    private float mouseX;
    private float mouseY;
    private float finalInputX;
    private float finalInputZ;
    private float smoothX;
    private float smoothY;
    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotationY += finalInputX * 150 * Time.deltaTime;
        rotationX += finalInputZ * 150 * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -80, 80);
        Quaternion localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
        transform.rotation = localRotation;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation) as GameObject;
            Rigidbody bulletInstanceRB = bulletInstance.GetComponent<Rigidbody>();
            bulletInstanceRB.AddForce(bulletInstanceRB.transform.forward * bulletSpeed);
            Destroy(bulletInstance, 1f);
        }
    }
}
