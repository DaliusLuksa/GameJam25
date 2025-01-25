using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitNewOne());
    }
    private IEnumerator WaitNewOne()
    {
         yield return new WaitForSeconds(4f);

         this.gameObject.SetActive(false);
    }
}
