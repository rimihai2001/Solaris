using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject fstCelestial in celestials)
        {
            foreach (GameObject sndCelestial in celestials)
            {
                if (!fstCelestial.Equals(sndCelestial))
                {
                    float fstMass = fstCelestial.GetComponent<Rigidbody>().mass;
                    float sndMass = sndCelestial.GetComponent<Rigidbody>().mass;

                    float r = Vector3.Distance(fstCelestial.transform.position, sndCelestial.transform.position);

                    fstCelestial.GetComponent<Rigidbody>().AddForce((sndCelestial.transform.position - fstCelestial.transform.position).normalized * (G * (fstMass * sndMass) / (r * r)));
                }
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject fstCelestial in celestials)
        {
            foreach (GameObject sndCelestial in celestials)
            {
                if (!fstCelestial.Equals(sndCelestial))
                {
                    float sndMass = sndCelestial.GetComponent<Rigidbody>().mass;
                    // adding velocity depending on the surrounding objects
                    float r = Vector3.Distance(fstCelestial.transform.position, sndCelestial.transform.position);
                    fstCelestial.transform.LookAt(sndCelestial.transform);

                    fstCelestial.GetComponent<Rigidbody>().velocity += fstCelestial.transform.right * Mathf.Sqrt((G * sndMass) / r);

                }
            }
        }
    }
}
