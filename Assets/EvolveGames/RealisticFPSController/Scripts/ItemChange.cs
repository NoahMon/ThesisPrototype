using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class ItemChange : MonoBehaviour
    {
        [Header("Item Change")]
        [SerializeField] public Animator ani;
        [SerializeField] bool LoopItems = true;
        [SerializeField, Tooltip("You can add your new item here.")] GameObject[] Items;
        [SerializeField, Tooltip("These logos must have the same order as the items.")] Sprite[] ItemLogos;
        [SerializeField] int ItemIdInt;
        int MaxItems;
        int ChangeItemInt;
        [HideInInspector] public bool DefiniteHide;

        private void Start()
        {
            if (ani == null && GetComponent<Animator>()) ani = GetComponent<Animator>();
            DefiniteHide = false;
            ChangeItemInt = ItemIdInt;
            MaxItems = Items.Length - 1;
          
        }
        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                ItemIdInt++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                ItemIdInt--;
            }

            if(Input.GetKeyDown(KeyCode.H))
            {
                if (ani.GetBool("Hide")) Hide(false);
                else Hide(true);
            }

            if (ItemIdInt < 0) ItemIdInt = LoopItems ? MaxItems : 0;
            if (ItemIdInt > MaxItems) ItemIdInt = LoopItems ? 0 : MaxItems;


            if (ItemIdInt != ChangeItemInt)
            {
                ChangeItemInt = ItemIdInt;
               
            }
        }

        public void Hide(bool Hide)
        {
            DefiniteHide = Hide;
            ani.SetBool("Hide", Hide);
        }

    }

}
