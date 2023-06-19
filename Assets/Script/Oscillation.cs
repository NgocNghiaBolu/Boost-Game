using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 StartMoveOscilltion;
    [SerializeField] Vector3 MovermentVector;
    [SerializeField] [Range(0,1)] float MoverMentFactor;//pham vi di chuyen
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartMoveOscilltion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }//neu ve 0 thi dung tai noi do luon
        float cycles = Time.time * period;//lien tuc di len theo time
        const float tau = Mathf.PI * 2;//gia tri cua tau la 3,14 *2

        float rawSinWare = Mathf.Sin(cycles * tau);//chay tu -1 den 1

        MoverMentFactor = (rawSinWare + 1f) / 2f;//luon dc tinh toan lai tu -1 den 1

        Vector3 offset = MovermentVector * MoverMentFactor;
        transform.position = StartMoveOscilltion + offset;
    }
}
