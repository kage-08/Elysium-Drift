using UnityEngine;
using System.Collections;

public class PlayerMagic : MonoBehaviour
{
    public bool IsCasting { get; private set; }

    [SerializeField] private float castTime = 1.5f;
    [SerializeField] private float mpCost = 10f;

    private PlayerStatus status;
    private Coroutine castCoroutine;

    void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            HandleMagicKey();
    }

    private void HandleMagicKey()
    {
        if (IsCasting)
        {
            CancelCast();
        }
        else
        {
            castCoroutine = StartCoroutine(CastRoutine());
        }
    }

    private IEnumerator CastRoutine()
    {
        if (!status.ConsumeMP(mpCost)) yield break;

        IsCasting = true;
        yield return new WaitForSeconds(castTime);

        Debug.Log("Magic Fired!");
        IsCasting = false;
    }

    private void CancelCast()
    {
        if (castCoroutine != null)
            StopCoroutine(castCoroutine);

        IsCasting = false;
    }
}
