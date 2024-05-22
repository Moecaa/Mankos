using UnityEngine;
using easyInputs;
using GorillaLocomotion;
/*Copyright © [ThatOneGorilla]

Permission is hereby granted, to any person obtaining a copy of this software and associated documentation files RopeSwinger, to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
public class RopeSwinger : MonoBehaviour
{
    [Header("DO NOT GIVE OUT")]
    public Transform Player;
    public RopeSwinger OtherHand;
    public EasyHand Hand = EasyHand.LeftHand;
    [Space(20f)]
    public AudioSource AudioSource;
    public AudioClip LetGoAudioClip;
    public AudioClip GrabAudioClip;
    [Space(5f)]
    public LayerMask ClimbableLayer;
    [Space(20f)]
    public float GrabableRange = 0.056f;
    public float Strength = 1.1f;
    [Space(10f)]
    [Header("Debug")]
    public bool DebugActive;

    // private
    private Collider[] NearbyGrabbableObjects;
    private Transform AttachmentPoint;
    private Vector3 Velo;
    private bool Grabbing;
    private bool Climbing;
    private Rigidbody RB;
    private Rigidbody SwingingRigid;
    private GorillaLocomotion.Player LocalPlayer;
    private void Start()
    {
        LocalPlayer = Player.GetComponent<GorillaLocomotion.Player>();
        RB = Player.GetComponent<Rigidbody>();
        Climbing = false;
    }
    private void Update()
    {
        if (Climbing && SwingingRigid != null)
        {
            SwingingRigid.maxAngularVelocity = 12f;
            SwingingRigid.velocity = Vector3.ClampMagnitude(SwingingRigid.velocity, 7);
        }
        Grabbing = EasyInputs.GetGripButtonDown(Hand);

        NearbyGrabbableObjects = Physics.OverlapSphere(transform.position, GrabableRange, ClimbableLayer);
    }
    private void FixedUpdate()
    {
        if ((DebugActive || Grabbing) && (Climbing || NearbyGrabbableObjects.Length > 0))
        {
            if (!Climbing)
            {
                AudioSource.clip = GrabAudioClip;
                AudioSource.Play();

                if (AttachmentPoint = null)
                {
                    Destroy(AttachmentPoint);
                }
                AttachmentPoint = new GameObject("AttachmentPoint").transform;
                AttachmentPoint.position = transform.position;
                AttachmentPoint.parent = NearbyGrabbableObjects[0].transform;

                StartCoroutine(EasyInputs.Vibration(Hand, 0.3f, 0.2f));

                SwingingRigid = NearbyGrabbableObjects[0].GetComponent<Rigidbody>();
                if (SwingingRigid != null)
                {
                    Velo = LocalPlayer.currentVelocity * 100;

                    SwingingRigid.AddForce(Velo.magnitude * Strength * Velo.normalized, ForceMode.Acceleration);
                }

                Debug.Log("Player climbed at " + NearbyGrabbableObjects[0].name + ", and setup!");

                Climbing = true;
            }

            Velo = LocalPlayer.currentVelocity * 100;

            StartCoroutine(EasyInputs.Vibration(Hand, Velo.magnitude / 5f, 0.2f));

            RB.velocity = (AttachmentPoint.position - transform.position) / Time.fixedDeltaTime;
        }
        else if (Climbing)
        {
            StartCoroutine(EasyInputs.Vibration(Hand, 0.1f, 0.2f));

            if (AttachmentPoint != null)
            {
                Destroy(AttachmentPoint.gameObject);
            }

            SwingingRigid = null;
            AttachmentPoint = null;

            AudioSource.clip = LetGoAudioClip;
            AudioSource.Play();

            Climbing = false;

            Debug.Log("Climbing stopped!");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, GrabableRange);
    }
}