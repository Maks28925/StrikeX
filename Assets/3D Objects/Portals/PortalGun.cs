using UnityEngine;

public class PortalGun : MonoBehaviour
{

    [Header("Other Portals")]
    public Portal Red;
    public Portal Blue;

    [Header("GameObjects")]
    public GameObject target;

    [Header("Variables")]
    public bool hitWall  = false;
    public bool GroundWall = false;

    [Header("LayerMasks")]
    public LayerMask LayerWall;
    public LayerMask PortalGround;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer != 6) return;

                target.transform.position = hit.point;
                target.transform.rotation = Quaternion.LookRotation(hit.normal);
                target.transform.localPosition += target.transform.forward * 0.2f;

                Ray _ray = new Ray(target.transform.position, target.transform.right);
                RaycastHit hitRight;

                Ray _ray1 = new Ray(target.transform.position, -target.transform.right);
                RaycastHit hitLeft;

                Ray _ray2 = new Ray(target.transform.position, -target.transform.up);
                RaycastHit hitDown;


                Ray _ray3 = new Ray(target.transform.position, target.transform.up);
                RaycastHit hitUp;

                Debug.DrawRay(target.transform.position, _ray.direction);
                Debug.DrawRay(target.transform.position, _ray1.direction);


                if (Physics.Raycast(_ray, out hitRight, 1.3f, LayerWall))
                {
                    hitWall = true;
                }


                else if (Physics.Raycast(_ray1, out hitLeft, 1.3f, LayerWall))
                {
                    hitWall = true;
                }

                else if (Physics.Raycast(_ray3, out hitUp, 1.7f, PortalGround))
                {
                    hitWall = true;
                }

                else if (Physics.Raycast(_ray2, out hitDown, 1.7f, PortalGround))
                {
                    if (hitDown.collider.gameObject.layer == 16)
                    {
                        GroundWall = true;
                        CreateTP(hit, GroundWall, hitDown);
                        GroundWall = false;
                        Debug.Log("hitwall: " + hitWall + ", GroundWall: " + GroundWall);
                        hitWall = true;
                    }
                    else { hitWall = true; }
                }

                else { hitWall = false; }
               

                if (hitWall)
                {
                    Debug.Log("return");
                    return;
                }

                else
                {
                    CreateTP(hit, GroundWall, hit);
                }
            }
        }
    }


    public void CreateTP(RaycastHit hit, bool Ground, RaycastHit rayDown)
    {
        if (!Ground)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Red.transform.rotation = Quaternion.LookRotation(hit.normal);
                Red.transform.position = hit.point + Red.transform.forward * 0.6f;
            }
            else
            {
                Blue.transform.rotation = Quaternion.LookRotation(hit.normal);
                Blue.transform.position = hit.point + Blue.transform.forward * 0.6f;
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Red.transform.rotation = Quaternion.LookRotation(hit.normal);
                Red.transform.position = hit.point + Red.transform.forward * 0.6f;
                Red.transform.position = new Vector3(Red.transform.position.x, rayDown.collider.gameObject.transform.position.y +  2f, Red.transform.position.z);
                Debug.Log(rayDown.collider.gameObject.transform.position.y);
            }

            else
            {
                Blue.transform.rotation = Quaternion.LookRotation(hit.normal);
                Blue.transform.position = hit.point + Blue.transform.forward * 0.6f;
                Blue.transform.position = new Vector3(Blue.transform.position.x, rayDown.collider.gameObject.transform.position.y + 2f, Blue.transform.position.z);
            }
        }
        hitWall = false;
    }
}
