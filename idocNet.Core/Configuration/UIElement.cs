using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using idocNet.Core.Data.Entities.Validation;

namespace idocNet.Core.Configuration
{
    public class UIElement : ConfigurationElement, IEnsureValidation
    {
        /// <summary>
        /// Determines the amount of messages per page are shown on the topic detail page
        /// </summary>
        [ConfigurationProperty("messagesPerPage", IsRequired = true)]
        public int MessagesPerPage
        {
            get
            {
                return (int)this["messagesPerPage"];
            }
            set
            {
                this["messagesPerPage"] = value;
            }
        }

        /// <summary>
        /// Determines the amount of topics per page are shown on the forum detail page
        /// </summary>
        [ConfigurationProperty("topicsPerPage", IsRequired = true)]
        public int TopicsPerPage
        {
            get
            {
                return (int)this["topicsPerPage"];
            }
            set
            {
                this["topicsPerPage"] = value;
            }
        }
        /// <summary>
        /// Phân trang theo số lượng default = 10 bản ghi 1 trang
        /// </summary>
        [ConfigurationProperty("defaultPerPage", IsRequired = true)]
        public int DefaultPerPage
        {
            get
            {
                return (int)this["defaultPerPage"];
            }
            set
            {
                this["defaultPerPage"] = value;
            }
        }
        /// <summary>
        /// Hop - sua lay 5 tin theo yeu cau anh trung
        /// </summary>
        [ConfigurationProperty("listNewsPerPage", IsRequired = true)]
        public int ListNewsPerPage
        {
            get
            {
                return (int)this["listNewsPerPage"];
            }
            set
            {
                this["listNewsPerPage"] = value;
            }
        }

        /// <summary>
        /// Amount of tags in the tag cloud
        /// </summary>
        [ConfigurationProperty("tagsCloudCount", IsRequired = true)]
        public int TagsCloudCount
        {
            get
            {
                return (int)this["tagsCloudCount"];
            }
            set
            {
                this["tagsCloudCount"] = value;
            }
        }

        [ConfigurationProperty("dateFormat", IsRequired = false)]
        public string DateFormat
        {
            get
            {
                return (string)this["dateFormat"];
            }
            set
            {
                this["dateFormat"] = value;
            }
        }

        [ConfigurationProperty("template", IsRequired = true)]
        public TemplateElement Template
        {
            get
            {
                return (TemplateElement)this["template"];
            }
            set
            {
                this["template"] = value;
            }
        }

        [ConfigurationProperty("resources", IsRequired = true)]
        public ResourcesElement Resources
        {
            get
            {
                return (ResourcesElement)this["resources"];
            }
            set
            {
                this["resources"] = value;
            }
        }

        /// <summary>
        /// Determines if user details (image, member since, ...) are shown on topic detail page
        /// </summary>
        [ConfigurationProperty("showUserDetailsOnList", IsRequired = false)]
        public bool ShowUserDetailsOnList
        {
            get
            {
                return (bool)this["showUserDetailsOnList"];
            }
            set
            {
                this["showUserDetailsOnList"] = value;
            }
        }

        /// <summary>
        /// Determines which is the default forum sorting of topics
        /// </summary>
        [ConfigurationProperty("defaultForumSort", IsRequired = false)]
        public ForumSort DefaultForumSort
        {
            get
            {
                return (ForumSort)this["defaultForumSort"];
            }
            set
            {
                this["defaultForumSort"] = value;
            }
        }

        #region IEnsureValidation Members

        public void ValidateFields()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
