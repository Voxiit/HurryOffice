using UnityEngine;

public class CharacterControllerCollider : MonoBehaviour
{
    //Attributes
    [SerializeField]
    private float _pushPower = 2.0f;
    

    //This will be called when the character controller will hit an object
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitBody = hit.collider.attachedRigidbody;

        //We hit nothing
        if (hitBody == null || hitBody.isKinematic)
        {
            return;
        }

        //We push in front of us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        //Create push vector
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        
        //Apply it
        hitBody.velocity = pushDir * _pushPower;

    }
}
