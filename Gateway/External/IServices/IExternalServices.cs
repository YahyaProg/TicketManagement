using Core.GenericResultModel;
using Gateway.Model.External.Requests;
using Gateway.Model.External.Responses;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.External.IServices
{
    /// <summary>
    /// [Header("XApiKey", "2LPYp9mF2KfZhtmHINin2LnYqtio2KfYsdin2Kog2KjYp9mG2qkg2b7Yp9ix2LPbjNin2YY=")]
    /// </summary>
    public interface IExternalServices
    {
        #region Parsian Local

        /// <summary>
        /// امضادارهای حقوقی
        /// </summary>
        /// <param name="cbComsignRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbComsign")]
        Task<Response<ApiResult<List<CbComsignResponse>>>> CbComsign([Body] CbComsignRequest request);

        /// <summary>
        /// سپرده نقدی
        /// </summary>
        /// <param name="cbSepordenaghdiRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbSepordenaghdi")]
        Task<Response<ApiResult<List<CbSepordenaghdiResponse>>>> CbSepordenaghdi([Body] CbSepordenaghdiRequest request);

        /// <summary>
        /// CbAccountTurnovers
        /// </summary>
        /// <param name="CbAccountTurnoversRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbAccountTurnover")]
        Task<Response<ApiResult<List<CbAccountTurnoverResponse>>>> CbAccountTurnover([Body] CbAccountTurnoversRequest request);

        /// <summary>
        /// مشخخات صاحب سپرده
        /// </summary>
        /// <param name="cbCustomerRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbCustomer")]
        Task<Response<ApiResult<CbCustomerResponse>>> CbCustomer([Body] CbCustomerRequest request);

        /// <summary>
        /// تاخیر باز پرداخت
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsianLocal/CbLatePayment")]
        Task<Response<ApiResult<CbLatePaymentResponse>>> CbLatePayment([Query] string customerId);

        /// <summary>
        /// جزئیات تاخیر بازپرداخت
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsianLocal/CbLatePaymentDtlResponse")]
        Task<Response<ApiResult<List<CbLatePaymentDtlResponse>>>> CbLatePaymentDtlResponse([Query] int customerId);

        /// <summary>
        /// تسهیلات
        /// </summary>
        /// <param name="cbLoanRequest"></param>
        /// <returns></returns> 
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbLoan")]
        Task<Response<ApiResult<List<CbLoanResponse>>>> CbLoan([Body] CbLoanRequest request);

        /// <summary>
        /// تعهدات
        /// </summary>
        /// <param name="cbLcRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbLc")]
        Task<Response<ApiResult<List<CbLcResponse>>>> CbLc([Body] CbLcRequest request);

        /// <summary>
        /// ضمانت نامه اعطایی
        /// </summary>
        /// <param name="cbBgRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbBg")]
        Task<Response<ApiResult<List<CbBgResponse>>>> CbBg([Body] CbBgRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbBgloanlcRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbBgloanlc")]
        Task<Response<ApiResult<CbBgloanlcResponse>>> CbBgloanlc([Body] CbBgloanlcRequest request);

        /// <summary>
        /// وثایق در اختیار بانک
        /// </summary>
        /// <param name="cbSecuritiesRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbSecurities")]
        Task<Response<ApiResult<List<CbSecuritiesResponse>>>> CbSecurities([Body] CbSecuritiesRequest request);

        /// <summary>
        /// چک های در اختیار بانک
        /// </summary>
        /// <param name="cbDepositedChequesRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbDepositedCheques")]
        Task<Response<ApiResult<List<CbDepositedChequesResponse>>>> CbDepositedCheques([Body] CbDepositedChequesRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbCustrelRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/CbCustrel")]
        Task<Response<ApiResult<CbCustrelResponse>>> CbCustrel([Body] CbCustrelRequest request);


        /// <summary>
        /// دریافت شعب زیرمجموعه شعبه ورودی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/GetAllBranchRelations")]
        Task<Response<ApiResult<BranchResponse>>> GetAllBranchRelations([Body] BranchCodeRequest request);


        /// <summary>
        /// دریافت  کاربران شعبه ورودی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsianLocal/GetAllBranchUsers")]
        Task<Response<ApiResult<BranchUserResponse>>> GetAllBranchUsers([Body] BranchCodeRequest request);


        #endregion

        #region Parsian

        /// <summary>
        /// دریافت شماره شبا
        /// </summary>
        /// <param name="iban"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsian/ShebaEnq")]
        Task<Response<ApiResult<ShebaEnqResponse>>> ShebaEnq([Query] string iban);

        /// <summary>
        /// میانگین مانده حساب
        /// </summary>
        /// <param name="averageAccountBalanceRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/AverageAccountBalance")]
        Task<Response<ApiResult<AverageAccountBalanceResponse>>> AverageAccountBalance([Body] AverageAccountBalanceRequest request);

        /// <summary>
        /// استعلام چک
        /// </summary>
        /// <param name="estelamChequeRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/EstelamCheque")]
        Task<Response<ApiResult<EstelamChequeResponse>>> EstelamCheque([Body] EstelamChequeRequest request);

        /// <summary>
        /// اسعلام جزئیات چک
        /// </summary>
        /// <param name="chequeId"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsian/EstelamChequeDetail")]
        Task<Response<ApiResult<EstelamChequeDetailResponse>>> EstelamChequeDetail([Query] string chequeId);

        /// <summary>
        /// دریافت اطلاعات شناسایی
        /// </summary>
        /// <param name="getIdentificationRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/GetIdentification")]
        Task<Response<ApiResult<GetIdentificationResponse>>> GetIdentification([Body] GetIdentificationRequest request);

        /// <summary>
        /// استعلام سربازی
        /// </summary>
        /// <param name="natioalId"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsian/InquiryMilitary")]
        Task<Response<ApiResult<InquiryMilitaryResponse>>> InquiryMilitary([Query] string natioalId);

        /// <summary>
        /// استعلام سربازی
        /// </summary>
        /// <param name="samatEstelamRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/SamatEstelam")]
        Task<Response<ApiResult<SamatEstelamResponse>>> SamatEstelam([Body] SamatEstelamRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samatEstelamByRqNumberRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/SamatEstelamByRqNumber")]
        Task<Response<ApiResult<SamatEstelamByRqNumberResponse>>> SamatEstelamByRqNumber([Body] SamatEstelamByRqNumberRequest request);

        /// <summary>
        /// استعلام وثیقه
        /// </summary>
        /// <param name="estelamVasigheRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/EstelamVasighe")]
        Task<Response<ApiResult<EstelamVasigheResponse>>> EstelamVasighe([Body] EstelamVasigheRequest request);

        /// <summary>
        /// استعلام ضامن
        /// </summary>
        /// <param name="estelamZamenRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/EstelamZamen")]
        Task<Response<ApiResult<EstelamZamenResponse>>> EstelamZamen([Body] EstelamZamenRequest request);

        /// <summary>
        /// استعلام شاهکار
        /// </summary>
        /// <param name="inquiryShahkarRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/InquiryShahkar")]
        Task<Response<ApiResult<InquiryShahkarResponse>>> InquiryShahkar([Body] InquiryShahkarRequest request);

        /// <summary>
        /// استعلام کدپستی
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Get("parsian/InquiryPostalCode")]
        Task<Response<ApiResult<InquiryPostalCodeResponse>>> InquiryPostalCode([Query] string postalCode);

        /// <summary>
        /// دریافت گزارش اعتبارسنجی
        /// </summary>
        /// <param name="iranianScoreInquiryRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/iranianScoreInquiry")]
        Task<Response<ApiResult<IranianScoreInquiryResponse>>> IranianScoreInquiry([Body] IranianScoreInquiryRequest request);

        /// <summary>
        /// دریافت اطلاعات اعتبار سنجی ایرانیان
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/iranianScoreInquiry")]
        Task<Response<ApiResult<GetIranianReportResponse>>> IranianGetReport([Body] GetIranianReportRequest request);

        /// <summary>
        /// سرویس مانده بدهی کوتاه مدت (تا یک سال ) و بلند مدت( بالای یک سال )  عقود بانکی به غیر از خرید دین نزد سیستم بانکی
        /// </summary>
        /// <param name="GetSamatLoansInfoRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/GetSamatLoansInfo")]
        Task<Response<ApiResult<GetSamatLoansInfoResponse>>> GetSamatLoansInfo([Query] GetSamatLoansInfoRequest request);

        /// <summary>
        /// سرویس مانده تسهیلات غیر جاری( معوق – سررسید گذشته – مشکوک الوصول ) نزد سیستم بانکی
        /// </summary>
        /// <param name="GetSamatDebtorLoansInClaimRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/GetSamatDebtorLoansInClaim")]
        Task<Response<ApiResult<double?>>> GetSamatDebtorLoansInClaim([Query] GetSamatDebtorLoansInClaimRequest request);

        /// <summary>
        /// سرویس  مانده ضمانتنامه گمرکی نزد سیستم بانکی
        /// </summary>
        /// <param name="GetSamatGuaranteesInfoRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/GetSamatGuaranteesInfo")]
        Task<Response<ApiResult<GetSamatLoansInfoResponse>>> GetSamatGuaranteesInfo([Query] GetSamatGuaranteesInfoRequest request);

        /// <summary>
        /// دریافت بدهی مشتری در دو سال گذشته
        /// </summary>
        /// <param name="GetCustomerDebitInTwoLastYearRequest"></param>
        /// <returns></returns>
        [AllowAnyStatusCode]
        [Post("parsian/GetCustomerDebitInTwoLastYear")]
        Task<Response<ApiResult<GetCustomerDebitInTwoLastYearResponse>>> GetCustomerDebitInTwoLastYear([Query] GetCustomerDebitInTwoLastYearRequest request);

        #endregion
    }
}
