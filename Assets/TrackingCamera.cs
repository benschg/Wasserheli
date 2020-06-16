using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    float distance;

    Vector3 playerPrevPos, playerMoveDir;

    Vector3 camOffset;

    float mouseVerticalMovement = 0.0f;
    float verticalOffset = 5.0f;

    // Use this for initialization
    void Start () {
        camOffset = new Vector3(20, 5, 0);
        offset = transform.position - player.transform.position;

        distance = offset.magnitude;
        playerPrevPos = player.transform.position;
    }

    void LateUpdate () {
        this.GetMouseMovements();

        verticalOffset += mouseVerticalMovement * .8f;
        if (verticalOffset > 10) { verticalOffset = 10; }
        else if (verticalOffset < -10) { verticalOffset = -10; }

        camOffset.y = this.verticalOffset;

        Vector3 playerDir = player.transform.forward;

        Vector3 offset = player.transform.rotation * camOffset;

        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + offset, 2.0f * Time.deltaTime);
        // playerMoveDir = player.transform.position - playerPrevPos;
        // if (playerMoveDir != Vector3.zero)
        // {
        //     playerMoveDir = playerMoveDir.normalized;
        //     transform.position = player.transform.position - playerMoveDir * distance;

        //     transform.position += new Vector3(0, transform.position.y + 5.0f, 0); // required height

             transform.LookAt(player.transform.position);
        // }
    }

    void GetMouseMovements()
    {
        this.mouseVerticalMovement = Input.GetAxis("Mouse Y");

    }
}
