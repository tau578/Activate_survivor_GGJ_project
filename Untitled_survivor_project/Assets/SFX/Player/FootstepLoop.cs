using UnityEngine;

public class FootstepLoop : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip walkLoopClip;
    [SerializeField] private AudioClip runLoopClip; // optional, if null -> walk used
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;

    [SerializeField] private float inputDeadzone = 0.1f;

    void Update()
    {
        if (source == null) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool movingInput = (new Vector2(h, v)).sqrMagnitude > (inputDeadzone * inputDeadzone);

        if (!movingInput)
        {
            if (source.isPlaying) source.Stop();
            return;
        }

        AudioClip clipToUse = (Input.GetKey(runKey) && runLoopClip != null) ? runLoopClip : walkLoopClip;
        if (clipToUse == null)
        {
            if (source.isPlaying) source.Stop();
            return;
        }

        if (source.isPlaying && source.clip == clipToUse) return;

        source.Stop();
        source.clip = clipToUse;
        source.loop = true;
        source.Play();
    }

    void OnDisable()
    {
        if (source != null && source.isPlaying) source.Stop();
    }
}