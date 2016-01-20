using System;
using System.Configuration;

namespace ff.Study.DesignPattern.Concept.Configurating
{
    /*
     .NET Framework提供的主要配置实体类
     System.Configuration.ConfigurationSectionGroup（配置节组）sectionGroup
     System.Configuration.ConfigurationSection（配置节）section
     System.Configuration.ConfigurationElementCollection（配置元素集合）
     System.Configuration.ConfigurationElement（配置元素）
     */

    public abstract class NamedConfigurationElementBase : ConfigurationElement
    {
        private const string NameItem = "name";
        private const string DescriptionItem = "description";

        [ConfigurationProperty(NameItem, IsKey = true, IsRequired = true)]
        public virtual string Name { get { return base[NameItem] as string; } }

        [ConfigurationProperty(DescriptionItem, IsRequired = false)]
        public virtual string Description { get { return base[DescriptionItem] as string; } }
    }

    public class ExampleConfigurationElement : NamedConfigurationElementBase { }

    public class DiagramConfigurationElement : NamedConfigurationElementBase { }

    public class PictureConfigurationElement : NamedConfigurationElementBase
    {
        private const string ColorizedItem = "colorized";

        [ConfigurationProperty(ColorizedItem, IsRequired = true)]
        public bool Colorized { get { return (bool)base[ColorizedItem]; } }
    }

    //定义包括NamedConfigurationElementBase的ConfigurationElementCollection
    [ConfigurationCollection(typeof(NamedConfigurationElementBase), CollectionType=ConfigurationElementCollectionType.AddRemoveClearMap)]
    public abstract class NamedConfigurationElementCollectionBase<T> : ConfigurationElementCollection where T : NamedConfigurationElementBase, new()
    {
        //外部通过index获取集合中特定的configuration element
        public T this[int index] { get { return (T)base.BaseGet(index); } }
        public new T this[string name] { get { return (T)base.BaseGet(name); } }

        /// <summary>
        /// 创建一个新的 NamedConfigurationElement 实例
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// 获取集合中某个特定 NamedConfigurationElement的key（Name属性）
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as T).Name;
        }
    }

    public class ExampleConfigurationElementCollection : NamedConfigurationElementCollectionBase<ExampleConfigurationElement>
    {
    }

    public class DiagramConfigurationElementCollection : NamedConfigurationElementCollectionBase<DiagramConfigurationElement>
    {
    }

    public class PictureConfigurationElementCollection : NamedConfigurationElementCollectionBase<PictureConfigurationElement>
    {
    }

    /// <summary>
    /// example的ConfigurationElementCollection（optional）
    /// diagrams的ConfigurationElementCollection（optional）
    /// </summary>
    public abstract class ParagraphConfigurationSectionBase : ConfigurationSection
    {
        private const string ExamplesItem = "examples";
        private const string DiagramsItem = "diagrams";

        [ConfigurationProperty(ExamplesItem, IsRequired = false)]
        public virtual ExampleConfigurationElementCollection Examples
        {
            get
            {
                return base[ExamplesItem] as ExampleConfigurationElementCollection;
            }
        }

        [ConfigurationProperty(DiagramsItem, IsRequired = false)]
        public virtual DiagramConfigurationElementCollection Diagrams
        {
            get
            {
                return base[DiagramsItem] as DiagramConfigurationElementCollection;
            }
        }
    }

    public class DelegatingParagramConfigurationSection : ParagraphConfigurationSectionBase
    {
        private const string PicturesItem = "pictures";

        [ConfigurationProperty(PicturesItem, IsRequired = false)]
        public virtual PictureConfigurationElementCollection Pictures
        {
            get
            {
                return base[PicturesItem] as PictureConfigurationElementCollection;
            }
        }
    }

    public class GenericsParagramConfigurationSection : ParagraphConfigurationSectionBase
    {
    }

    /// <summary>
    /// 整个配置节组的对象，包括delegating和generics两配置节
    /// </summary>
    public class ChapterConfigurationSectionGroup : ConfigurationSectionGroup
    {
        private const string DelegatingItem = "delegating";
        private const string GenericsItem = "generics";

        public ChapterConfigurationSectionGroup() : base() { }

        [ConfigurationProperty(DelegatingItem, IsRequired = true)]
        public virtual DelegatingParagramConfigurationSection Delegating
        {
            get
            {
                return base.Sections[DelegatingItem] as DelegatingParagramConfigurationSection;
            }
        }

        [ConfigurationProperty(GenericsItem, IsRequired = true)]
        public virtual GenericsParagramConfigurationSection Generics
        {
            get
            {
                return base.Sections[GenericsItem] as GenericsParagramConfigurationSection;
            }
        }
    }

    //用于调度 App.Config相关Configuration的Broker类型
    public static class ConfigurationBroker
    {
        private static ChapterConfigurationSectionGroup group;

        static ConfigurationBroker()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var gp =  config.GetSectionGroup("ff.study.designPattern.concept");
            group = (ChapterConfigurationSectionGroup)gp;
        }

        public static DelegatingParagramConfigurationSection Delegating { get { return group.Delegating; } }

        public static GenericsParagramConfigurationSection Generics { get { return group.Generics; } }
    }
}
