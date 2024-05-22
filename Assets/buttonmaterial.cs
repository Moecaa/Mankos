using easyInputs;
using UnityEngine;

public class buttonmaterial : MonoBehaviour
{
    private Color ButtonFirstColor;
    public Color ButtonPressedColor = Color.red;
    public AudioSource click;

    private Renderer buttonRenderer;

    void Start()
    {
        buttonRenderer = this.gameObject.GetComponent<Renderer>();
        ButtonFirstColor = buttonRenderer.material.color;
        click = GameObject.FindWithTag("ClickSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        click.GetComponent<Transform>().position = this.transform.position;
        if (other.GetComponent<HandColliders>().isLeftHand == true)
        {
            click.Play();
            buttonRenderer.material.color = ButtonPressedColor;
            StartCoroutine(EasyInputs.Vibration(EasyHand.LeftHand, 0.15f, 0.15f));
        }
        if (other.GetComponent<HandColliders>().isRightHand == true)
        {
            click.Play();
            buttonRenderer.material.color = ButtonPressedColor;
            StartCoroutine(EasyInputs.Vibration(EasyHand.RightHand, 0.15f, 0.15f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            buttonRenderer.material.color = ButtonFirstColor;
        }
    }
}
