using DT; 
using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    private Camera m_Camera;
    private MeshRenderer _meshRenderer;   
    public float twinkleFrequency = 1.0f;

    void Awake(){
        m_Camera = Camera.main; //Hard-code to the main camera
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sharedMaterials[0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 1.0f));
        _meshRenderer.sharedMaterials[1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.0f));
    }

    void Start(){
    }

    void Update()
    {
        //Rotate the stars to face the camera
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }

    public void AnimateStar(float duration){
        this.StopAllCoroutines();
        this.StartCoroutine(this.CrossFadeMaterials(twinkleFrequency, duration));
    }

    private IEnumerator CrossFadeMaterials(float frequency, float duration) {
        //Initial states
        float timeElapsed = 0.0f; 
        bool toggle = false; 
        for (float time = 0.0f; time < duration; time += Time.deltaTime) {           
            timeElapsed += Time.deltaTime;
            if (timeElapsed > frequency)
            {
                timeElapsed -= frequency;
                toggle = !toggle;
            }                    
            if (toggle)
            {
                _meshRenderer.sharedMaterials [0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, Easers.Ease(EaseType.QuadOut, 1.0f, 0.0f, timeElapsed, frequency)));
                _meshRenderer.sharedMaterials [1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, Easers.Ease(EaseType.QuadOut, 0.0f, 1.0f, timeElapsed, frequency)));
            } else
            {
                _meshRenderer.sharedMaterials [0].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, Easers.Ease(EaseType.QuadOut, 0.0f, 1.0f, timeElapsed, frequency)));
                _meshRenderer.sharedMaterials [1].SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, Easers.Ease(EaseType.QuadOut, 1.0f, 0.0f, timeElapsed, frequency)));
            }              
            yield return new WaitForEndOfFrame();
        }
    }

}