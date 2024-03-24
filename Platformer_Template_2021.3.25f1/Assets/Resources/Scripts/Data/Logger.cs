using UnityEngine;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <Remarks>
/// 
/// Displays logs in the console sent from any script. <br></br>
/// Attach this script to an empty GameObject in the scene. <br></br>
/// When required to log a message, call the Log method from any script by initialize and .log($message) <br></br>
/// 
/// </Remarks>
public class Logger : MonoBehaviour
{
    [SerializeField] private bool _showLogs;

    public void Log(object message, Object sender)
    {
        if (_showLogs)
            Debug.Log(message, sender);
    }
}
