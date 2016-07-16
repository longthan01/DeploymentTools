using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SMTools.Extensions;

namespace SMTools.Utility
{
    public static class XmlLoader
    {
        private static List<ConfigSection> XmlConfigs
        {
            get;
            set;
        }
        /// <summary>
        /// Get or Set path to configuration file
        /// </summary>
        public static string ConfigurationFile
        {
            get;
            set;
        }

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
        /// <summary>
        /// Initialize configuration
        /// </summary>
        /// <param name="configFile">Path to configuration file</param>
        public static void Initialize(string configFile)
        {
            List<ConfigSection> xmlConfigs = new List<ConfigSection>();
            XmlElement root = GetRoot(configFile);
            foreach (XmlNode child in root.ChildNodes)
            {
                ConfigSection section = new ConfigSection();
                section.SectionName = child.Name;

                XmlNodeList configs = child.SelectNodes("config");
                foreach (XmlNode node in configs)
                {
                    XmlNode name = node.SelectSingleNode("name");
                    XmlNode value = node.SelectSingleNode("value");
                    ConfigItem item = new ConfigItem()
                    {
                        Name = name.InnerText,
                        Value = value.InnerText
                    };
                    XmlNodeList attrs = node.SelectNodes("attribute");
                    if (attrs != null)
                    {
                        foreach (XmlNode att in attrs)
                        {
                            XmlNode an = att.SelectSingleNode("name");
                            XmlNode av = att.SelectSingleNode("value");
                            ConfigItem attributeItem = new ConfigItem()
                            {
                                Name = an.InnerText,
                                Value = av.InnerText
                            };
                            item.Attributes.Add(attributeItem);
                        }
                    }
                    section.Items.Add(item);
                }
                xmlConfigs.Add(section);
            }
            XmlConfigs = xmlConfigs;
        }
        /// <summary>
        /// Get configuration items by section name
        /// </summary>
        /// <param name="sectionName">Section name in config file</param>
        /// <returns>An object of ConfigItemCollection</returns>
        public static ConfigItemCollection GetConfig(string sectionName)
        {
            var section = XmlConfigs.FirstOrDefault(x => x.SectionName.SuperEquals(sectionName));
            return section == null ? null : section.Items;
        }

        public static void Save(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("configurations");
            foreach (ConfigSection section in XmlConfigs)
            {
                XmlElement sc = doc.CreateElement(section.SectionName);
                foreach (ConfigItem item in section.Items) // save each section to xml
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
                    sc.AppendChild(configItem);
                }
                root.AppendChild(sc);
            }
            doc.AppendChild(root);
            SaveConfig(doc, fileName);
        }
        /// <summary>
        /// Get Inner text of a node in xml file
        /// </summary>
        /// <param name="nodeName">Node name</param>
        /// <param name="xmlFile">Path to xml file</param>
        /// <returns>Inner text of node</returns>
        public static string GetValue(string nodeName, string xmlFile)
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
