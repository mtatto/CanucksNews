﻿//    -------------------------------------------------------------------------------------------- 
//    Copyright (c) 2011 Microsoft Corporation.  All rights reserved. 
//    Use of this sample source code is subject to the terms of the Microsoft license 
//    agreement under which you licensed this sample source code and is provided AS-IS. 
//    If you did not accept the terms of the license agreement, you are not authorized 
//    to use this sample source code.  For the terms of the license, please see the 
//    license agreement between you and Microsoft. 
﻿//    -------------------------------------------------------------------------------------------- 

 /// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.450")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class statuses
{

    private statusesStatus[] statusField;

    private string typeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("status")]
    public statusesStatus[] status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }
}
/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.450")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class statusesStatus
{

    private string created_atField;

    private ulong idField;

    private string textField;

    private string sourceField;

    private bool truncatedField;

    private bool favoritedField;

    private string in_reply_to_status_idField;

    private string in_reply_to_user_idField;

    private string in_reply_to_screen_nameField;

    private byte retweet_countField;

    private bool retweetedField;

    private statusesStatusUser userField;

    private object geoField;

    private object coordinatesField;

    private object placeField;

    private bool possibly_sensitiveField;

    private bool possibly_sensitiveFieldSpecified;

    private object contributorsField;

    /// <remarks/>
    public string created_at
    {
        get
        {
            return this.created_atField;
        }
        set
        {
            this.created_atField = value;
        }
    }

    /// <remarks/>
    public ulong id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    public string source
    {
        get
        {
            return this.sourceField;
        }
        set
        {
            this.sourceField = value;
        }
    }

    /// <remarks/>
    public bool truncated
    {
        get
        {
            return this.truncatedField;
        }
        set
        {
            this.truncatedField = value;
        }
    }

    /// <remarks/>
    public bool favorited
    {
        get
        {
            return this.favoritedField;
        }
        set
        {
            this.favoritedField = value;
        }
    }

    /// <remarks/>
    public string in_reply_to_status_id
    {
        get
        {
            return this.in_reply_to_status_idField;
        }
        set
        {
            this.in_reply_to_status_idField = value;
        }
    }

    /// <remarks/>
    public string in_reply_to_user_id
    {
        get
        {
            return this.in_reply_to_user_idField;
        }
        set
        {
            this.in_reply_to_user_idField = value;
        }
    }

    /// <remarks/>
    public string in_reply_to_screen_name
    {
        get
        {
            return this.in_reply_to_screen_nameField;
        }
        set
        {
            this.in_reply_to_screen_nameField = value;
        }
    }

    /// <remarks/>
    public byte retweet_count
    {
        get
        {
            return this.retweet_countField;
        }
        set
        {
            this.retweet_countField = value;
        }
    }

    /// <remarks/>
    public bool retweeted
    {
        get
        {
            return this.retweetedField;
        }
        set
        {
            this.retweetedField = value;
        }
    }

    /// <remarks/>
    public statusesStatusUser user
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }

    /// <remarks/>
    public object geo
    {
        get
        {
            return this.geoField;
        }
        set
        {
            this.geoField = value;
        }
    }

    /// <remarks/>
    public object coordinates
    {
        get
        {
            return this.coordinatesField;
        }
        set
        {
            this.coordinatesField = value;
        }
    }

    /// <remarks/>
    public object place
    {
        get
        {
            return this.placeField;
        }
        set
        {
            this.placeField = value;
        }
    }

    /// <remarks/>
    public bool possibly_sensitive
    {
        get
        {
            return this.possibly_sensitiveField;
        }
        set
        {
            this.possibly_sensitiveField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool possibly_sensitiveSpecified
    {
        get
        {
            return this.possibly_sensitiveFieldSpecified;
        }
        set
        {
            this.possibly_sensitiveFieldSpecified = value;
        }
    }

    /// <remarks/>
    public object contributors
    {
        get
        {
            return this.contributorsField;
        }
        set
        {
            this.contributorsField = value;
        }
    }
}
/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.450")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class statusesStatusUser
{

    private ulong idField;

    private string nameField;

    private string screen_nameField;

    private string locationField;

    private string descriptionField;

    private string profile_image_urlField;

    private string profile_image_url_httpsField;

    private string urlField;

    private bool protectedField;

    private ulong followers_countField;

    private string profile_background_colorField;

    private string profile_text_colorField;

    private string profile_link_colorField;

    private string profile_sidebar_fill_colorField;

    private string profile_sidebar_border_colorField;

    private ulong friends_countField;

    private string created_atField;

    private long favourites_countField;

    private long utc_offsetField;

    private string time_zoneField;

    private string profile_background_image_urlField;

    private string profile_background_image_url_httpsField;

    private bool profile_background_tileField;

    private bool profile_use_background_imageField;

    private string notificationsField;

    private bool geo_enabledField;

    private bool verifiedField;

    private string followingField;

    private ulong statuses_countField;

    private string langField;

    private bool contributors_enabledField;

    private string follow_request_sentField;

    private long listed_countField;

    private bool show_all_inline_mediaField;

    private bool default_profileField;

    private bool default_profile_imageField;

    private bool is_translatorField;

    /// <remarks/>
    public ulong id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string screen_name
    {
        get
        {
            return this.screen_nameField;
        }
        set
        {
            this.screen_nameField = value;
        }
    }

    /// <remarks/>
    public string location
    {
        get
        {
            return this.locationField;
        }
        set
        {
            this.locationField = value;
        }
    }

    /// <remarks/>
    public string description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string profile_image_url
    {
        get
        {
            return this.profile_image_urlField;
        }
        set
        {
            this.profile_image_urlField = value;
        }
    }

    /// <remarks/>
    public string profile_image_url_https
    {
        get
        {
            return this.profile_image_url_httpsField;
        }
        set
        {
            this.profile_image_url_httpsField = value;
        }
    }

    /// <remarks/>
    public string url
    {
        get
        {
            return this.urlField;
        }
        set
        {
            this.urlField = value;
        }
    }

    /// <remarks/>
    public bool @protected
    {
        get
        {
            return this.protectedField;
        }
        set
        {
            this.protectedField = value;
        }
    }

    /// <remarks/>
    public ulong followers_count
    {
        get
        {
            return this.followers_countField;
        }
        set
        {
            this.followers_countField = value;
        }
    }

    /// <remarks/>
    public string profile_background_color
    {
        get
        {
            return this.profile_background_colorField;
        }
        set
        {
            this.profile_background_colorField = value;
        }
    }

    /// <remarks/>
    public string profile_text_color
    {
        get
        {
            return this.profile_text_colorField;
        }
        set
        {
            this.profile_text_colorField = value;
        }
    }

    /// <remarks/>
    public string profile_link_color
    {
        get
        {
            return this.profile_link_colorField;
        }
        set
        {
            this.profile_link_colorField = value;
        }
    }

    /// <remarks/>
    public string profile_sidebar_fill_color
    {
        get
        {
            return this.profile_sidebar_fill_colorField;
        }
        set
        {
            this.profile_sidebar_fill_colorField = value;
        }
    }

    /// <remarks/>
    public string profile_sidebar_border_color
    {
        get
        {
            return this.profile_sidebar_border_colorField;
        }
        set
        {
            this.profile_sidebar_border_colorField = value;
        }
    }

    /// <remarks/>
    public ulong friends_count
    {
        get
        {
            return this.friends_countField;
        }
        set
        {
            this.friends_countField = value;
        }
    }

    /// <remarks/>
    public string created_at
    {
        get
        {
            return this.created_atField;
        }
        set
        {
            this.created_atField = value;
        }
    }

    /// <remarks/>
    public long favourites_count
    {
        get
        {
            return this.favourites_countField;
        }
        set
        {
            this.favourites_countField = value;
        }
    }

    /// <remarks/>
    public long utc_offset
    {
        get
        {
            return this.utc_offsetField;
        }
        set
        {
            this.utc_offsetField = value;
        }
    }

    /// <remarks/>
    public string time_zone
    {
        get
        {
            return this.time_zoneField;
        }
        set
        {
            this.time_zoneField = value;
        }
    }

    /// <remarks/>
    public string profile_background_image_url
    {
        get
        {
            return this.profile_background_image_urlField;
        }
        set
        {
            this.profile_background_image_urlField = value;
        }
    }

    /// <remarks/>
    public string profile_background_image_url_https
    {
        get
        {
            return this.profile_background_image_url_httpsField;
        }
        set
        {
            this.profile_background_image_url_httpsField = value;
        }
    }

    /// <remarks/>
    public bool profile_background_tile
    {
        get
        {
            return this.profile_background_tileField;
        }
        set
        {
            this.profile_background_tileField = value;
        }
    }

    /// <remarks/>
    public bool profile_use_background_image
    {
        get
        {
            return this.profile_use_background_imageField;
        }
        set
        {
            this.profile_use_background_imageField = value;
        }
    }

    /// <remarks/>
    public string notifications
    {
        get
        {
            return this.notificationsField;
        }
        set
        {
            this.notificationsField = value;
        }
    }

    /// <remarks/>
    public bool geo_enabled
    {
        get
        {
            return this.geo_enabledField;
        }
        set
        {
            this.geo_enabledField = value;
        }
    }

    /// <remarks/>
    public bool verified
    {
        get
        {
            return this.verifiedField;
        }
        set
        {
            this.verifiedField = value;
        }
    }

    /// <remarks/>
    public string following
    {
        get
        {
            return this.followingField;
        }
        set
        {
            this.followingField = value;
        }
    }

    /// <remarks/>
    public ulong statuses_count
    {
        get
        {
            return this.statuses_countField;
        }
        set
        {
            this.statuses_countField = value;
        }
    }

    /// <remarks/>
    public string lang
    {
        get
        {
            return this.langField;
        }
        set
        {
            this.langField = value;
        }
    }

    /// <remarks/>
    public bool contributors_enabled
    {
        get
        {
            return this.contributors_enabledField;
        }
        set
        {
            this.contributors_enabledField = value;
        }
    }

    /// <remarks/>
    public string follow_request_sent
    {
        get
        {
            return this.follow_request_sentField;
        }
        set
        {
            this.follow_request_sentField = value;
        }
    }

    /// <remarks/>
    public long listed_count
    {
        get
        {
            return this.listed_countField;
        }
        set
        {
            this.listed_countField = value;
        }
    }

    /// <remarks/>
    public bool show_all_inline_media
    {
        get
        {
            return this.show_all_inline_mediaField;
        }
        set
        {
            this.show_all_inline_mediaField = value;
        }
    }

    /// <remarks/>
    public bool default_profile
    {
        get
        {
            return this.default_profileField;
        }
        set
        {
            this.default_profileField = value;
        }
    }

    /// <remarks/>
    public bool default_profile_image
    {
        get
        {
            return this.default_profile_imageField;
        }
        set
        {
            this.default_profile_imageField = value;
        }
    }

    /// <remarks/>
    public bool is_translator
    {
        get
        {
            return this.is_translatorField;
        }
        set
        {
            this.is_translatorField = value;
        }
    }
}
