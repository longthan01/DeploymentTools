using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SMTools.DeploymentBase
{
    public static class XmlLoader
    {
        #region private methods
        private static XmlDocument GetDocument(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            return doc;
        }

        private static void SaveConfig(XmlDocument document, string destPath)
        {
            XmlWriterSettings st = new XmlWriterSettings();
            st.Indent = true;
            using (XmlWriter wr = XmlWriter.Create(destPath, st))
            {
                document.Save(wr);
            }
        }

        private static XmlElement GetRoot(string fileName)
        {
            XmlDocument doc = GetDocument(fileName);
            return doc.DocumentElement;
        }
        #endregion

        #region public methods
        public static ConfigItemCollection GetConfig(string fileName)
        {
            ConfigItemCollection collection = new ConfigItemCollection();
            XmlElement root = GetRoot(fileName);
            XmlNodeList configs = root.SelectNodes("config");
            foreach (XmlNode node in configs)
            {
                XmlNode name = node.SelectSingleNode("name");
                XmlNode value = node.SelectSingleNode("value");
                ConfigItem item = new ConfigItem()
                {
                    Name = name.InnerText,
                    Value = value.InnerText
                };
                XmlNodeList attrs = node.SelectNodes("attributes");
                if (attrs != null)
                {
                    foreach (XmlNode att in attrs)
                    {
                        XmlNode an = node.SelectSingleNode("name");
                        XmlNode av = node.SelectSingleNode("value");
                        ConfigItem attributeItem = new ConfigItem()
                        {
                            Name = an.InnerText,
                            Value = av.InnerText
                        };
                        item.Attributes.Add(attributeItem);
                    }
                }
                collection.Add(item);
            }
            return collection;
        }

        public static void Save(ConfigItemCollection configItemsCollection, string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("configurations");
            foreach (ConfigItem item in configItemsCollection)
            {
                XmlElement configItem = doc.CreateElement("config");

                XmlElement name = doc.CreateElement("name");
                name.InnerText = item.Name;
                XmlElement value = doc.CreateElement("value");
                value.InnerText = item.Value;

                if (item.Attributes != null && item.Attributes.Count > 0)
                {
                    XmlElement attribute = doc.CreateElement("attributes");
                    foreach (ConfigItem att in item.Attributes)
                    {
                        XmlElement attNodeName = doc.CreateElement("name");
                        attNodeName.InnerText = att.Name;
                        XmlElement attNodeValue = doc.CreateElement("value");
                        attNodeValue.InnerText = att.Value;
                        attribute.AppendChild(attNodeName);
                        attribute.AppendChild(attNodeValue);
                    }
                    configItem.AppendChild(attribute);
                }
                configItem.AppendChild(name);
                configItem.AppendChild(value);
                root.AppendChild(configItem);
            }
            doc.AppendChild(root);
            SaveConfig(doc, fileName);
        }
        /// <summary>
        /// Get inner text of nodeName in xml file
        /// </summary>
        /// <param name="nodeName">Node name</param>
        /// <param name="xmlFile">The path to xml file</param>
        /// <returns></returns>
        public static string GetValue(string nodeName, string xmlFile)
        {
            XmlElement root = GetRoot(xmlFile);
            XmlNode node = root.SelectSingleNode(nodeName);
            string res = null;
            if (node != null)
            {
                res = node.InnerText;
            }
            return res;
        }

        public static string GetValueIteration(string nodeName, string xmlFile)
        {
            XmlElement root = GetRoot(xmlFile);
            string res = null;
            Stack<XmlNode> stack = new Stack<XmlNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                XmlNode node = stack.Pop();
                foreach (XmlNode i in node.ChildNodes)
                {
                    if (i.Name.SuperEquals(nodeName))
                    {
                        res = i.InnerText;
                        stack.Clear();
                        return res;
                    }
                    stack.Push(i);
                }
            }
            return res;
        }
        #endregion
    }
}
