using UnityEngine;
using System.Collections;

public class CombatText: MonoBehaviour
{

    private float speed;
    private Vector3 direction;
    private float fadeTime;
    public TextMesh textMesh;
    public AnimationClip critAnim;

    private bool crit;

    // Update is called once per frame
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Update()
    {
        if(!crit)
        {
            float translation = speed * Time.deltaTime;

            transform.Translate(direction * translation);
        }

       


    }

    public void Initialize(float speed, Vector3 direction, float fadeTime, bool crit)
    {
        this.speed = speed;
        this.direction = direction;
        this.fadeTime = fadeTime;
        this.crit = crit;

        if(crit)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            StartCoroutine(Critical());
        }
        else
        {
            StartCoroutine(Fadeout());
        }

        
    }

    private IEnumerator Critical()
    {
        
        crit = false;
        StartCoroutine(Fadeout());
        // yield return new WaitForSeconds(critAnim.length);
        yield return null;
    }

    private IEnumerator Fadeout()
    {
        float startAplha = textMesh.font.material.color.a;
        
       

        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while(progress < 1.0f)
        {
            Color tmpColor = textMesh.font.material.color;

            textMesh.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAplha, 0, progress));

            progress += rate * Time.deltaTime;

            yield return null;

        }
        Destroy(gameObject);
    }

}
