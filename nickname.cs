using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class nickname : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nickname_id;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nickname_id.GetComponent<TMP_Text>().text = userInfo.getInstance().nickname;
    }
}
