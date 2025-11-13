using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Found Object")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Found Object", message: "[Agent] found [object] with [tag]", category: "Events", id: "9b4c3b2760310cdc364973829190b19f")]
public sealed partial class FoundObject : EventChannel<GameObject, GameObject, string> { }

