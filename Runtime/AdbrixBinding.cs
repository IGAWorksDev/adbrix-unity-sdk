#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
#define HAS_ADBRIX_SDK
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdbrixPlugin
{
    public static class Constants
    {
        public const string KEY_STRING_CLASS_NAME = "className";
        public const string KEY_STRING_METHOD_NAME = "methodName";
        public const string KEY_OBJECT_METHOD_PARAM = "methodParam";
        public const string KEY_ANY_METHOD_ARGS_1 = "args1";
        public const string KEY_ANY_METHOD_ARGS_2 = "args2";
        public const string KEY_ANY_METHOD_ARGS_3 = "args3";
        public const string KEY_ANY_METHOD_ARGS_4 = "args4";
        public const string KEY_ANY_METHOD_ARGS_5 = "args5";
        public const string KEY_INVOKE_METHOD_NAME = "invoke";
    }
    public static class ABDeepLinkResult
    {
        public const int PROCESSED = 0;
        public const int ORGANIC = 1;
        public const int TRACKING_LINK_SETTINGS_INCORRECTLY = 2;
        public const int ORGANIC_NCPI_IN_PROCESS = 3;
        public const int NO_CONVERSION = -1;
        public static string GetResult(int value)
        {
            switch (value)
            {
                case PROCESSED: return nameof(PROCESSED);
                case ORGANIC: return nameof(ORGANIC);
                case TRACKING_LINK_SETTINGS_INCORRECTLY: return nameof(TRACKING_LINK_SETTINGS_INCORRECTLY);
                case ORGANIC_NCPI_IN_PROCESS: return nameof(ORGANIC_NCPI_IN_PROCESS);
                case NO_CONVERSION: return nameof(NO_CONVERSION);
                default: return null;
            }
        }
    }
    public static class ABEvent
    {
        public const string LOGIN = "abx:login";
        public const string LOGOUT = "abx:logout";
        public const string SIGN_UP = "abx:sign_up";
        public const string USE_CREDIT = "abx:use_credit";
        public const string APP_UPDATE = "abx:app_update";
        public const string INVITE = "abx:invite";
        public const string PURCHASE = "abx:purchase";
        public const string LEVEL_ACHIEVED = "abx:level_achieved";
        public const string TUTORIAL_COMPLETED = "abx:tutorial_completed";
        public const string CHARACTER_CREATED = "abx:character_created";
        public const string STAGE_CLEARED = "abx:stage_cleared";
        public const string REFUND = "abx:refund";
        public const string ADD_TO_CART = "abx:add_to_cart";
        public const string ADD_TO_WISHLIST = "abx:add_to_wishlist";
        public const string PRODUCT_VIEW = "abx:product_view";
        public const string CATEGORY_VIEW = "abx:category_view";
        public const string REVIEW_ORDER = "abx:review_order";
        public const string SEARCH = "abx:search";
        public const string SHARE = "abx:share";
        public const string VIEW_HOME = "abx:view_home";
        public const string LIST_VIEW = "abx:list_view";
        public const string CART_VIEW = "abx:cart_view";
        public const string PAYMENT_INFO_ADDED = "abx:paymentinfo_added";
    }
    public static class ABEventProperty
    {
        public const string IS_SKIP = "abx:is_skip";
        public const string LEVEL = "abx:level";
        public const string STAGE = "abx:stage";
        public const string PREV_VER = "abx:prev_ver";
        public const string CURR_VER = "abx:curr_ver";
        public const string KEYWORD = "abx:keyword";
        public const string SHARING_CHANNEL = "abx:sharing_channel";
        public const string SIGN_CHANNEL = "abx:sign_channel";
        public const string INVITE_CHANNEL = "abx:invite_channel";
        public const string ORDER_ID = "abx:order_id";
        public const string DELIVERY_CHARGE = "abx:delivery_charge";
        public const string PENALTY_CHARGE = "abx:penalty_charge";
        public const string PAYMENT_METHOD = "abx:payment_method";
        public const string ORDER_SALES = "abx:order_sales";
        public const string DISCOUNT = "abx:discount";
        public const string CATEGORY1 = "abx:category1";
        public const string CATEGORY2 = "abx:category2";
        public const string CATEGORY3 = "abx:category3";
        public const string CATEGORY4 = "abx:category4";
        public const string CATEGORY5 = "abx:category5";
        public const string ITEMS = "abx:items";
        public const string ITEM_PRODUCT_ID = "abx:product_id";
        public const string ITEM_PRODUCT_NAME = "abx:product_name";
        public const string ITEM_PRICE = "abx:price";
        public const string ITEM_QUANTITY = "abx:quantity";
        public const string ITEM_DISCOUNT = "abx:discount";
        public const string ITEM_CURRENCY = "abx:currency";
        public const string ITEM_CATEGORY1 = "abx:category1";
        public const string ITEM_CATEGORY2 = "abx:category2";
        public const string ITEM_CATEGORY3 = "abx:category3";
        public const string ITEM_CATEGORY4 = "abx:category4";
        public const string ITEM_CATEGORY5 = "abx:category5";
    }
    public static class ABCurrency
    {
        public const string KRW = "KRW";
        public const string USD = "USD";
        public const string JPY = "JPY";
        public const string EUR = "EUR";
        public const string GBP = "GBP";
        public const string CNY = "CNY";
        public const string TWD = "TWD";
        public const string HKD = "HKD";
        public const string IDR = "IDR"; // Indonesia
        public const string INR = "INR"; // India
        public const string RUB = "RUB"; // Russia
        public const string THB = "THB"; // Thailand
        public const string VND = "VND"; // Vietnam
        public const string MYR = "MYR"; // Malaysia
    }
    public static class ABInviteChannel
    {
        public const string KAKAO = "Kakao";
        public const string NAVER = "Naver";
        public const string LINE = "Line";
        public const string GOOGLE = "Google";
        public const string FACEBOOK = "Facebook";
        public const string TWITTER = "Twitter";
        public const string WHATSAPP = "whatsApp";
        public const string QQ = "QQ";
        public const string WECHAT = "WeChat";
        public const string ETC = "ETC";
    }
    public static class ABPaymentMethod
    {
        public const string CREDIT_CARD = "CreditCard";
        public const string BANK_TRANSFER = "BankTransfer";
        public const string MOBILE_PAYMENT = "MobilePayment";
        public const string ETC = "ETC";
    }
    public static class ABSharingChannel
    {
        public const string FACEBOOK = "Facebook";
        public const string KAKAOTALK = "KakaoTalk";
        public const string KAKAOSTORY = "KakaoStory";
        public const string LINE = "Line";
        public const string WHATSAPP = "whatsApp";
        public const string QQ = "QQ";
        public const string WECHAT = "WeChat";
        public const string SMS = "SMS";
        public const string EMAIL = "Email";
        public const string COPY_URL = "copyUrl";
        public const string ETC = "ETC";
    }
    public static class ABSignUpChannel
    {
        public const string KAKAO = "Kakao";
        public const string NAVER = "Naver";
        public const string LINE = "Line";
        public const string GOOGLE = "Google";
        public const string FACEBOOK = "Facebook";
        public const string TWITTER = "Twitter";
        public const string WHATSAPP = "whatsApp";
        public const string QQ = "QQ";
        public const string WECHAT = "WeChat";
        public const string ETC = "ETC";
        public const string SKT_ID = "SkTid";
        public const string APPLE_ID = "AppleId";
        public const string USER_ID = "UserId";
    }


    public static class ABConfig
    {
        public const string IOS_LOG_ENABLE = "df_config_log_enable";
        public const string ANDROID_LOG_ENABLE = "android_log_enable";
        public const string ANDROID_LOG_LEVEL = "android_log_level";
        public const string ANDROID_COLLECT_GOOGLE_ADVERTISING_ID = "android_collect_google_advertising_id";
    }
    public static class ABAndroidLogLevel
    {
        public const int VERBOSE = 2;
        public const int DEBUG = 3;
        public const int INFO = 4;
        public const int WARN = 5;
        public const int ERROR = 6;
        public const int ASSERT = 7;
    }

}