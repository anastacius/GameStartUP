using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Gameplay.Attribute
{
    public class AttributePool : MonoBehaviour
    {
        private Dictionary<AttributeType, Attribute> attributes = new Dictionary<AttributeType, Attribute>();

        private void Awake()
        {
            Attribute[] childAttributes = transform.GetComponentsInChildren<Attribute>();
            attributes = new Dictionary<AttributeType, Attribute>(childAttributes.Length);
            foreach (Attribute childAttribute in childAttributes)
            {
                if (attributes.ContainsKey(childAttribute.AttributeType))
                {
                    Debug.Log("Duplicated attribute " + childAttribute.AttributeType, this);
                    continue;
                }
                attributes.Add(childAttribute.AttributeType, childAttribute);
            }
        }

        public Attribute GetAttribute(AttributeType targetAttribute)
        {
            if (!attributes.ContainsKey(targetAttribute))
            {
                Debug.Log("Can't found any attribute of type: " + targetAttribute, this);
                return null;
            }
            return attributes[targetAttribute];
        }
    }
}
