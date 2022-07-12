using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTMS_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool connect = true;
    public FTMS_IndoorBike connector;
    public Text info;
    public Text resistance_show;

    public string device_name = "APXPRO 46080";
    public string service_id = "{00001826-0000-1000-8000-00805f9b34fb}";
    public string read_characteristic = "{00002ad2-0000-1000-8000-00805f9b34fb}";
    public string write_characteristic= "{00002ad9-0000-1000-8000-00805f9b34fb}";
    void Start()
    {
        connector = new FTMS_IndoorBike(this);
        if (connect) {
            StartCoroutine(connector.connect(device_name, service_id, read_characteristic, write_characteristic));
        }
    }

    public void write_resistance(float val) {
        connector.write_resistance(val);
        resistance_show.text = "Resistance: " + Mathf.FloorToInt(val).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (connect)
        {
            connector.Update();
            info.text = connector.output;
        }
    }
    private void OnApplicationQuit()
    {
        connector.quit();
    }
}
