using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameobject;
    public GameObject MainAvatarGameobject;

    public GameObject AvatarHeadGameobject;
    public GameObject AvatarBodyGameobject;

    public GameObject[] AvatarModelPrefabs;

    public TextMeshProUGUI PlayerName_Text;

    // Start is called before the first frame update
    void Start()
    {
        //Setup the player

        if (photonView.IsMine)
        {
            //The player is local
            LocalXRRigGameobject.SetActive(true);
            gameObject.GetComponent<MovementController>().enabled = true;
            gameObject.GetComponent<AvatarInputConverter>().enabled = true;


            //Getting the avatar selection data so that the correct avatar models can be instantiated.
            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER,out avatarSelectionNumber))
            {
                Debug.Log("Avatar selection number: "+ (int)avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel",RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }



            SetLayerRecursively(AvatarHeadGameobject,11);
            SetLayerRecursively(AvatarBodyGameobject,12);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if (teleportationAreas.Length>0)
            {
                Debug.Log("Found "+ teleportationAreas.Length+ " teleportation area.");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }

            MainAvatarGameobject.AddComponent<AudioListener>();


        }
        else
        {
            //The player is remote
            LocalXRRigGameobject.SetActive(false);
            gameObject.GetComponent<MovementController>().enabled = false;
            gameObject.GetComponent<AvatarInputConverter>().enabled = false;

            //Remote players can be seen by the local player
            //So, we set the avatar head and body to Default layer
            SetLayerRecursively(AvatarHeadGameobject, 0);
            SetLayerRecursively(AvatarBodyGameobject, 0);
        }

        if (PlayerName_Text != null)
        {
            PlayerName_Text.text = photonView.Owner.NickName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber], LocalXRRigGameobject.transform);

        AvatarInputConverter avatarInputConverter = transform.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
        SetUpAvatarGameobject(avatarHolder.HeadTransform, avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform, avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}
