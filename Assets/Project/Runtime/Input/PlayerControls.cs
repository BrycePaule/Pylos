// GENERATED AUTOMATICALLY FROM 'Assets/Project/Runtime/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""fc5005c9-3ae5-40bb-b454-955151181234"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""42e19a2c-7074-430c-9442-0a3a2c906ee3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""dd14bd38-4905-4dca-a8a4-52ee11b35c83"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""a4bec1a0-93f6-45b0-81de-c615b5ff7ee8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClickHold"",
                    ""type"": ""Button"",
                    ""id"": ""96b14470-056d-4bff-aa4f-0fa2bd822a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""71ccf9b2-72fc-4b2f-8a3f-a41e960bd02d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a6595a08-2d7b-493c-8961-bfe191d7334e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8756ce93-c515-414a-b3e5-56ecaff00b5e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""74f1bd59-ecdb-4fce-9315-f0ab33bb2c9d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1c90014f-dfce-4abd-b8ae-885dafb480a1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""704ff797-1dc8-4a70-b4db-c9ccd5b7eef4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""46b67c81-d75c-41cb-951c-74dc2cf16848"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86cedec6-fad3-43e4-9e43-7edcb57e3278"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClickHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b545ea1-f0a9-4d58-b2ff-afbcd583fa63"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3426eb1-cbeb-4df5-a9bf-976efba51a97"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""d8ee76eb-f4c8-4385-9d20-8a8b7470158a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""4df486f5-1bde-4846-a075-044e7eacf486"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""5ca7f49d-37f2-401d-8e17-ca29955ad669"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""8ab5aab8-0e5c-4954-8da0-5f800cf10295"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f0376c5b-9bba-49e4-8d33-aaa36a56ef34"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e92dee4f-bc23-4250-8a07-c1f94d38177e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dddf256e-9b57-4b0e-8fa3-f58002d892a3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4b9cf2a6-23a6-4bed-96d9-2c511cbac31d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b78f46c0-0387-4cb7-a8ab-d262e7384365"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bc3949c5-28fa-4c06-b9ad-81823819d65b"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c957406a-5933-4cae-9207-31ecf7b71c83"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DevKeys"",
            ""id"": ""38097a87-c968-490c-940c-19ea98a280ff"",
            ""actions"": [
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""97a0c734-6d2e-4dbb-928a-2d4b5f2f4457"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55dd805e-8b36-4491-b2a4-c12200af869a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""HotKeys"",
            ""id"": ""2b9afb4c-699c-4d07-8041-394a4a0ef4cb"",
            ""actions"": [
                {
                    ""name"": ""Save Location Marker"",
                    ""type"": ""Value"",
                    ""id"": ""8413d683-fdb2-4694-b34f-17b6ab97020c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Snap To Location Marker"",
                    ""type"": ""Value"",
                    ""id"": ""ad238385-5d94-4981-ac56-dc2da15e4f25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Scale"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleMenu"",
                    ""type"": ""Button"",
                    ""id"": ""daef6845-65ae-4cb2-a368-8ca003152c5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleBuildMenu"",
                    ""type"": ""Button"",
                    ""id"": ""6c6b6ed6-1132-4f23-9765-ae859c669f68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Marker 1"",
                    ""id"": ""5fc343bf-7861-41c1-a59b-ecff37bf5190"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""588802bc-4e1b-4f97-9377-7703e2843ebf"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Button"",
                    ""id"": ""613a07d7-8ab0-43d3-b0e7-611a2199b8ed"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 2"",
                    ""id"": ""95833baf-4151-4215-8ce0-875e8fcb86c3"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""70ac913a-51e0-47d3-8973-a12fee705ad2"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""635644ae-83c8-4cb9-9621-d97537f807af"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 3"",
                    ""id"": ""a5e7384c-d454-4a91-afe7-cff94c8042f3"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""88d16eed-5349-48bd-8dd9-ddaa61cdbad9"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""3b462b10-bea5-4fca-b105-1ac95cc6e913"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 4"",
                    ""id"": ""5ad83e40-7418-45d2-80e0-057279fa7c0a"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""dfbc8d06-0eed-4249-80de-f7b71a60f9de"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""d36c58ca-de53-4e4c-a2d1-ccd211258442"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 5"",
                    ""id"": ""aea632af-6d98-40ae-9a52-5bd3806573d7"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""393a0a26-5aeb-4fe5-8fe7-2014071fa930"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""ebdef9d3-d252-4bd1-9934-3130137e3eaa"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 6"",
                    ""id"": ""78afa0bb-ccd1-42cb-9c18-784ee468be78"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""85b97cc2-1764-4e5f-a290-a525b46c6e6c"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""e827757d-7420-4c41-9255-0bd8f7bcf411"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 7"",
                    ""id"": ""98450c61-84c5-459c-b453-303712215fc5"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""7d396df1-d45f-40c8-a43e-dcfe786869c9"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""641790d3-ecb1-48f9-a284-617ea6590e97"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 8"",
                    ""id"": ""b2e71c8d-2126-4eb3-83f5-a0208b8990c4"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""42ff4df3-cf69-4903-a02b-cb16918214e5"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""a86e39a8-31e3-4904-970a-3da6d95fb460"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 9"",
                    ""id"": ""87834104-3a78-4a92-844d-9be59f5a56e7"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""ca8facca-029c-4607-a9fa-8d6be2d66528"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""e8d764b8-f7e2-4605-91c7-e9033e1bd842"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 1"",
                    ""id"": ""accdd163-4554-43d8-bc05-e732e9223712"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""08434132-6e44-4ea8-8449-df2f65b9e250"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""94c392b8-6847-4350-8184-c55c33f1550d"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 2"",
                    ""id"": ""7feee1c3-6d49-4cc1-b74c-7543c888b319"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""3b7fbe04-0b0e-46df-9194-3e5b3cdd193d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""176bf4fd-56ab-4b8c-b645-35557c7ab869"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 3"",
                    ""id"": ""56f4df20-d228-49c4-80b0-2361343c2df6"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""f33fc3af-f439-49b1-8fbe-d25f44fa9d40"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""e3607648-9d94-4cae-bb34-cf4c963f6b3c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 4"",
                    ""id"": ""7c448f6e-62f3-4e59-a9fe-8859c6fad99a"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4f1c5888-42bc-42e0-837c-8a5dfe3db0bb"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""2da4abb3-9c70-4675-b71a-a54ca602f7dc"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 5"",
                    ""id"": ""f478f624-87a1-48c8-8cef-b46ffca98177"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c8224dcb-b7aa-4ece-8f9a-1815fe7e143d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""d309e1ac-ba56-4fe2-9748-2be05499aa3f"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 6"",
                    ""id"": ""389f8164-db27-4a38-ac24-9a74ee01ad28"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""3b1844fd-757a-4e6d-b3b5-2e2d9409a5c2"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""43db05d2-080e-4b4a-8558-27ae1f9bd56e"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 7"",
                    ""id"": ""eadd37fb-781c-46bd-8a0e-da3ca5d6fc3e"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""80ddc1a2-ea36-439d-b47c-e323c983fc3e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""7ee29caf-f467-47e5-8205-2d0612f575c5"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 8"",
                    ""id"": ""0cc71f5b-4833-4b4f-832d-35936ce9eadf"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""baed0306-7d37-42cd-820b-484f1bc099d5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""be5b227e-7bff-4595-9af8-d5d23bb5a42b"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Marker 9"",
                    ""id"": ""ca6e222c-a084-4b06-b063-2f6c894fd41b"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""21ef18c0-782e-4d58-b9ca-00ba8b4e73f7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""809a99f1-8d5b-4abe-96d2-3dd5d1ec04ab"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Snap To Location Marker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c02d827f-8ab3-4977-997b-e59ba51798b6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49405b11-31a0-4465-bab6-100370583143"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleBuildMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_MousePos = m_Player.FindAction("MousePos", throwIfNotFound: true);
        m_Player_LeftClick = m_Player.FindAction("LeftClick", throwIfNotFound: true);
        m_Player_LeftClickHold = m_Player.FindAction("LeftClickHold", throwIfNotFound: true);
        m_Player_RightClick = m_Player.FindAction("RightClick", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Movement = m_Camera.FindAction("Movement", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        m_Camera_Boost = m_Camera.FindAction("Boost", throwIfNotFound: true);
        // DevKeys
        m_DevKeys = asset.FindActionMap("DevKeys", throwIfNotFound: true);
        m_DevKeys_MousePos = m_DevKeys.FindAction("MousePos", throwIfNotFound: true);
        // HotKeys
        m_HotKeys = asset.FindActionMap("HotKeys", throwIfNotFound: true);
        m_HotKeys_SaveLocationMarker = m_HotKeys.FindAction("Save Location Marker", throwIfNotFound: true);
        m_HotKeys_SnapToLocationMarker = m_HotKeys.FindAction("Snap To Location Marker", throwIfNotFound: true);
        m_HotKeys_ToggleMenu = m_HotKeys.FindAction("ToggleMenu", throwIfNotFound: true);
        m_HotKeys_ToggleBuildMenu = m_HotKeys.FindAction("ToggleBuildMenu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_MousePos;
    private readonly InputAction m_Player_LeftClick;
    private readonly InputAction m_Player_LeftClickHold;
    private readonly InputAction m_Player_RightClick;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @MousePos => m_Wrapper.m_Player_MousePos;
        public InputAction @LeftClick => m_Wrapper.m_Player_LeftClick;
        public InputAction @LeftClickHold => m_Wrapper.m_Player_LeftClickHold;
        public InputAction @RightClick => m_Wrapper.m_Player_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @MousePos.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @LeftClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClickHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClickHold;
                @LeftClickHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClickHold;
                @LeftClickHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClickHold;
                @RightClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @LeftClickHold.started += instance.OnLeftClickHold;
                @LeftClickHold.performed += instance.OnLeftClickHold;
                @LeftClickHold.canceled += instance.OnLeftClickHold;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Movement;
    private readonly InputAction m_Camera_Zoom;
    private readonly InputAction m_Camera_Boost;
    public struct CameraActions
    {
        private @PlayerControls m_Wrapper;
        public CameraActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Camera_Movement;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputAction @Boost => m_Wrapper.m_Camera_Boost;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Boost.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnBoost;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // DevKeys
    private readonly InputActionMap m_DevKeys;
    private IDevKeysActions m_DevKeysActionsCallbackInterface;
    private readonly InputAction m_DevKeys_MousePos;
    public struct DevKeysActions
    {
        private @PlayerControls m_Wrapper;
        public DevKeysActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePos => m_Wrapper.m_DevKeys_MousePos;
        public InputActionMap Get() { return m_Wrapper.m_DevKeys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DevKeysActions set) { return set.Get(); }
        public void SetCallbacks(IDevKeysActions instance)
        {
            if (m_Wrapper.m_DevKeysActionsCallbackInterface != null)
            {
                @MousePos.started -= m_Wrapper.m_DevKeysActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_DevKeysActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_DevKeysActionsCallbackInterface.OnMousePos;
            }
            m_Wrapper.m_DevKeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
            }
        }
    }
    public DevKeysActions @DevKeys => new DevKeysActions(this);

    // HotKeys
    private readonly InputActionMap m_HotKeys;
    private IHotKeysActions m_HotKeysActionsCallbackInterface;
    private readonly InputAction m_HotKeys_SaveLocationMarker;
    private readonly InputAction m_HotKeys_SnapToLocationMarker;
    private readonly InputAction m_HotKeys_ToggleMenu;
    private readonly InputAction m_HotKeys_ToggleBuildMenu;
    public struct HotKeysActions
    {
        private @PlayerControls m_Wrapper;
        public HotKeysActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SaveLocationMarker => m_Wrapper.m_HotKeys_SaveLocationMarker;
        public InputAction @SnapToLocationMarker => m_Wrapper.m_HotKeys_SnapToLocationMarker;
        public InputAction @ToggleMenu => m_Wrapper.m_HotKeys_ToggleMenu;
        public InputAction @ToggleBuildMenu => m_Wrapper.m_HotKeys_ToggleBuildMenu;
        public InputActionMap Get() { return m_Wrapper.m_HotKeys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HotKeysActions set) { return set.Get(); }
        public void SetCallbacks(IHotKeysActions instance)
        {
            if (m_Wrapper.m_HotKeysActionsCallbackInterface != null)
            {
                @SaveLocationMarker.started -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSaveLocationMarker;
                @SaveLocationMarker.performed -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSaveLocationMarker;
                @SaveLocationMarker.canceled -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSaveLocationMarker;
                @SnapToLocationMarker.started -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSnapToLocationMarker;
                @SnapToLocationMarker.performed -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSnapToLocationMarker;
                @SnapToLocationMarker.canceled -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnSnapToLocationMarker;
                @ToggleMenu.started -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.performed -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleMenu;
                @ToggleMenu.canceled -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleMenu;
                @ToggleBuildMenu.started -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleBuildMenu;
                @ToggleBuildMenu.performed -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleBuildMenu;
                @ToggleBuildMenu.canceled -= m_Wrapper.m_HotKeysActionsCallbackInterface.OnToggleBuildMenu;
            }
            m_Wrapper.m_HotKeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SaveLocationMarker.started += instance.OnSaveLocationMarker;
                @SaveLocationMarker.performed += instance.OnSaveLocationMarker;
                @SaveLocationMarker.canceled += instance.OnSaveLocationMarker;
                @SnapToLocationMarker.started += instance.OnSnapToLocationMarker;
                @SnapToLocationMarker.performed += instance.OnSnapToLocationMarker;
                @SnapToLocationMarker.canceled += instance.OnSnapToLocationMarker;
                @ToggleMenu.started += instance.OnToggleMenu;
                @ToggleMenu.performed += instance.OnToggleMenu;
                @ToggleMenu.canceled += instance.OnToggleMenu;
                @ToggleBuildMenu.started += instance.OnToggleBuildMenu;
                @ToggleBuildMenu.performed += instance.OnToggleBuildMenu;
                @ToggleBuildMenu.canceled += instance.OnToggleBuildMenu;
            }
        }
    }
    public HotKeysActions @HotKeys => new HotKeysActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnLeftClickHold(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
    }
    public interface IDevKeysActions
    {
        void OnMousePos(InputAction.CallbackContext context);
    }
    public interface IHotKeysActions
    {
        void OnSaveLocationMarker(InputAction.CallbackContext context);
        void OnSnapToLocationMarker(InputAction.CallbackContext context);
        void OnToggleMenu(InputAction.CallbackContext context);
        void OnToggleBuildMenu(InputAction.CallbackContext context);
    }
}
