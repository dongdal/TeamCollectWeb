Imports System.Threading

Public NotInheritable Class CultureHelper
    Private Sub New()
    End Sub
    ' Valid cultures
    Private Shared ReadOnly _validCultures As New List(Of String)() From { _
        "af", _
        "af-ZA", _
        "sq", _
        "sq-AL", _
        "gsw-FR", _
        "am-ET", _
        "ar", _
        "ar-DZ", _
        "ar-BH", _
        "ar-EG", _
        "ar-IQ", _
        "ar-JO", _
        "ar-KW", _
        "ar-LB", _
        "ar-LY", _
        "ar-MA", _
        "ar-OM", _
        "ar-QA", _
        "ar-SA", _
        "ar-SY", _
        "ar-TN", _
        "ar-AE", _
        "ar-YE", _
        "hy", _
        "hy-AM", _
        "as-IN", _
        "az", _
        "az-Cyrl-AZ", _
        "az-Latn-AZ", _
        "ba-RU", _
        "eu", _
        "eu-ES", _
        "be", _
        "be-BY", _
        "bn-BD", _
        "bn-IN", _
        "bs-Cyrl-BA", _
        "bs-Latn-BA", _
        "br-FR", _
        "bg", _
        "bg-BG", _
        "ca", _
        "ca-ES", _
        "zh-HK", _
        "zh-MO", _
        "zh-CN", _
        "zh-Hans", _
        "zh-SG", _
        "zh-TW", _
        "zh-Hant", _
        "co-FR", _
        "hr", _
        "hr-HR", _
        "hr-BA", _
        "cs", _
        "cs-CZ", _
        "da", _
        "da-DK", _
        "prs-AF", _
        "div", _
        "div-MV", _
        "nl", _
        "nl-BE", _
        "nl-NL", _
        "en", _
        "en-AU", _
        "en-BZ", _
        "en-CA", _
        "en-029", _
        "en-IN", _
        "en-IE", _
        "en-JM", _
        "en-MY", _
        "en-NZ", _
        "en-PH", _
        "en-SG", _
        "en-ZA", _
        "en-TT", _
        "en-GB", _
        "en-US", _
        "en-ZW", _
        "et", _
        "et-EE", _
        "fo", _
        "fo-FO", _
        "fil-PH", _
        "fi", _
        "fi-FI", _
        "fr", _
        "fr-BE", _
        "fr-CA", _
        "fr-FR", _
        "fr-LU", _
        "fr-MC", _
        "fr-CH", _
        "fy-NL", _
        "gl", _
        "gl-ES", _
        "ka", _
        "ka-GE", _
        "de", _
        "de-AT", _
        "de-DE", _
        "de-LI", _
        "de-LU", _
        "de-CH", _
        "el", _
        "el-GR", _
        "kl-GL", _
        "gu", _
        "gu-IN", _
        "ha-Latn-NG", _
        "he", _
        "he-IL", _
        "hi", _
        "hi-IN", _
        "hu", _
        "hu-HU", _
        "is", _
        "is-IS", _
        "ig-NG", _
        "id", _
        "id-ID", _
        "iu-Latn-CA", _
        "iu-Cans-CA", _
        "ga-IE", _
        "xh-ZA", _
        "zu-ZA", _
        "it", _
        "it-IT", _
        "it-CH", _
        "ja", _
        "ja-JP", _
        "kn", _
        "kn-IN", _
        "kk", _
        "kk-KZ", _
        "km-KH", _
        "qut-GT", _
        "rw-RW", _
        "sw", _
        "sw-KE", _
        "kok", _
        "kok-IN", _
        "ko", _
        "ko-KR", _
        "ky", _
        "ky-KG", _
        "lo-LA", _
        "lv", _
        "lv-LV", _
        "lt", _
        "lt-LT", _
        "wee-DE", _
        "lb-LU", _
        "mk", _
        "mk-MK", _
        "ms", _
        "ms-BN", _
        "ms-MY", _
        "ml-IN", _
        "mt-MT", _
        "mi-NZ", _
        "arn-CL", _
        "mr", _
        "mr-IN", _
        "moh-CA", _
        "mn", _
        "mn-MN", _
        "mn-Mong-CN", _
        "ne-NP", _
        "no", _
        "nb-NO", _
        "nn-NO", _
        "oc-FR", _
        "or-IN", _
        "ps-AF", _
        "fa", _
        "fa-IR", _
        "pl", _
        "pl-PL", _
        "pt", _
        "pt-BR", _
        "pt-PT", _
        "pa", _
        "pa-IN", _
        "quz-BO", _
        "quz-EC", _
        "quz-PE", _
        "ro", _
        "ro-RO", _
        "rm-CH", _
        "ru", _
        "ru-RU", _
        "smn-FI", _
        "smj-NO", _
        "smj-SE", _
        "se-FI", _
        "se-NO", _
        "se-SE", _
        "sms-FI", _
        "sma-NO", _
        "sma-SE", _
        "sa", _
        "sa-IN", _
        "sr", _
        "sr-Cyrl-BA", _
        "sr-Cyrl-SP", _
        "sr-Latn-BA", _
        "sr-Latn-SP", _
        "nso-ZA", _
        "tn-ZA", _
        "si-LK", _
        "sk", _
        "sk-SK", _
        "sl", _
        "sl-SI", _
        "es", _
        "es-AR", _
        "es-BO", _
        "es-CL", _
        "es-CO", _
        "es-CR", _
        "es-DO", _
        "es-EC", _
        "es-SV", _
        "es-GT", _
        "es-HN", _
        "es-MX", _
        "es-NI", _
        "es-PA", _
        "es-PY", _
        "es-PE", _
        "es-PR", _
        "es-ES", _
        "es-US", _
        "es-UY", _
        "es-VE", _
        "sv", _
        "sv-FI", _
        "sv-SE", _
        "syr", _
        "syr-SY", _
        "tg-Cyrl-TJ", _
        "tzm-Latn-DZ", _
        "ta", _
        "ta-IN", _
        "tt", _
        "tt-RU", _
        "te", _
        "te-IN", _
        "th", _
        "th-TH", _
        "bo-CN", _
        "tr", _
        "tr-TR", _
        "tk-TM", _
        "ug-CN", _
        "uk", _
        "uk-UA", _
        "wen-DE", _
        "ur", _
        "ur-PK", _
        "uz", _
        "uz-Cyrl-UZ", _
        "uz-Latn-UZ", _
        "vi", _
        "vi-VN", _
        "cy-GB", _
        "wo-SN", _
        "sah-RU", _
        "ii-CN", _
        "yo-NG" _
    }
    ' Include ONLY cultures you are implementing
    ' first culture is the DEFAULT
    ' Spanish NEUTRAL culture
    ' Arabic NEUTRAL culture
    Private Shared ReadOnly _cultures As New List(Of String)() From { _
        "fr-FR", _
        "en"
    }
    ''' <summary>
    ''' Returns true if the language is a right-to-left language. Otherwise, false.
    ''' </summary>
    Public Shared Function IsRighToLeft() As Boolean
        Return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft

    End Function
    ''' <summary>
    ''' Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
    ''' </summary>
    ''' <param name="name">Culture's name (e.g. en-US)</param>
    Public Shared Function GetImplementedCulture(name As String) As String
        ' make sure it's not null
        If String.IsNullOrEmpty(name) Then
            Return GetDefaultCulture()
        End If
        ' return Default culture
        ' make sure it is a valid culture first
        If _validCultures.Where(Function(c) c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() = 0 Then
            Return GetDefaultCulture()
        End If
        ' return Default culture if it is invalid
        ' if it is implemented, accept it
        If _cultures.Where(Function(c) c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0 Then
            Return name
        End If
        ' accept it
        ' Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
        ' the function will return closes match that is "en-US" because at least the language is the same (ie English)  
        Dim n = GetNeutralCulture(name)
        For Each c As String In _cultures
            If c.StartsWith(n) Then
                Return c
            End If
        Next
        ' else 
        ' It is not implemented
        Return GetDefaultCulture()
        ' return Default culture as no match found
    End Function
    ''' <summary>
    ''' Returns default culture name which is the first name decalared (e.g. en-US)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetDefaultCulture() As String
        Return _cultures(0)
        ' return Default culture
    End Function
    Public Shared Function GetCurrentCulture() As String
        Return Thread.CurrentThread.CurrentCulture.Name
    End Function
    Public Shared Function GetCurrentNeutralCulture() As String
        Return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name)
    End Function
    Public Shared Function GetNeutralCulture(name As String) As String
        If Not name.Contains("-") Then
            Return name
        End If

        Return name.Split("-"c)(0)
        ' Read first part only. E.g. "en", "es"
    End Function
End Class