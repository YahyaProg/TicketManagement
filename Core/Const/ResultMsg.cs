using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Const;


public static class ResultMessage
{
    public static class Error
    {
        public const string Common = "هنگام انجام عملیات خطایی رخ داده است !";
        public const string FailedCommon = "عملیات انجام نشد";
        public const string RequiredAttribute = "لطفا {0} را وارد کنید !";
        public const string MaxLength = "حداکثر تعداد کارکتر {0} 255 عدد میباشد.";

        public static string notFound(string value = "")
            => $" {value} یافت نشد ";
        public static string minValueCurrency(long value)
            => $" مبلغ نباید کمتر از {value} ریال باشد";
        public static string required(string value)
            => $"{value} اجباری است";
    }

    public static class Success
    {
        public const string Common = "عملیات با موفقبت انجام شد";
    }
}