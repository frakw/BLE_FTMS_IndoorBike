using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTMS_UI : MonoBehaviour
{
    // Start is called before the first frame update
    private bool connected = false;
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

    }

    public void connect() {
        if (device_name.Length > 0 && service_id.Length > 0 && read_characteristic.Length > 0 && write_characteristic.Length > 0)
        {
            StartCoroutine(connector.connect(device_name, service_id, read_characteristic, write_characteristic));
            connected = true;
        }
    }

    public void write_resistance(float val) {
        if (connected)
        {
            connector.write_resistance(val);
            resistance_show.text = "Resistance: " + Mathf.FloorToInt(val).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            connector.Update();
            info.text = connector.output;
        }
    }
    private void OnApplicationQuit()
    {
        connector.quit();
    }

    public void change_device_name(string _device_name) {
        device_name = _device_name;
    }
    public void change_service_id(string _service_id)
    {
        service_id = _service_id;
    }
    public void change_read_characteristic(string _read_characteristic)
    {
        read_characteristic = _read_characteristic;
    }
    public void change_write_characteristic(string _write_characteristic)
    {
        write_characteristic = _write_characteristic;
    }
}
