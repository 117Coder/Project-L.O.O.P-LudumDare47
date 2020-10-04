using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollowscript : MonoBehaviour
{
    public Transform player;
    public float speed;

    private void LateUpdate()
    {
        //Vector3 newpos = Vector3.Lerp(player.position.x, player.position.y, this.transform.position.z);
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);

    }
}
