using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Carrera")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Carrera", message: "Carrera terminada", category: "Events", id: "e0b9d3a16d84e207da116e3bd46cfa7d")]
public sealed partial class Carrera : EventChannel { }

