using System.Collections.Generic;

namespace Gateway.Model.External.Responses
{
    public class EstelamVasigheResponse
    {
        public string BankCode { get; set; }
        public string DateEstlm { get; set; }
        public string RequestNum { get; set; }
        public string ShenaseEstlm { get; set; }
        public string ShenaseRes { get; set; }
        public string ShobeCode { get; set; }
        public List<EstelamVasigheRows> EstelamVasigheRows { get; set; }
    }

    public class EstelamVasigheRows
    {
        public string Amount { get; set; }
        public string Priority { get; set; }
        /// <summary>
        /// [کالا] kala = 10,
        /// [چک] chek = 11,
        /// [قراردادهای لازم الاجرا] qarardad_haye_lazem_ol_ejra = 12,
        /// [فلزات گرانبها] felezat_geranbaha = 13,
        /// [زمین زراعی] zamin_zeraee = 14,
        /// [بیمه نامه صندوق ضمانت صادرات] bimenameh_sandoq_zemanat_saderat = 15,
        /// [ضمانت نامه] zemanatnameh = 20,
        /// [اوراق بهادار] oraq_bahadar = 30,
        /// [غیرمنقول ملکی] qeyr_manghol_melki =41,
        /// [غیرمنقول محل طرح] qeyr_manghol_mahal_tarh = 42,
        /// [غیرمنقول کارخانه] qeyr_manghol_karkhaneh = 43,
        /// [غیرمنقول ماشین آلات و تجهیزات] qeyr_manghol_mashin_alat_va_tajhizaat = 44,
        /// [سفته و بروات] sefteh_brovat = 50,
        /// [سپرده های قرض الحسنه] sepordeh_haye_qarz_ol_hasaneh = 60,
        /// [نقدی] naghdi = 70
        /// </summary>
        public string Type { get; set; }
    }
}
