using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace OpenStrata.Xml
{
    
    /// <summary>
    /// Constains Extension methods for objects of OpenStataXDocument type.
    /// </summary>
    public static class OpenStrataXDocumentExtensions
    {


        public static XElement ReTypeChildren<childType>(this XElement parent, XName childElements)
            where childType : XElement, new()
        {

            foreach (XElement childElement in parent.Elements(childElements).InDocumentOrder())
            {

                 var reTypedElement = new childType();

                reTypedElement.ReplaceAll(childElement.Attributes(), childElement.Nodes());

                childElement.ReplaceWith(reTypedElement);
                
            }

            return parent;
        }

        #region LazyLoad

        public static XAttribute LazyLoad(this XElement parent, string name, XAttribute privateHolderIn, out XAttribute privateHolderOut, object defaultValue = null)
        {
            privateHolderOut = privateHolderIn;
            if (privateHolderOut == null)
            {
                privateHolderOut = parent.GetOrCreateAttribute(name, defaultValue);
            }
            return privateHolderOut;
        }

        public static XElement LazyLoad(this XContainer parent, string name, XElement privateHolderIn, out XElement privateHolderOut)
        {
            privateHolderOut = privateHolderIn;
            if (privateHolderOut == null)
            {
                privateHolderOut = parent.GetOrCreateElement(name);
            }
            return privateHolderOut;
        }

        public static XElement LazyLoadRetypeChildren<childType>(this XContainer parent, string name, XName ChildElementName, XElement privateHolderIn, out XElement privateHolderOut)
            where childType : XElement, new()
        {
            privateHolderOut = privateHolderIn;
            if (privateHolderOut == null)
            {
                privateHolderOut = parent.GetOrCreateElement(name).ReTypeChildren<childType>(ChildElementName);
            }
            return privateHolderOut;
        }

        public static x LazyLoad<x>(this XContainer parent, string name, x privateHolderIn, out x privateHolderOut)
            where x : XElement, new()
        {

            privateHolderOut = privateHolderIn;

            privateHolderOut = privateHolderIn;
            if (privateHolderOut == null)
            {
                privateHolderOut = new x();

                privateHolderOut.ReplaceAll(parent.GetOrCreateElement(name).Nodes());

                //TODO:  Monitor this to ensure the new node keeps its attachment to the parent container.
                parent.GetOrCreateElement(name).ReplaceWith(privateHolderOut);
            }
            return privateHolderOut;
        }

        #endregion

        #region AttachTo

        public static XElement AttachTo (this XElement element, XContainer parent)
        {
            parent.Add(element);

            return element;
        }

        public static XElement ReplaceElement(this XElement newElement, XElement replacing)
        {
            //replacing?.AddBeforeSelf(newElement);
            //replacing.Remove();

            replacing.ReplaceWith(newElement);

            return newElement;
        }


        public static XAttribute AttachTo(this XAttribute attribute, XElement parent)
        {
            parent.Add(attribute);
            return attribute;
        }

        #endregion

        #region GetOrCreateAttribute

        public static XAttribute GetOrCreateAttribute(this XElement parent, string name, string attrNameSpace = "", object value = null)
        {
            return parent.GetOrCreateAttribute(XName.Get(name, attrNameSpace), value);
        }

        public static XAttribute GetOrCreateAttribute(this XElement parent, XName xname, object value = null)
        {
            return parent.Attribute(xname) ?? new XAttribute(xname, value ?? string.Empty).AttachTo(parent);
        }


        #endregion

        #region GetOrCreateElement

        public static XElement GetOrCreateElement(this OpenStrataXDocument parent, string name, XNamespace defaultNamespace = null)
        {
            return parent.GetOrCreateElement(name, defaultNamespace ?? parent.DefaultNameSpace);
        }


        //public static XElement GetOrCreateElement(this XContainer parent, string name, XNamespace defaultNamespace = null)
        //{

        //    return parent.GetOrCreateElement(name, defaultNamespace ?? parent.Document.Root.GetDefaultNamespace());
        //}

        public static XElement GetOrCreateElement(this XContainer parent, string name, XNamespace defaultNamespace = null)
        {
            return parent.GetOrCreateElement((defaultNamespace  ?? parent?.Document?.Root.GetDefaultNamespace())?.GetName(name) ?? XName.Get(name,""));
        }

        public static XElement GetOrCreateElement(this XContainer parent, XName xname)
        {
            return parent.Element(xname) ??
                        new XElement(xname).AttachTo(parent);
        }


        #endregion

        //public static XElement CreateElement(this XName usingName)
        //{
        //    return new XElement(usingName);
        //}

        public static XElement AddXmlnsAttribute(this XElement toElement, string xmlnsPrefix, string xmlnsName, bool whenTrue = true)
        {
            if (!whenTrue) return toElement;

            var root = toElement.Document.Root;

            var xmlns = root.Attribute(XNamespace.Xmlns + xmlnsPrefix);
            if (xmlns == null) root.Add(new XAttribute(XNamespace.Xmlns + xmlnsPrefix, xmlnsName));

            return toElement;
        }

        public static XElement AddText(this XElement element, string text)
        {
            return element.AddXOject(new XText(text));
        }

        public static XElement AddXOject(this XElement element, XObject xobject)
        {
            element.Add(xobject);
            return element;
        }


        public static XName DefaultNilName = XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance");
        public static void SetValueOrNil(this XElement element, object value, XName nilName = null)
        {
            var nil = nilName ?? DefaultNilName;

            if (value == null)
            {
                element.RemoveNodes();
                element.Value = string.Empty;
                element.GetOrCreateAttribute(nil, true);
            }
            else
            {
                element.SetValue(value);
                element.Attribute(nil)?.Remove();
            }

        }


        //public static XElement AddNilAttr(this XElement element, XName nilName, bool value = true)
        //{
        //     element.GetOrCreateAttribute()
        //}


    }
}
