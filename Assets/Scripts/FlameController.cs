using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent( typeof( ParticleSystem ) ) ]
public class FlameController : MonoBehaviour
{
    private ParticleSystem partSys;

    void Start()
    {
        partSys = gameObject.GetComponent< ParticleSystem >();
        partSys.Stop();
    }

    public void ToggleFlame( Transform flameDestination, bool flaming )
    {
        if( flaming )
        {
            transform.LookAt( flameDestination );
            partSys.Play();
        }
        else
        {
            partSys.Stop();
        }
    }
}
