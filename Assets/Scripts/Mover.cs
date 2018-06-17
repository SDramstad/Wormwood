using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{    
    public bool isHoldingRigidBody = false;

    public Vector3 holdedRigedScale;

    public float holdedRigedMass = 0;

    public GameObject holdingRigedObject;

    void Update()
    {
        PhysicPickUp();
    }

    public void PhysicPickUp()
    {

        if (Input.GetKeyDown(KeyCode.E) && !isHoldingRigidBody)
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                holdingRigedObject = hit.collider.gameObject;

                // Do stuff. Like instantiate an object at hit.point
                if (holdingRigedObject.GetComponent<Renderer>())
                {
                    if (Vector3.Distance(this.gameObject.transform.position, holdingRigedObject.transform.position) < 3f  && holdingRigedObject.GetComponent<Renderer>().isVisible == true  && holdingRigedObject.GetComponent<Rigidbody>() != null && holdingRigedObject.GetComponent<Rigidbody>().mass <= 3)
                    {

                        holdedRigedScale = holdingRigedObject.transform.localScale;

                        holdingRigedObject.transform.parent = this.gameObject.transform;

                        holdedRigedMass = holdingRigedObject.GetComponent<Rigidbody>().mass;

                        Destroy(holdingRigedObject.GetComponent<Rigidbody>());

                        isHoldingRigidBody = true;

                    }
                }
                else
                {
                }
            }

        }
        else if(Input.GetKeyDown(KeyCode.E) && isHoldingRigidBody)
        {
            PhysicThrow();
        }
        else if (Input.GetMouseButton(0)  && isHoldingRigidBody)
        {
            PhysicThrow(10);
        }

    }

    private void PhysicThrow(float force = 0)
    {
        
        holdingRigedObject.transform.parent = null;

        holdingRigedObject.AddComponent<Rigidbody>();

        holdingRigedObject.GetComponent<Rigidbody>().mass = holdedRigedMass;

        holdingRigedObject.transform.localScale = holdedRigedScale;

        if (force != 0)
            holdingRigedObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * force, ForceMode.Impulse);
        
        isHoldingRigidBody = false;

    }
}
